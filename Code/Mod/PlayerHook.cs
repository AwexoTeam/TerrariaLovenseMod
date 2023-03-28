using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Timers;
using Terraria;
using Terraria.ModLoader;

namespace LovenseControl
{
    public class PlayerHook : ModPlayer
    {
        private string configPath;
        private LovenseConfig playerConfig;

        private float lastRecordedHp = float.MaxValue;
        private float lastRecordedMp = float.MaxValue;

        private int interval = 150;

        private DateTime lastCheck = DateTime.Now;
        private bool isDead;

        public override void OnEnterWorld(Player player)
        {
            base.OnEnterWorld(player);
            Timer checkTimer = new Timer();
            checkTimer.Interval = 150;
            checkTimer.AutoReset = true;
            checkTimer.Elapsed += OnModTick;
            checkTimer.Start();

        }

        public override void PostUpdate()
        {
            base.PostUpdate();
            bool dead = Player.dead;

            if (dead == isDead)
                return;

            foreach (var toy in LovenseAPI.toys)
            {
                if (dead)
                    toy.OnDeath();
                else
                    toy.OnRespawn();
            }

            isDead = dead;
        }

        private void OnModTick(object sender, ElapsedEventArgs e)
        {
            if (LovenseAPI.toys.Count <= 0)
                return;

            
            float currHp = Player.statLife;
            float maxHp = Player.statLifeMax;

            float currMp = Player.statMana;
            float maxMp = Player.statManaMax;

            foreach (var toy in LovenseAPI.toys)
            {
                Helper(toy.OnHealthChanged, currHp, maxHp, ref lastRecordedHp);
                Helper(toy.OnManaChanged, currMp, maxMp, ref lastRecordedMp);
            }
        }

        private void Helper(Action<float,float, float> func, float curr, float max, ref float recorded)
        {
            func(curr, recorded, max);
            recorded = curr;
        }

        public override void PlayerDisconnect(Player player)
        {
            base.PlayerDisconnect(player);
            if (player == Main.LocalPlayer)
                LovenseAPI.StopAll();
        }
    }
}