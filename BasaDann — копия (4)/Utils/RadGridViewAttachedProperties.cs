using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace BasaDann.Utils
{
    public static class RadGridViewAttachedProperties
    {
        public static readonly DependencyProperty RowDoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("RowDoubleClickCommand", typeof(ICommand), typeof(RadGridViewAttachedProperties), new UIPropertyMetadata(null, OnRowDoubleClickCommandChanged));

        public static ICommand GetRowDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(RowDoubleClickCommandProperty);
        }

        public static void SetRowDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(RowDoubleClickCommandProperty, value);
        }

        private static void OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var obj = sender as DependencyObject;
            if (obj != null)
            {
                var command = GetRowDoubleClickCommand(obj);
                if (command != null)
                {
                    var frameworkElement = e.OriginalSource as FrameworkElement;
                    var dataContext = frameworkElement?.DataContext;
                    if (command.CanExecute(dataContext))
                    {
                        command.Execute(dataContext);
                    }
                }
            }
        }

        private static void OnRowDoubleClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var controlIsTheRightType = d as RadGridView;
            if (controlIsTheRightType == null)
            {
                return;
            }

            controlIsTheRightType.MouseDoubleClick += OnMouseDoubleClick;
        }
    }
}
