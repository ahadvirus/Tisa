using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tisa.Store.Models.DataTransfers;
using Tisa.Store.Models.Entities;
using Tisa.Store.Models.ViewModels;
using Tisa.Store.Pages;

namespace Tisa.Store
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationViewModel ViewModel { get; }

        private Models.Data.Contexts.AppContext DbContext { get; }

        public MainWindow()
        {
            InitializeComponent();

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
            ViewModel.Page = new MainPage();
        }

        private void FilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Page = new FilterPage();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Selected = new Product();

            ViewModel.Page = new NewPage();
        }
    }
}