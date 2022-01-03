using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Events
{
    public interface OnPlayerLeaveEvent
    {
        void PlayerLeave(VRC.Player player);
    }
}
