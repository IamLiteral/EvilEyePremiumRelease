using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Windows.Forms;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace EvilEye.Module.World
{
    class JoinByID : BaseModule
    {
        public JoinByID() : base("JoinByID", "Make Sure To Copy A World ID To Your Clipboard Before Clicking", Main.Instance.worldButtonGroup, null) { }

        public override void OnEnable()
        {
            VRCFlowManager.prop_VRCFlowManager_0.Method_Public_Void_String_WorldTransitionInfo_Action_1_String_Boolean_0(Misc.GetClipboard());
        } 
	}
}
