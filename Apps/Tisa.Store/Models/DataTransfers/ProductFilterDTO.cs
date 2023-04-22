using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tisa.Store.Models.Contracts;
using Tisa.Store.Models.Entities;

namespace Tisa.Store.Models.DataTransfers
{
    public class ProductFilterDTO : BaseViewModel
    {
        public ProductFilterDTO(int id)
        {
            Id = id;
            left = string.Empty;
            operation = string.Empty;
            right = string.Empty;
        }

        public int Id { get; }

        public IDictionary<string, string> Properties
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {
                        nameof(string.Empty),
                        string.Empty
                    },
                    {
                        nameof(Product.Title),
                        "نوع"
                    },
                    {
                        nameof(Product.Count),
                        "تعداد"
                    },
                    {
                        nameof(Product.Power),
                        "مقدار"
                    },
                    {
                        nameof(Product.Unit),
                        "واحد"
                    },
                    {
                        nameof(Product.Description),
                        "توضیحات"
                    }
                };
            }
        }

        private string left;

        public string Left
        {
            get { return left; }
            set
            {
                left = value;
                OnPropertyChanged(nameof(Left));
            }
        }

        public IDictionary<string, string> Operations
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {
                        nameof(string.Empty),
                        string.Empty
                    },
                    {
                        nameof(Expression.GreaterThan),
                        "بیشتر"
                    },
                    {
                        nameof(Expression.GreaterThanOrEqual),
                        "بیشتر یا مساوری"
                    },
                    {
                        nameof(Expression.LessThan),
                        "کمتر"
                    },
                    {
                        nameof(Expression.LessThanOrEqual),
                        "کمتر یا مساوی"
                    },
                    {
                        nameof(Expression.Equal),
                        "برابر"
                    },
                    {
                        nameof(Expression.NotEqual),
                        "نابرابر"
                    }
                };
            }
        }

        public string operation;

        public string Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }

        private string right;


        public string Right
        {
            get { return right; }
            set
            {
                right = value;
                OnPropertyChanged(nameof(Right));
            }
        }

        public Func<Product, bool>? Prediction()
        {
            Func<Product, bool>? result = null;
            MethodInfo? binary = typeof(Expression)
                .GetMethods()
                .FirstOrDefault(method => 
                    string.Compare(method.Name, Operation, StringComparison.OrdinalIgnoreCase) == 0 && 
                                          method.GetParameters().Length == 2
                    );

            if (binary != null)
            {
                ParameterExpression parameter = Expression.Parameter(typeof(Product), nameof(Product).ToLower());
                MemberExpression member = Expression.Property(parameter, Left);
                ConstantExpression constant = Expression.Constant(Right, typeof(string));

                object? invoke = binary.Invoke(null, new object?[] { member, constant });
                if (invoke != null)
                {
                    Expression<Func<Product, bool>> lambda = Expression.Lambda<Func<Product, bool>>(
                        (BinaryExpression)invoke,
                        new ParameterExpression[]
                        {
                            parameter
                        }
                    );

                    result = lambda.Compile();
                }
            }

            return result;
        }
    }
}