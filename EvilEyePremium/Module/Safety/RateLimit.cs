using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EvilEye.Events;
using ExitGames.Client.Photon;
using MelonLoader;
using UnityEngine;
using VRC;

namespace EvilEye.Module.Safety
{
    internal class RateLimit : BaseModule, OnEventEvent, OnWorldInitEvent
    {
        Dictionary<int, int> senderAmount = new Dictionary<int, int>();

        public RateLimit() : base("RateLimiter", "RateLimit Photon Events", Main.Instance.safetyPhotonGroup, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
            Main.Instance.onWorldInitEvents.Add(this);
            MelonCoroutines.Start(DelayedUpdate());
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
            Main.Instance.onWorldInitEvents.Remove(this);
        }

        IEnumerator DelayedUpdate()
        {
            while (this.toggled)
            {
                foreach(int key in senderAmount.Keys)
                {
                    if(senderAmount[key] >= 70)
                    {
                        Patches.blacklistedPlayers.Add(key);
                        Task.Run(() =>
                        {
                            Thread.Sleep(30000);
                            Patches.blacklistedPlayers.Remove(key);
                        });
                    }
                    senderAmount[key] = 0;
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield break;
        }

        public bool OnEvent(EventData eventData)
        {
            if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8 || eventData.Code == 33 || eventData.Code == 253 || eventData.Code == 254)
                return true;
            if(!senderAmount.Keys.Contains(eventData.Sender))
                senderAmount.Add(eventData.Sender, 0);

            senderAmount[eventData.Sender] += 1;
            return true;
        }

        public void OnWorldInit()
        {
            senderAmount = new Dictionary<int, int>();
        }
    }
}
