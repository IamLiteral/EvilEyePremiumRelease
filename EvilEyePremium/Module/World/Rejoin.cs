using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World
{
    class Rejoin : BaseModule
    {
        public Rejoin() : base("Rejoin", "Rejoin the World", Main.Instance.worldButtonGroup, null) { }

        public override void OnEnable()
        {
            VRCFlowManager.prop_VRCFlowManager_0.Method_Public_Void_String_WorldTransitionInfo_Action_1_String_Boolean_0(WorldWrapper.GetLocation());
        }
    }
}
