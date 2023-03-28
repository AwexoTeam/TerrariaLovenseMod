using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseControl
{
    public class Toy
    {
        public string nickName { get; set; }
        public string fVersion { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public int battery { get; set; }
        public string version { get; set; }
        public int status { get; set; }

        [JsonIgnore] private int lastValue = -1;
        [JsonIgnore] public virtual int healthVibrateId { get { return 0; } }
        [JsonIgnore] public virtual int manaVibrateId { get { return 0; } }

        public virtual void OnHealthChanged(float curr, float lastRecorded, float max)
            => Helper(healthVibrateId, curr, lastRecorded, max);

        public virtual void OnManaChanged(float curr, float lastRecorded, float max)
            => Helper(manaVibrateId, curr, lastRecorded, max);

        private void Helper(int number, float curr, float lastRecorded, float max)
        {
            if (curr >= max && lastValue == 0)
                return;

            if (curr >= max)
            {
                LovenseAPI.Vibrate(0, this);
                lastValue = 0;
                return;
            }

            int num = GetIntensity(curr, max);
            if (num == lastValue)
                return;

            LovenseAPI.Vibrate(num, this, number);
            lastValue = num;
        }

        public virtual void OnRespawn() { }
        public virtual void OnDeath() { }

        public virtual void Stop()
        {
            LovenseAPI.Vibrate(0, this);
        }
        protected int GetIntensity(float curr, float max)
        {

            float procent = 1-(curr / max);

            float minI = GlobalConfig.minIntensity;
            float maxI = GlobalConfig.maxIntensity;

            float intensity = (1 - procent) * minI + procent * maxI;

            return (int)MathF.Floor(intensity);
        }

        public virtual void UpdateByToy(Toy toy)
        {
            this.nickName = toy.nickName;
            this.fVersion = toy.fVersion;
            this.name = toy.name;
            this.id = toy.id;
            this.battery = toy.battery;
            this.version = toy.version;
            this.status = toy.status;
        }
    }
}
