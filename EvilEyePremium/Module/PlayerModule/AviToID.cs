using EvilEye.SDK;

namespace EvilEye.Module.PlayerModule
{
    class AviToID : BaseModule
    {
        public AviToID() : base("Change Avatar By ID", "copy an avatarid into your clipboard then click change. ", Main.Instance.playerButtonGroup, null, false) { }
        public override void OnEnable()
        {        
            if (Misc.GetClipboard().StartsWith("avtr"))
                PlayerWrapper.ChangeAvatar(Misc.GetClipboard());  
        }
       
    }
}
