using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Events
{
    public interface OnSceneLoadedEvent
    {
        void OnSceneWasLoadedEvent(int buildIndex, string sceneName);
    }
}
