using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseControl
{
    public class Max : Toy
    {
        public override void OnManaChanged(float curr, float lastRecorded, float max){ }

        public override void OnDeath()
        {
            LovenseAPI.FunctionWithStrength("AirAuto", 3, this);
        }

        public override void OnRespawn()
        {
            LovenseAPI.FunctionWithStrength("AirAuto", 0, this);
        }

        public override void Stop()
        {
            base.Stop();
            OnRespawn();
        }
    }
}
