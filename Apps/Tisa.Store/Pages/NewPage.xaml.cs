using System.Windows;
using System.Windows.Controls;
using Tisa.Store.Models.ViewModels;

namespace Tisa.Store.Pages;

public partial class NewPage : UserControl
{
    public ApplicationViewModel ViewModel
    {
        get
        {
            return (ApplicationViewModel)DataContext;
        }
    }
    
    private Models.Data.Contexts.AppContext DbContext { get; }
    
    public NewPage()
    {
        InitializeComponent();

        DbContext = new Models.Data.Contexts.AppContext();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Selected != null)
        {
            //return;
            DbContext.Products.Add(ViewModel.Selected);
            DbContext.SaveChanges();
            ViewModel.Products.Add(ViewModel.Selected);
        }
        
    }
}