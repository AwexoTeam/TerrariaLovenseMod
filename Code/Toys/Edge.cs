using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanizer.On;

namespace LovenseControl
{
    public class Edge : Toy
    {
        private float lastValueHp = -1;
        private float lastValueMp = -1;

        public override void OnHealthChanged(float curr, float lastRecorded, float max)
        {
            if (curr >= max && lastValueHp == 0)
                return;

            if (curr >= max)
            {
                LovenseAPI.Vibrate(0, this,1);
                lastValueHp = 0;
                return;
            }

            int num = GetIntensity(curr, max);
            if (num == lastValueHp)
                return;

            LovenseAPI.Vibrate(num, this, 1);
            lastValueHp = num;
        }

        public override void OnManaChanged(float curr, float lastRecorded, float max)
        {
            if (curr >= max && lastValueMp == 0)
                return;

            if (curr >= max)
            {
                LovenseAPI.Vibrate(0, this,2);
                lastValueMp = 0;
                return;
            }

            int num = GetIntensity(curr, max);
            if (num == lastValueMp)
                return;

            LovenseAPI.Vibrate(num, this, 2);
            lastValueMp = num;
        }

        private void Helper(int number, float curr, float lastRecorded, float max)
        {
            
        }
    }
}
