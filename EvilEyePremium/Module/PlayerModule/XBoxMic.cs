using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK;
using EvilEye.Module;
using EvilEye.Events;

namespace EvilEye.Module.PlayerModule
{
    class XBoxMic : BaseModule
    {
        public XBoxMic() : base("XboxMic", "1v1 in COD bro", Main.Instance.playerToggleGroup, null, true) { }
          
        

        public override void OnEnable()
        {
            PlayerWrapper.LocalPlayer().GetUspeaker().field_Public_BitRate_0 = BitRate.BitRate_8K;
        }

        public override void OnDisable()
        {

            PlayerWrapper.LocalPlayer().GetUspeaker().field_Public_BitRate_0 = BitRate.BitRate_24K;
        }

    
    }
}
