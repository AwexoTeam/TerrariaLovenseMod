using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LovenseControl
{
    public class SystemHook : ModSystem
    {
        public override void PreSaveAndQuit()
        {
            LovenseAPI.StopAll();
        }
    }
}