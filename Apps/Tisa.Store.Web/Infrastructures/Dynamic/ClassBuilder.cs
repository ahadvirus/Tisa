using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;

namespace Tisa.Store.Web.Infrastructures.Dynamic;

public class ClassBuilder
{
    private AssemblyName AssemblyName { get; }

    public ClassBuilder(string className)
    {
        AssemblyName = new AssemblyName(className);
    }

    public object? CreateObject(IDictionary<string, Type?> properties)
    {
        
        TypeBuilder dynamicClass = CreateClass();
        CreateConstructor(dynamicClass);
        foreach (KeyValuePair<string, Type?> property in properties)
        {
            if(property.Value == null || string.IsNullOrEmpty(property.Key))
            {
                continue;
            }
            
            CreateProperty(dynamicClass, property.Key, property.Value);
        }

        Type type = dynamicClass.CreateType();

        return Activator.CreateInstance(type);
    }

    private TypeBuilder CreateClass()
    {
        AssemblyBuilder assemblyBuilder =
            AssemblyBuilder.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(nameof(Tisa));
        TypeBuilder typeBuilder = moduleBuilder.DefineType(
            AssemblyName.FullName,
            TypeAttributes.Public |
            TypeAttributes.Class |
            TypeAttributes.AutoClass |
            TypeAttributes.AnsiClass |
            TypeAttributes.BeforeFieldInit |
            TypeAttributes.AutoLayout,
            null
        );
        return typeBuilder;
    }

    private void CreateConstructor(TypeBuilder typeBuilder)
    {
        typeBuilder.DefineDefaultConstructor(
            MethodAttributes.Public |
            MethodAttributes.SpecialName |
            MethodAttributes.RTSpecialName
        );
    }

    private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
    {
        FieldBuilder fieldBuilder = typeBuilder.DefineField(
            string.Format("_{0}", JsonNamingPolicy.CamelCase.ConvertName(propertyName)),
            propertyType,
            FieldAttributes.Private
        );

        PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(
            propertyName,
            PropertyAttributes.HasDefault,
            propertyType,
            null
        );

        MethodBuilder getPropMethodBuilder = typeBuilder.DefineMethod(
            string.Format("get_{0}", propertyName),
            MethodAttributes.Public |
            MethodAttributes.SpecialName |
            MethodAttributes.HideBySig,
            propertyType,
            Type.EmptyTypes
        );

        ILGenerator getIl = getPropMethodBuilder.GetILGenerator();

        getIl.Emit(OpCodes.Ldarg_0);
        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
        getIl.Emit(OpCodes.Ret);

        MethodBuilder setPropMethodBuilder = typeBuilder.DefineMethod(
            string.Format("set_{0}", propertyName),
            MethodAttributes.Public |
            MethodAttributes.SpecialName |
            MethodAttributes.HideBySig,
            null,
            new[] { propertyType }
        );

        ILGenerator setIl = setPropMethodBuilder.GetILGenerator();
        Label modifyProperty = setIl.DefineLabel();
        Label exitSet = setIl.DefineLabel();

        setIl.MarkLabel(modifyProperty);
        setIl.Emit(OpCodes.Ldarg_0);
        setIl.Emit(OpCodes.Ldarg_1);
        setIl.Emit(OpCodes.Stfld, fieldBuilder);

        setIl.Emit(OpCodes.Nop);
        setIl.MarkLabel(exitSet);
        setIl.Emit(OpCodes.Ret);

        propertyBuilder.SetGetMethod(getPropMethodBuilder);
        propertyBuilder.SetSetMethod(setPropMethodBuilder);
    }
}