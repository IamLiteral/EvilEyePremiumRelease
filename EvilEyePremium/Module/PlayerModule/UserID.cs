using EvilEye.SDK;
using System;

namespace EvilEye.Module.PlayerModule
{
    class CopyUserID : BaseModule
    {
        public CopyUserID() : base("Get User ID", "Copy the UserID to clipboard", Main.Instance.playerButtonGroup, null) { }

        public override void OnEnable()
        {
            if (PlayerWrapper.LocalPlayer().GetAPIUser().id != "")
                Misc.SetClipboard(PlayerWrapper.LocalPlayer().GetAPIUser().id);
            LoggerUtill.Log("User ID: " + PlayerWrapper.LocalPlayer().GetAPIUser().id + " Copied to clipboard.", ConsoleColor.Green);
        }

    }
}
