using ExitGames.Client.Photon;
using Il2CppSystem.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.SDK.PhotonSDK
{
	static class PhotonExtensions
	{
		public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			Il2CppSystem.Object Object = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
			OpRaiseEvent(code, Object, RaiseEventOptions, sendOptions);
		}
		public static Dictionary<int, Player> GetAllPhotonPlayers()
		{
			return VRC.Player.prop_Player_0.prop_Player_1.prop_Room_0.prop_Dictionary_2_Int32_Player_0;
		}


		public static void OpRaiseEvent(byte code, Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		   => PhotonNetwork.Method_Private_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0
			(code,
			 customObject,
			 RaiseEventOptions,
			 sendOptions);
	}
}