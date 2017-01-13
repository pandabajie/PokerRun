using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokerRun
{
    class UIManager: DependencyObject
    {
        public static int GetCartValue(DependencyObject obj)
        {
            return (int)obj.GetValue(CartValueProperty);
        }

        public static void SetCartValue(DependencyObject obj, int value)
        {
            obj.SetValue(CartValueProperty, value);
        }

        public static readonly DependencyProperty CartValueProperty =
            DependencyProperty.RegisterAttached("CartValue", typeof(int), typeof(UIManager), new PropertyMetadata(0));


        public static int GetCartFlag(DependencyObject obj)
        {
            return (int)obj.GetValue(CartFlagProperty);
        }

        public static void SetCartFlag(DependencyObject obj, int value)
        {
            obj.SetValue(CartFlagProperty, value);
        }
        public static readonly DependencyProperty CartFlagProperty =
            DependencyProperty.RegisterAttached("CartFlag", typeof(int), typeof(UIManager), new PropertyMetadata(0));
    }
}
