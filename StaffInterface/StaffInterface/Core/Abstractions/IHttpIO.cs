using System.Net.Http;

namespace StaffInterface.Core.Abstractions
{
    public interface IHttpIO
    {
        public void Dispose();
        (HttpResponseMessage response, string error) SendGet(string url);
        (HttpResponseMessage response, string error) SendPost(string url, HttpContent content);
    }
}