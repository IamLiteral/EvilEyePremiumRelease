using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye;
using EvilEye.Events;
using EvilEye.Module;
using EvilEye.SDK.ButtonAPI;
using UnityEngine;
using VRC;

namespace EvilEye.Module.Render
{
    internal class CustomNameplates : BaseModule, OnPlayerJoinEvent
    {
        public CustomNameplates() : base("CustomNameplates","Cool Kids Nameplate", Main.Instance.rendererUI,null,true,true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onPlayerJoinEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onPlayerJoinEvents.Remove(this);
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
            nameplate.player = player;
        }
    }
}
