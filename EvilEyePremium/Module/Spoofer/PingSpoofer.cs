using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK.PhotonSDK;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnhollowerBaseLib;

namespace EvilEye.Module.Spoofer
{
    internal class PingSpoofer : BaseModule, OnSendOPEvent
    {
        byte[] ping;
        public PingSpoofer() : base("PingSpoofer","Spoofes Ping to 69", Main.Instance.spooferGroup, null, true)
        {
            ping = BitConverter.GetBytes(Main.Instance.config.getConfigInt("FakePing",69));
        }

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
            if (opCode == 7)
            {
                byte[] movementData = parameters.Cast<Il2CppStructArray<byte>>();
                Buffer.BlockCopy(ping, 0, movementData, 68, 2);
                parameters = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(movementData);
            }

            return true;
        }
    }
}
