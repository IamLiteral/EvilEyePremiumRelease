using EvilEye.SDK;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace EvilEye.Module.Render
{
    class ObjectESP : BaseModule
    {
        public ObjectESP() : base("Trigger ESP", "ESP on every trigger in world", Main.Instance.rendererUI, null, true, true) { }

        public override void OnEnable()
        {
            for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
            {
                if (WorldWrapper.vrc_Pickups[i].gameObject.activeSelf)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(WorldWrapper.vrc_Pickups[i].GetComponentInChildren<MeshRenderer>(), true);
                }
            }
        }

        public override void OnDisable()
        {
            for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
            {
                if (WorldWrapper.vrc_Pickups[i].gameObject.activeSelf)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(WorldWrapper.vrc_Pickups[i].GetComponentInChildren<MeshRenderer>(), false);
                }
            }
        }

    }
}
