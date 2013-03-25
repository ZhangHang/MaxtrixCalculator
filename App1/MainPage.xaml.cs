using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MaxtrixCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int rowCount = 7;
        const int columnCount = 11;
        public MainPage()
        {
            this.InitializeComponent();
            this.resetButton.Click += (s, e) =>
            {
                output.Text = string.Empty;
                foreach (var item in grids)
                {
                    item.Reset();
                }
            };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CreateColumnAndRows(rootGrid, columnCount, rowCount);
            CreateFlippableBoxItem();
        }

        void CreateColumnAndRows(Grid contianer, int columnCount, int rowCount)
        {
            while ((columnCount--) > 0)
                contianer.ColumnDefinitions.Add(new ColumnDefinition());

            while ((rowCount--) > 0)
                contianer.RowDefinitions.Add(new RowDefinition());
        }

        FlippableBox[,] grids = new FlippableBox[rowCount, columnCount];

        void CreateFlippableBoxItem()
        {
            SolidColorBrush normalBrush = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush flippedBursh = new SolidColorBrush(Windows.UI.Colors.DarkGray);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var item = new FlippableBox(normalBrush, flippedBursh);
                    item.SetValue(Grid.RowProperty, i);
                    item.SetValue(Grid.ColumnProperty, j);

                    item.Seleted += item_Seleted;

                    grids[i, j] = item;
                    rootGrid.Children.Add(item);
                }
            }
        }

        void item_Seleted(int row, int cloumn, bool flipped)
        {
            foreach (var item in grids)
            {
                bool increase = flipped;
                item.Change(row, cloumn, increase);
            }

            output.Text = string.Empty;
            foreach (var item in grids)
            {
                output.Text += item.innerText + ",";
            }
            output.Text = output.Text.Substring(0, output.Text.Length - 1);
        }
    }
}
