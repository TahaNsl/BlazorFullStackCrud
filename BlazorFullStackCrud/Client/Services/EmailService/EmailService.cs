using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;

namespace BlazorFullStackCrud.Client.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private HttpClient _httpClient;
        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Sendmail(string email)
        {
            await _httpClient.PostAsJsonAsync("email/Sendmail", email);
        }
    }
}