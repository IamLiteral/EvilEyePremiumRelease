using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye;
using EvilEye.Events;
using EvilEye.Module;
using EvilEye.SDK;
using ExitGames.Client.Photon;
using UnhollowerBaseLib;
using UnityEngine;

namespace EvilEye.Module.Safety
{
    internal class AntiInvalidEvent : BaseModule, OnEventEvent
    {
        public AntiInvalidEvent() : base("AntiInvalidEvent", "Anti for all the Photon Exploits", Main.Instance.safetyPhotonGroup, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            switch (eventData.Code)
            {
                case 7:
                    if (eventData.CustomData.Cast<Il2CppArrayBase<byte>>().Length >= 300)
                    {
                        return false;
                    }
                    break;
                case 9:
                    if (eventData.Parameters[245].ToString() == null || eventData.Parameters[245].ToString().Length >= 150 || eventData.Parameters[245].ToString().Length <= 9)
                    {
                        return false;
                    }
                    break;
                case 209:
                    return false;
                case 210:
                    return false;
                    
            }
            return true;
        }
    }
}
