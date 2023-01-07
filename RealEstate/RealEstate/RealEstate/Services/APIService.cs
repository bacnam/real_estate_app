using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RealEstate.Services
{
    public interface IAPIService
    {
        Task<T> GetAsync<T>(string path);
        Task<T> PostAsync<T>(string path, string data);
        Task<T> DeleteAsync<T>(string path);
        Task<T> PutAsync<T>(string path, string data);
    }

    public class APIService : IAPIService
    {
#if DEBUG
        //public static string URL = "http://192.168.1.148:8080/api/";
        public static string URL = "http://10.0.0.238:8000/api/";
#else
        public static string URL = "http://34.87.87.77/api/";
#endif

        public async Task<T> DeleteAsync<T>(string path)
        {
            string token = await Xamarin.Essentials.SecureStorage.GetAsync("token");
            using (HttpClient httpClient = GetHttpClient(token))
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, path);
                    var res = await httpClient.DeleteAsync(url);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("THÔNG BÁO", "Phiên đăng nhập đã hết hạn.", "OK");
                        Xamarin.Essentials.SecureStorage.Remove("token");
                        await (Xamarin.Forms.Application.Current as App).InitAsync(true);
                    }
                    return Activator.CreateInstance<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return Activator.CreateInstance<T>();
                }
            }
        }

        public async Task<T> GetAsync<T>(string path)
        {
            string token = await Xamarin.Essentials.SecureStorage.GetAsync("token");
            using (HttpClient httpClient = GetHttpClient(token))
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, path);
                    var res = await httpClient.GetAsync(url);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("THÔNG BÁO", "Phiên đăng nhập đã hết hạn.", "OK");
                        Xamarin.Essentials.SecureStorage.Remove("token");
                        await (Xamarin.Forms.Application.Current as App).InitAsync(true);
                    }
                    return Activator.CreateInstance<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Activator.CreateInstance<T>();
                }
            }
        }

        public async Task<T> PostAsync<T>(string path, string data)
        {
            string token = await Xamarin.Essentials.SecureStorage.GetAsync("token");
            using (HttpClient httpClient = GetHttpClient(token))
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, path);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    var res = await httpClient.PostAsync(url, content);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("THÔNG BÁO", "Phiên đăng nhập đã hết hạn.", "OK");
                        Xamarin.Essentials.SecureStorage.Remove("token");
                        await (Xamarin.Forms.Application.Current as App).InitAsync(true);
                    }
                    else
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("THÔNG BÁO", "Không thể kết nối tới máy chủ.", "OK");
                    }
                    return Activator.CreateInstance<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Activator.CreateInstance<T>();
                }
            }
        }

        public async Task<T> PutAsync<T>(string path, string data)
        {
            string token = await Xamarin.Essentials.SecureStorage.GetAsync("token");
            using (HttpClient httpClient = GetHttpClient(token))
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, path);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    var res = await httpClient.PutAsync(url, content);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("THÔNG BÁO", "Phiên đăng nhập đã hết hạn.", "OK");
                        Xamarin.Essentials.SecureStorage.Remove("token");
                        await (Xamarin.Forms.Application.Current as App).InitAsync(true);
                    }
                    return Activator.CreateInstance<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Activator.CreateInstance<T>();
                }
            }
        }

        private HttpClient GetHttpClient(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
    }
}
