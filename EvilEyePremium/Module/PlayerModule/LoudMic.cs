using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Module;

namespace EvilEye.Module.PlayerModule
{
    class LoudMic : BaseModule
    {
        public LoudMic() : base("LoudMic", "Microphone Go Brrrr", Main.Instance.playerToggleGroup, null, true) { }

        public override void OnEnable()
        {
            USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
        }

        public override void OnDisable()
        {
            USpeaker.field_Internal_Static_Single_1 = 1;
        }
    }
}
