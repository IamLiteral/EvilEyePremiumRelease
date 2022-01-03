using Photon.Realtime;

namespace EvilEye.Events
{
    public interface OnSendOPEvent
    {
        bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions);
    }
}
