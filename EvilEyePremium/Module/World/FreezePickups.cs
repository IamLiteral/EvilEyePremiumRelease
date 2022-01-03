using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK;
using VRC.SDKBase;

namespace EvilEye.Module.World
{
    internal class FreezePickups : BaseModule, OnUpdateEvent
	{
		public FreezePickups() : base("FreezePickups", "No one besides you can use Pickups", Main.Instance.worldToggleGroup,null,true)
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
			for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
			{
				Networking.SetOwner(Networking.LocalPlayer, WorldWrapper.vrc_Pickups[i].gameObject);
			}
		}
	}
}
