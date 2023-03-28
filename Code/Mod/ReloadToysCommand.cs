using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LovenseControl
{
    public class ReloadToysCommand : ModCommand
    {
        public override string Command => "reloadtoys";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            LovenseAPI.LoadToys();
        }
    }
}
