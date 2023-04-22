using System.Windows;
using System.Windows.Controls;
using Tisa.Store.Models.DataTransfers;
using Tisa.Store.Models.ViewModels;

namespace Tisa.Store.Pages;

public partial class FilterPage : UserControl
{
    public ApplicationViewModel ViewModel
    {
        get
        {
            return (ApplicationViewModel)DataContext;
        }
    }

    public FilterPage()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Filters.Add(new ProductFilterDTO(ViewModel.Filters.Count));
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        ProductFilterDTO? row = (ProductFilterDTO)((Button)sender).DataContext;
        if (row != null)
        {
            ViewModel.Filters.Remove(row);
        }
        
        
    }
}