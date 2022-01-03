using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using Photon.Realtime;

namespace EvilEye.Module.World
{
    internal class MasterLock : BaseModule, OnSendOPEvent
	{
		public MasterLock() : base("MasterLock", "Needs to be Master Client", Main.Instance.worldToggleGroup,null, true)
		{
		}

		public override void OnEnable()
		{
			Main.Instance.onSendOPEvents.Add(this);
		}

		public override void OnDisable()
		{
			Main.Instance.onSendOPEvents.Remove(this);
		}

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
			switch (opCode)
			{
				case 4:
					return false;
				case 5:
					return false;
				case 6:
					return false;
				default:
					return false;
			}
		}
    }
}
