using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Terraria.ModLoader;

namespace LovenseControl
{
    public class TestToysCommand : ModCommand
    {
        public override string Command => "testtoy";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            foreach (var item in LovenseAPI.toys)
                LovenseAPI.Vibrate(20,item);

            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Start();

            timer.Elapsed += OnTimesUp;
        }

        private void OnTimesUp(object sender, ElapsedEventArgs e)
        {
            foreach (var item in LovenseAPI.toys)
                LovenseAPI.Vibrate(0,item);

        }
    }
}
