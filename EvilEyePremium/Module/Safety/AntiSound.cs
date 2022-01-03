using EvilEye.Events;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
/*
namespace EvilEye.Module.Safety
{
    class AntiSound : BaseModule, OnAvatarLoadedEvent
    {
        AudioSource[] sources = new AudioSource[] { };
        public AntiSound() : base("Anti Avi Sound", "Removes Sound From Avatar When Spawned", Main.Instance.avatarbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnAvatarLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnAvatarLoadEvents.Remove(this);
        }

        public bool OnAvatarLoad(VRCPlayer player, GameObject __0)
        {
            if (__0 == null) return true;
            sources = __0.GetComponentsInChildren<AudioSource>();
            MelonCoroutines.Start(StopSounds());
            Array.Clear(sources, 0, sources.Length);
            return true;
        }
        private void FindAndStop()
        {
            for (int i = 0; i < sources.Length; i++)
            {
                AudioSource source = sources[i];
                if (source.isPlaying)
                    source.Stop();
            }
        }
        private IEnumerator StopSounds()
        {
            FindAndStop();
            yield return new WaitForSeconds(0.6f);
            FindAndStop();
            yield break;
        }
    }
}
*/