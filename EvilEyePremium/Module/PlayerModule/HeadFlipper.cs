using UnityEngine;
using EvilEye.SDK;
using EvilEye.Module;
using EvilEye.Events;
using VRC.DataModel;
using VRC;
using VRC.Core;

namespace EvilEye.Module.PlayerModule
{
    class HeadFlipper : BaseModule
    {

        public HeadFlipper() : base("HeadFlipper", "Fuck your desktop neck", Main.Instance.playerToggleGroup, null, true) { }
        private NeckRange orgin;

        public override void OnEnable()
        {
            orgin = PlayerWrapper.LocalPlayer().GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
            PlayerWrapper.LocalPlayer().GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue,float.MaxValue,0f);
        }

        public override void OnDisable()
        {
            PlayerWrapper.LocalPlayer().GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = orgin;
        }
    }
}
