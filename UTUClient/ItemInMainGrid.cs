using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace UTUClient
{
    class ItemInMainGrid : Grid
    {
        public string LeftText
        {
            get
            {
                return left.Text;
            }
            set
            {
                left.Text = value;
            }
        }
        public string RightText
        {
            get
            {
                return right.Text;
            }
            set
            {
                right.Text = value;
            }
        }

        TextBlock left;
        TextBlock right;

        public ItemInMainGrid()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            MouseEnter += (e, a) => Background = new SolidColorBrush(Colors.AliceBlue);
            MouseLeave += (e, a) => Background = new SolidColorBrush(Colors.White);

            left = new TextBlock();
            Children.Add(left);

            right = new TextBlock();
            right.HorizontalAlignment = HorizontalAlignment.Right;
            Children.Add(right);
        }

        public void AddOnClickHandler(MouseButtonEventHandler handler)
        {
            MouseUp += handler;
        }
    }
}
