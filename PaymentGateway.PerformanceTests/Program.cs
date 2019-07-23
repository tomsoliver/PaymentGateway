using Newtonsoft.Json;
using PaymentGateway.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;

namespace PaymentGateway.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            var stopwatch = new Stopwatch();

            var handler = new HttpClientHandler();
            handler.SslProtocols = SslProtocols.Tls12;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44387/api/payments");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", 
                    Convert.ToBase64String(Encoding.UTF8.GetBytes("tom:oliver")));

                // Test Post
                var request = new PaymentRequest("1234567891234567", DateTime.Now.AddYears(1), 100, "GBP", 123);
                var jsonContent = JsonConvert.SerializeObject(request);

                HttpResponseMessage result = null;
                using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                {
                    stopwatch.Start();
                    result = client.PostAsync("", content).GetAwaiter().GetResult();
                    stopwatch.Stop();

                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    Console.WriteLine(result.StatusCode);
                }

                // Test Get
                stopwatch.Reset();

                stopwatch.Start();
                var result2 = client.GetAsync(result.Headers.Location.PathAndQuery).GetAwaiter().GetResult();
                stopwatch.Stop();

                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                Console.WriteLine(result2.StatusCode);
            }

            Console.ReadLine();
        }
    }
}
