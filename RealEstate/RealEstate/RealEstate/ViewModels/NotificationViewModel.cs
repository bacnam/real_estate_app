using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        public NotificationViewModel()
        {
            Notifications = new ObservableCollection<NotificationModel>();
        }

        public ObservableCollection<NotificationModel> Notifications { get; }

        public async Task<bool> ReadNofificationAsync(NotificationModel notification)
        {
            bool success = false;
            if (IsBusy)
            {
                return success;
            }
            IsBusy = true;
            try
            {
                var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
                ReadNotificationRequest request = new ReadNotificationRequest() { Id = notification.Id };
                var res = await apiService.PutAsync<ReadNotificationResponse>("accounts/notifications", request.ToJson());
                success = res.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return success;
        }

        public async Task GetNotificationAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
                var res = await apiService.GetAsync<GetNotificationResponse>("accounts/notifications");
                if (res.Success)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Notifications.Clear();

                        var notifications = from n in res.Notifications
                                            select new NotificationModel()
                                            {
                                                Id = n.Id,
                                                Source = n.Source,
                                                IsRead = n.IsRead,
                                                Time = n.Time,
                                                Title = n.Title,
                                                NotificationType = n.NotificationType
                                            };
                        foreach (var notification in notifications)
                        {
                            Notifications.Add(notification);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
