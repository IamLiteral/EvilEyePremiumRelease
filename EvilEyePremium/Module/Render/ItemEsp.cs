using UnityEngine;
using VRC.SDKBase;
using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;

namespace EvilEye.Module.Render
{
    class ItemEsp : BaseModule
    {
        private VRC_Pickup[] _pickupStored;
        
        public ItemEsp() : base("Item ESP", "See Items n shit", Main.Instance.rendererButton, null, true, true) { }
        
        public override void OnEnable()
            {
                this._pickupStored = Object.FindObjectsOfType<VRC_Pickup>();
                
            }

            //private override void OnStateChange(bool state)
            //{
            //    VRC_Pickup[] pickupStored = this._pickupStored;
             //   for (int i = 0; i < pickupStored.Length; i++)
             //   {
             //       UnityEngine.Renderer component = pickupStored[i].GetComponent<UnityEngine.Renderer>();
             //       if (component != null)
              //      {
              //          HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(component, state);
                    }
                }
          //  }
      //  }   
   // }
