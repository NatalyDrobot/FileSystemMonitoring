using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FileSystemWatcherConsole
{
    [JsonObject(MemberSerialization.OptIn)]
    struct ListEvent
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("TypeObj")]
        public string TypeObj { get; set; }

        [JsonProperty("TypeEvent")]
        public string TypeEvent { get; set; }

        [JsonProperty("DateEvent")]
        public DateTime DateEvent { get; set; }
    }

    static class Events
    {
        public static void GetEventForDate(string date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26586/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/event/" + date).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonEvents = response.Content.ReadAsStringAsync().Result;

                    jsonEvents = "{ \"json\" : [ { \"events\" : " + jsonEvents + " } ] }";
                    //Console.WriteLine(jsonEvents);

                    Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(jsonEvents);
                    ListEvent[] events = JsonConvert.DeserializeObject<ListEvent[]>(obj["json"][0]["events"].ToString());

                    Console.WriteLine("События за " + date);
                    Console.WriteLine(new string('-', 30));

                    foreach (var item in events)
                    {
                        Console.WriteLine("Path: {0}", item.Path);
                        Console.WriteLine("TypeObj: {0}", item.TypeObj);
                        Console.WriteLine("TypeEvent: {0}", item.TypeEvent);
                        Console.WriteLine("DateEvent: {0}", item.DateEvent.ToString("yyyy-MM-dd hh:mm:ss"));
                        Console.WriteLine(new string('-', 30));
                    }
                }
                else
                {
                    Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }

                Console.Read();
            }
        }
    }
}
