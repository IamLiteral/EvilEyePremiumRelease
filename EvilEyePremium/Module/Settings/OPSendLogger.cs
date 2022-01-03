using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using EvilEye.SDK.PhotonSDK;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class OPSendLogger : BaseModule, OnSendOPEvent
    {
        public OPSendLogger() : base("OPSendLogger", "Logs Photon Events Send by you", Main.Instance.settingsLogger, null, true, true)
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
            LoggerUtill.Log($"[OPSendLog] {opCode} {JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(parameters), Formatting.Indented)}", ConsoleColor.White, true);
            return true;
        }
    }
}
