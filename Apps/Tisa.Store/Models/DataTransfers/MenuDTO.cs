using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tisa.Store.Models.DataTransfers
{
    public class MenuDTO
    {
        public MenuDTO()
        {
            Name = string.Empty;
            Title = string.Empty;
            Control = null;
            Children = new List<MenuDTO>();

        }

        public string Name { get; set; }
        public string Title { get; set; }
        public UserControl? Control { get; set; }
        public List<MenuDTO> Children { get; }

        public UIElement Element
        {
            get
            {
                UIElement result = new Grid();

                if (Children.Any())
                {
                    ((Grid)result).RowDefinitions.Add(new RowDefinition());

                    Expander expander = new Expander()
                    {
                        Header = Title,
                        //Padding = new Thickness(37, 14, 37, 14),
                        FontSize = 15,
                        IsExpanded = Children.Any(),
                        Visibility = Children.Any() ? Visibility.Visible : Visibility.Collapsed,
                        //Width = 210,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Background = null,
                        Foreground = Brushes.White,
                    };

                    ListView listView = new ListView()
                    {

                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        Foreground = Brushes.White,
                        Items = { Children.Select(menu => menu.Element).ToArray() }
                    };

                    expander.Content = listView;

                    Grid.SetRow(expander, 0);

                    ((Grid)result).Children.Add(expander);
                }
                else
                {
                    ((Grid)result).Children.Add(new TextBlock()
                    {
                        Text = Title,
                        DataContext = Control
                    });
                }

                return result;
                /*
                return Children.Any()
                    ? new Grid()
                    {
                        RowDefinitions = { new RowDefinition()},
                        Margin = new Thickness(5),
                        Children =
                        {
                            new ListBoxItem()
                            {
                                Content = Title,
                                Padding = new Thickness(37, 14, 37, 14),
                                FontSize = 15,
                                Foreground = Brushes.White,
                                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                VerticalContentAlignment = VerticalAlignment.Stretch,
                                Visibility = Children.Any() ? Visibility.Collapsed : Visibility.Visible,

                            },
                            new Expander()
                            {
                                Header = Title,
                                Padding = new Thickness(37, 14, 37, 14),
                                FontSize = 15,
                                IsExpanded = Children.Any(),
                                Visibility = Children.Any() ? Visibility.Visible : Visibility.Collapsed,
                                Width = 210,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Background = null,
                                Foreground = Brushes.White,
                                
                                Content = new ScrollViewer()
                                    {
                                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                                        Content = new ListView()
                                            {

                                                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                                Foreground = Brushes.White,
                                                Items = { Children.Select(child => child.Element) }

                                            }

                                    }
                            }
                        }
                    }
                    : new Grid()
                    {
                        Children = {
                            new TextBlock()
                            {
                                Text = Title,
                                DataContext = Control
                            }
                        }
                    };
                */
            }
        }

    }
}
