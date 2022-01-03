using EvilEye.SDK;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace EvilEye.Module.Render
{
    class TriggerESP : BaseModule
    {
        public TriggerESP() : base("Trigger ESP", "ESP on every trigger in world", Main.Instance.rendererUI, null, true, true) { }

        public override void OnEnable()
        {
            for (int i = 0; i < WorldWrapper.vrc_Triggers.Length; i++)
            {
                if (WorldWrapper.vrc_Triggers[i].gameObject.activeSelf) { 
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(WorldWrapper.vrc_Triggers[i].gameObject.GetComponentInChildren<MeshRenderer>(), true);
                }
            }
        }

        public override void OnDisable()
        {
            for (int i = 0; i < WorldWrapper.vrc_Triggers.Length; i++)
            {
                if (WorldWrapper.vrc_Triggers[i].gameObject.activeSelf)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(WorldWrapper.vrc_Triggers[i].gameObject.GetComponentInChildren<MeshRenderer>(), false);
                }
            }
        }

    }
}
