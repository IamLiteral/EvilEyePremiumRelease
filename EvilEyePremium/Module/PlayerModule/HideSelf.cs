using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;
using EvilEye.SDK;
using EvilEye.Module;


namespace EvilEye.Module.PlayerModule
{
    class HideSelf : BaseModule
    {
        private string backupID;
        public HideSelf() : base("Hide Self", "Unloads your Avatar", Main.Instance.playerToggleGroup, null, true) { }
        public override void OnDisable()
        {
            PlayerWrapper.ChangeAvatar(backupID);
            PlayerWrapper.LocalPlayer().SetHide(false);
        }

        public override void OnEnable()
        {
            backupID = PlayerWrapper.LocalVRCPlayer().GetAPIAvatar().id;
            PlayerWrapper.LocalPlayer().SetHide(true);
            
        }
    }
}