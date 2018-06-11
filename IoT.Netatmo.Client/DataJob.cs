using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;

namespace IoT.Netatmo.Client
{
    public class DataJob : IJob
    {
        private static TokenResponse Token { get; set; }
        private static readonly int DeviceIdentifier = int.Parse(Program.Configuration["deviceIdentifier"]); 

        public async Task Execute(IJobExecutionContext context)
        {
            if (Token == null)
                await RecoverToken();

            if (Token != null)
            {
                if (Token.ExpirationTime <= DateTime.Now)
                    await RefreshToken();

                var parameters = new Dictionary<string, string>
                {
                    {"access_token", Token.AccessToken},
                    {"", Program.Configuration["deviceId"]}
                };

                var urlData = Program.Configuration["urlData"];
                var client = new HttpClient();
                var respData = await client.PostAsync(urlData, new FormUrlEncodedContent(parameters));

                var jsonData = await respData.Content.ReadAsStringAsync();
                var dataResp = JsonConvert.DeserializeObject<DataResponse>(jsonData);

                Console.WriteLine("Data captured!");

                foreach (var device in dataResp.Data.Devices)
                {
                    Console.WriteLine($"\tDevice ID: {device.Id}");
                    Console.WriteLine($"\tTemperature: {device.DashboardData.Temperature}");
                    Console.WriteLine($"\tHumidity: {device.DashboardData.Humidity}");
                    Console.WriteLine($"\tCO2: {device.DashboardData.Co2}");
                    Console.WriteLine($"\tDate: {device.DashboardData.TimeUtc:g}");

                    await PostData(device.DashboardData);
                }
            }
            else
                Console.WriteLine("ERROR: Token couldn't be recovered!");
        }

        private static async Task PostData(Dashboard data)
        {
            var request = new JsonRequest
            {
                Value = data.Temperature,
                SourceId = DeviceIdentifier,
                SourceDescription = "Temperature Data",
                DataType = "float",
                Direction = "INPUT"
            };

            var json = JsonConvert.SerializeObject(request);
            var urlAlmeria = Program.Configuration["urlAlmeria"];
            var client = new HttpClient();
            await client.PostAsync(urlAlmeria, new StringContent(json, Encoding.UTF8, "application/json"));

            request.Value = data.Humidity;
            request.SourceDescription = "Humidity Data";
            request.DataType = "int";
            json = JsonConvert.SerializeObject(request);
            await client.PostAsync(urlAlmeria, new StringContent(json, Encoding.UTF8, "application/json"));

            request.Value = data.Co2;
            request.SourceDescription = "CO2 Data";
            json = JsonConvert.SerializeObject(request);
            await client.PostAsync(urlAlmeria, new StringContent(json, Encoding.UTF8, "application/json"));
            
        }

        private static async Task RecoverToken()
        {
            var values = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", Program.Configuration["username"]},
                {"password", Program.Configuration["password"]},
                {"client_id", Program.Configuration["clientId"]},
                {"client_secret", Program.Configuration["clientSecret"]},
                {"score", "read_station"},
            };

            var client = new HttpClient();
            var urlToken = Program.Configuration["urlToken"];
            var respToken = await client.PostAsync(urlToken, new FormUrlEncodedContent(values));

            var jsonResp = await respToken.Content.ReadAsStringAsync();
            Token = JsonConvert.DeserializeObject<TokenResponse>(jsonResp);
            Token.ExpirationTime = DateTime.Now.AddSeconds(Token.ExpiresIn);

            Console.WriteLine("Token has been recovered!");
        }

        private static async Task RefreshToken()
        {
            var values = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"refresh_token", Token.RefreshToken},
                {"client_id", Program.Configuration["clientId"]},
                {"client_secret", Program.Configuration["clientSecret"]}
            };

            var client = new HttpClient();
            var urlToken = Program.Configuration["urlToken"];
            var respToken = await client.PostAsync(urlToken, new FormUrlEncodedContent(values));

            var jsonResp = await respToken.Content.ReadAsStringAsync();
            Token = JsonConvert.DeserializeObject<TokenResponse>(jsonResp);
            Token.ExpirationTime = DateTime.Now.AddSeconds(Token.ExpiresIn);

            Console.WriteLine("Token has been refreshed!");
        }
    }
}