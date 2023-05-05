using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows;
using Tisa.Store.Models;
using Expression = System.Linq.Expressions.Expression;

namespace Tisa.Store
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ApplicationViewModel ViewModel { get; }

        //private Models.Data.Contexts.AppContext DbContext { get; }

        private string ClassName
        {
            get
            {
                return "Entity";
            }
        }
        
        private object? Entity { get; }

        public MainWindow()
        {
            InitializeComponent();

            ClassBuilder classBuilder = new ClassBuilder(ClassName);

            Entity = classBuilder.CreateObject(new Dictionary<string, Type>()
            {
                { "Id", typeof(int) },
                { "Name", typeof(string) },
                { "Count", typeof(string) }
            });

            if (Entity != null)
            {
                Debug.WriteLine(string.Format("\n\n{0}\n\n", Entity.GetType().FullName));
            }

            /*
            DbContext = new Models.Data.Contexts.AppContext();

            ViewModel = new ApplicationViewModel(
                DbContext.Products.ToList(),
                new List<ProductFilterDTO>(),
                new List<MenuDTO>()
                {
                    new MenuDTO()
                    {
                        Title = "محصولات",
                        Control = null,
                        Children =
                        {
                            new MenuDTO()
                            {
                                Title = "نمایش"
                            }
                        }
                    }
                },
                new MainPage()

            );

            DataContext = ViewModel;

            foreach (MenuDTO menu in ViewModel.Menu)
            {
                UIElement element = menu.Element;
                MenuStackPanel.Children.Add(element);
            }
            */

            //MainFramePage = new MainPage(ViewModels.Products, ViewModels.Filters);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new MainPage(ViewModels.Products, ViewModels.Filters));
            /*
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage.xlsx");

            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

            int rows = range.Rows.Count;
            int columns = range.Columns.Count;

            for (int row = 2; row <= rows; row++)
            {
                List<KeyValuePair<int, string>> values = new List<KeyValuePair<int, string>>();
                int store = 0;
                for (int column = 1; column <= columns; column++)
                {
                    if (column == 1)
                    {
                        store = (range.Cells[row, column] != null && range.Cells[row, column].Value2 != null) ?
                            int.Parse(range.Cells[row, column].Value2.ToString()) :
                            0;
                        continue;
                    }

                    int number = (column - 1);
                    do
                    {
                        number = number - 5;
                    }
                    while (number > 0);

                    number = number + 5;
                    string value = (range.Cells[row, column] != null && range.Cells[row, column].Value2 != null) ?
                        range.Cells[row, column].Value2.ToString() :
                        string.Empty;

                    values.Add(new KeyValuePair<int, string>(number, value));

                    if (number == 5)
                    {
                        ViewModels.Add(new Product(store, values));
                        values.Clear();
                    }
                }
            }
            
            ViewModels.Selected = ViewModels.Products.First();

            foreach (Product product in ViewModels.Products)
            {
                if (product.Valid)
                {
                    DbContext.Products.Add(product);
                    DbContext.SaveChanges();
                }
            }
            */
        }

        private void ListButton_OnClick(object sender, RoutedEventArgs e)
        {
            Type? entityType = Type.GetType(ClassName);
            if (entityType != null)
            {
                Debug.WriteLine(string.Format("\n\n{0}\n\n", entityType.FullName));
            }
            //ViewModel.Page = new MainPage();
        }

        private void FilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Entity == null)
            {
                return;
            }
            
            Type instanceType = Entity.GetType();
        
            ParameterExpression instance = Expression.Parameter(instanceType, ClassName.ToLower());

            Type propertyType = typeof(int);
            
            Type propertyFunc = typeof(Func<,>).MakeGenericType(instanceType, propertyType);
        
            MemberExpression property = Expression.Property(instance, "Id");
            
            LambdaExpression expression = Expression.Lambda(propertyFunc, property, instance);
            
            Debug.WriteLine("\n\n OK \n\n");
            //ViewModel.Page = new FilterPage();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            //ViewModel.Selected = new Product();

            //ViewModel.Page = new NewPage();
        }
    }
}