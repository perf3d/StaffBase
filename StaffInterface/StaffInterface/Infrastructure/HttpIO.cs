using Newtonsoft.Json;
using StaffInterface.Core.Abstractions;
using StaffInterface.Core.Models;
using StaffInterface.Infrastructure.Contracts;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace StaffInterface.Infrastructure
{
    public class HttpIO : IHttpIO, IDisposable
    {
        protected readonly static HttpClient _httpClient = new HttpClient();

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public (HttpResponseMessage response, string error) SendGet(string url)
        {
            string error = string.Empty;
            HttpResponseMessage response = new();
            try
            {
                response = _httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return (response, error);
                }
                else
                {
                    error = $"Ошибка при выполнении запроса: {response.ReasonPhrase}";
                    return (response, error);
                }
            }
            catch (Exception ex)
            {
                _httpClient.Dispose();
                //error = $"Ошибка при выполнении запроса: {ex.Message}";
                MessageBox.Show($"Ошибка при работе с сервером: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                //return (response, error);
            }
            return (response, error);
        }

        public (HttpResponseMessage response, string error) SendPost(string url, HttpContent content)
        {
            string error = string.Empty;
            HttpResponseMessage response = new();
            try
            {
                response = _httpClient.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return (response, error);
                }
                else
                {
                    error = $"Ошибка при выполнении запроса: {response.ReasonPhrase}";
                    return (response, error);
                }
            }
            catch (Exception ex)
            {
                _httpClient.Dispose();
                //error = $"Ошибка при выполнении запроса: {ex.Message}";
                MessageBox.Show($"Ошибка при работе с сервером: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                //return (response, error);
            }
            return (response, error);
        }
    }
}
