using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK;
using VRC.SDKBase;

namespace EvilEye.Module.Movement
{
    internal class Speed : BaseModule
    {
        public Speed() : base("Speed", "go brrrrrrrrrrrrrrrrrr", Main.Instance.movementGroup, null, true)
        {
        }

        public override void OnEnable()
        {
            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() * 2);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() * 2);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() * 2);
        }

        public override void OnDisable()
        {
            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() / 2);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() / 2);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() / 2);
        }
    }
}
