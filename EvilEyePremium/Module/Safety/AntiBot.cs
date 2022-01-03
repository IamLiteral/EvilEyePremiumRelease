using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK;
using ExitGames.Client.Photon;

namespace EvilEye.Module.Safety
{
    internal class AntiBot : BaseModule, OnEventEvent
    {
        public AntiBot() : base("AntiBot", "Block Bot Events", Main.Instance.safetyPhotonGroup, null, true, true)
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
            if (eventData.Sender == -1)
                return true;

            VRC.Player player = PlayerWrapper.GetPlayerByActorID(eventData.Sender);
            if (player != null)
            {
                if (player == PlayerWrapper.LocalPlayer())
                    return true;
                if (player.IsBot())
                    return false;
            }

            return true;
        }
    }
}
