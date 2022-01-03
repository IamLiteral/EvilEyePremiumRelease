using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK;
using UnityEngine;
using VRC.SDKBase;

namespace EvilEye.Module.World
{
    internal class ItemOrbit : BaseModule, OnUpdateEvent
	{
		public ItemOrbit() : base("ItemOrbit", "Makes all Pickups spin arround you", Main.Instance.worldToggleGroup,null, true)
		{
		}

		public override void OnEnable()
		{
			Main.Instance.onUpdateEvents.Add(this);
		}

		public override void OnDisable()
		{
			Main.Instance.onUpdateEvents.Remove(this);
		}

		public void OnUpdateEvent()
		{
			if (this.puppet == null)
			{
				this.puppet = new GameObject();
			}
			this.puppet.transform.position = PlayerWrapper.LocalPlayer().transform.position + new Vector3(0f, 0.2f, 0f);
			this.puppet.transform.Rotate(new Vector3(0f, 360f * Time.time * 1f, 0f));
			for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
			{
				VRC_Pickup vrc_Pickup = WorldWrapper.vrc_Pickups[i];
				if (Networking.GetOwner(vrc_Pickup.gameObject) != Networking.LocalPlayer)
				{
					Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
				}
				vrc_Pickup.transform.position = this.puppet.transform.position + this.puppet.transform.forward * 1f;
				this.puppet.transform.Rotate(new Vector3(0f, (float)(360 / WorldWrapper.vrc_Pickups.Length), 0f));
			}
		}

		private GameObject puppet;
	}
}
