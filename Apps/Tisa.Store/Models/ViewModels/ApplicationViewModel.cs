using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Tisa.Store.Models.Contracts;
using Tisa.Store.Models.DataTransfers;
using Tisa.Store.Models.Entities;

namespace Tisa.Store.Models.ViewModels;

public class ApplicationViewModel : BaseViewModel
{
    public ApplicationViewModel(
        IEnumerable<Product> products,
        IEnumerable<ProductFilterDTO> filters,
        IEnumerable<MenuDTO> menu,
        UserControl page
    )
    {
        Products = new ObservableCollection<Product>(products);
        Filters = new ObservableCollection<ProductFilterDTO>(filters);
        Menu = new ObservableCollection<MenuDTO>(menu);

        _selected = null;
        _page = page;
    }

    public ObservableCollection<Product> Products { get; }

    public ObservableCollection<ProductFilterDTO> Filters { get; }

    public ObservableCollection<MenuDTO> Menu { get; }

    public IEnumerable<Product> Filter
    {
        get
        {
            IEnumerable<Product> result = Products;

            foreach (ProductFilterDTO filter in Filters)
            {
                Func<Product, bool>? predicate = filter.Prediction();
                if (predicate != null)
                {
                    MethodInfo? where = typeof(Enumerable)
                        .GetMethods()
                        .Where(
                            method =>
                                string.Compare(
                                    method.Name,
                                    nameof(Enumerable.Where),
                                    StringComparison.OrdinalIgnoreCase
                                ) == 0
                        )
                        .Select(method => method.MakeGenericMethod(typeof(Product)))
                        .FirstOrDefault(method => method.GetParameters()
                            .Any(
                                parameter => parameter.ParameterType == typeof(Func<Product, bool>)
                            )
                        );
                    if (where != null)
                    {
                        object? invoke = where.Invoke(
                            null,
                            new object[]
                            {
                                result,
                                predicate
                            }
                        );

                        if (invoke != null)
                        {
                            result = (IEnumerable<Product>)invoke;
                        }
                    }
                }
            }

            return result;
        }
    }

    private Product? _selected;

    public Product? Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            OnPropertyChanged(nameof(Selected));
        }
    }

    private UserControl? _page;

    public UserControl? Page
    {
        get { return _page; }
        set
        {
            _page = value;
            OnPropertyChanged(nameof(Page));
        }
    }
}