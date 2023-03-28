using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LovenseControl
{
    public class LovenseConfig : ModConfig
    {
        public static LovenseConfig config
        { 
            get
            {
                return ModContent.GetInstance<LovenseConfig>();
            }
        }
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue("NILL")] public string name { get; set; }
        [DefaultValue("127.0.0.1")] public string ip { get; set; }

        [Range(0, 99999)]
        [DefaultValue(80)] public int port { get; set; }
        [DefaultValue(1)] public int minIntensity { get; set; }
        [DefaultValue(10)] public int maxIntensity { get; set; }

        public static LovenseConfig GetDefault()
        {
            LovenseConfig config = new LovenseConfig();
            Console.WriteLine(config.name);
            return config;
        }

        public static string GetConfigName()
        {
            LovenseConfig config = new LovenseConfig();

            string name = config.GetType().FullName;

            name = name.Replace(".", "_");
            name += ".json";

            return name;
        }
    }

    public static class GlobalConfig
    {
        public static string name => LovenseConfig.config.name;
        public static string ip => LovenseConfig.config.ip;
        public static int port => LovenseConfig.config.port;
        public static int minIntensity => LovenseConfig.config.minIntensity;
        public static int maxIntensity => LovenseConfig.config.maxIntensity;
    }
}
