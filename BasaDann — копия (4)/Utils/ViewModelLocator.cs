using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.ViewModels;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using WPFNotification.Services;

namespace BasaDann.Utils
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<INotificationDialogService, NotificationDialogService>();
        }

        public MainWindowViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        public static void Cleanup()
        {
            var notificationService = ServiceLocator.Current.GetInstance<INotificationDialogService>();
            notificationService.ClearNotifications();
            App.Current.Shutdown();
        }
    }
}