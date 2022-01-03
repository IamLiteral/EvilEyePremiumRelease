using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK.PhotonSDK;
using Photon.Realtime;
using UnhollowerBaseLib;

namespace EvilEye.Module.Spoofer
{
    internal class FPSSpoofer : BaseModule, OnSendOPEvent
    {
        byte fps;
        public FPSSpoofer() : base("PingSpoofer", "Spoofes Ping to 69", Main.Instance.spooferGroup, null, true)
        {
            fps = (byte)(short)Main.Instance.config.getConfigInt("FakePing", 69);
        }

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
            if (opCode == 7)
            {
                byte[] movementData = parameters.Cast<Il2CppStructArray<byte>>();
				movementData[71] = fps;
				parameters = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(movementData);
            }

            return true;
        }
    }
}
