using IL.Terraria;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LovenseControl
{
    public static class LovenseAPI
    {
        public static List<Toy> toys;
        
        public static string GetUri()
        {
            string ip = GlobalConfig.ip;
            int port = GlobalConfig.port;

            string rtn = "https://{0}.lovense.club:{1}/";
            return string.Format(rtn, ip, port);
        }

        public static void LoadToys()
        {
            string responseString = "";
            toys = new List<Toy>();

            if (GlobalConfig.name == "NILL")
                return;

            if (GlobalConfig.ip == "127.0.0.1")
                return;

            using (var client = new WebClient())
            {
                string uri = GetUri();
                uri += "GetToys";

                try
                {
                    responseString = client.DownloadString(uri);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                var rep = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                if(rep.code != "200")
                {
                    Console.WriteLine(responseString);
                    return;
                }

                if(rep.data.Count <= 0)
                {
                    Console.WriteLine("No toys were found!");
                    return;
                }

                List<Toy> readList = rep.data.Values.ToList();

                Assembly asm = Assembly.GetExecutingAssembly();
                var toyTypes = asm.GetTypes()
                    .Where(type => type.Namespace == "LovenseControl")
                    .Where(type => type.IsSubclassOf(typeof(Toy)))
                    .ToList();


                foreach (var toy in readList)
                {
                    Type type = toyTypes.Find(x => x.Name == toy.name);

                    if (type == null)
                    {
                        toys.Add(toy);
                        continue;
                    }

                    Toy specificToy = Activator.CreateInstance(type) as Toy;
                    specificToy.UpdateByToy(toy);
                    toys.Add(specificToy);

                    Console.WriteLine("Added " + specificToy.GetType().Name);
                }
            }
        }

        public static void FunctionWithStrength(string function,int strength, Toy toy)
        {
            if (toys == null)
                return;

            if (toys.Count <= 0)
                return;

            using (var client = new WebClient())
            {
                string uri = GetUri();
                uri += function;
                uri += "?v=" + strength;
                uri += "&t=" + toy.id;

                var responseString = client.DownloadString(uri);
            }
        }

        public static void Vibrate(int strength, Toy toy, int number = -1)
        {
            string function = "Vibrate";
            function += number > 0 ? number : string.Empty;

            FunctionWithStrength(function, strength, toy);
        }

        public static void StopAll()
        {
            foreach (var toy in toys)
            {
                toy.Stop();
            }
        }
    }
}
