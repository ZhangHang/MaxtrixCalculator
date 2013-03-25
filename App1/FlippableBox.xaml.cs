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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MaxtrixCalculator
{
    public sealed partial class FlippableBox : Grid
    {
        private SolidColorBrush normalBackgroundBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        private SolidColorBrush flippedBackgroundBrush = new SolidColorBrush(Windows.UI.Colors.DarkGray);

        public delegate void SeletedEventHandler(int row, int cloumn, bool flipped);
        public event SeletedEventHandler Seleted;

        public FlippableBox()
        {
            this.InitializeComponent();
            this.flipped = false;
            this.Tapped += FlippableBox_Tapped;
            this.innerText = 0;
        }

        public FlippableBox(
            SolidColorBrush normalBg,
            SolidColorBrush flippedBg)
            : this()
        {
            this.normalBackgroundBrush = normalBg;
            this.flippedBackgroundBrush = flippedBg;
            this.Background = this.normalBackgroundBrush;
        }

        public bool flipped
        {
            get;
            private set;
        }

        public int innerText
        {
            get
            {
                return Convert.ToInt32(this.innerTextBlock.Text);
            }
            set
            {
                this.innerTextBlock.Text = value.ToString();
            }
        }

        void FlippableBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Background = flipped ? normalBackgroundBrush : flippedBackgroundBrush;

            if (Seleted != null)
            {
                Seleted((int)this.GetValue(Grid.RowProperty), (int)this.GetValue(Grid.ColumnProperty), !flipped);
            }

            flipped = !flipped;
        }

        public void Change(int sourceRow, int sourceCloumn, bool increase, int expectDistance = 1)
        {
            int cloumn = (int)this.GetValue(Grid.ColumnProperty);
            int row = (int)this.GetValue(Grid.RowProperty);

            int targetDistance = (int)Math.Sqrt(
                Math.Pow((sourceRow - row), 2) +
                Math.Pow((sourceCloumn - cloumn)
                , 2));

            if (targetDistance > expectDistance)
                return;

            if (increase)
            {
                innerText++;
            }
            else
            {
                innerText--;
            }
        }

        public void Reset()
        {
            this.flipped = false;
            this.innerText = 0;
            this.Background = normalBackgroundBrush;
        }
    }
}
