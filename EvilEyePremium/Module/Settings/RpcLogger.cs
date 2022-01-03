using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.SDKBase;

namespace EvilEye.Module.Settings
{
    class RpcLogger : BaseModule, OnRPCEvent
    {
        public RpcLogger() : base("RPCLogger", "Logs RPCs", Main.Instance.settingsLogger, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onRPCEvents.Remove(this);
        }

        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            string output = "[RPC] ";

            if (sender != null)
            {
                output += sender.GetAPIUser().displayName + " sended ";
            }
            else
            {
                output += " INVISABLE sended ";
            }

            output += vrcBroadcastType + " ";

            output += vrcEvent.Name + " ";

            output += vrcEvent.EventType + " ";

            if (vrcEvent.ParameterObject != null)
                output += vrcEvent.ParameterObject.name + " ";

            output += vrcEvent.ParameterBool + " ";

            output += vrcEvent.ParameterBoolOp + " ";

            output += vrcEvent.ParameterFloat + " ";

            output += vrcEvent.ParameterInt + " ";

            output += vrcEvent.ParameterString + " ";

            if (vrcEvent.ParameterObjects != null)
            {
                for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++)
                {
                    output += vrcEvent.ParameterObjects[i].name + " ";
                }
            }

            try
            {
                var objects = Networking.DecodeParameters(vrcEvent.ParameterBytes);
                for (int i = 0; i < objects.Length; i++)
                {
                    output += Il2CppSystem.Convert.ToString(objects[i]) + " ";
                }
            }
            catch
            {
                for (int i = 0; i < vrcEvent.ParameterBytes.Length; i++)
                {
                    output += vrcEvent.ParameterBytes[i] + " ";
                }
            }
            LoggerUtill.Log(output, ConsoleColor.Cyan, true);
            return true;
        }
    }
}
