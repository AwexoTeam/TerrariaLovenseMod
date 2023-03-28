using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LovenseControl
{
    public class GameHook : Mod
    {
        public override void Load()
        {
            base.Load();
            LovenseAPI.LoadToys();
        }

    }
}
