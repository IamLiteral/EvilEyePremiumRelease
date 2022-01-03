using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRC.Core;

namespace EvilEye.Module.World
{
    class CopyWID : BaseModule
    {
        public CopyWID() : base("Get World ID", "Copy the World + InstanceID", Main.Instance.worldButtonGroup, null, false) { }

        public override void OnEnable()
        {
           if(WorldWrapper.GetLocation() != "")
                Misc.SetClipboard(WorldWrapper.GetLocation());
	        LoggerUtill.Log($"World ID: {WorldWrapper.GetLocation()} copied to clipboard.", ConsoleColor.Green);
            LoggerUtill.LogDebug($"<color=#FFB300>World ID</color>: {WorldWrapper.GetLocation()} copied to clipboard.");
        }
 
	}
}
