using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace EvilEye.Module.Render
{
    internal class PlayerList : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        QMLable playerList;
        bool firstOpen = true;
        public PlayerList() : base("PlayerList", "PlayerList on the side", Main.Instance.rendererUI, null, true, true)
        {
        }

        public override void OnEnable()
        {
            playerList.lable.SetActive(true);
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            if(firstOpen && this.save && Main.Instance.config.getConfigBool(this.name))
            {
                if (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer").activeSelf)
                    playerList.lable.transform.localPosition = new Vector3(-946f, -58.9001f, 0);
                else
                    playerList.lable.transform.localPosition = new Vector3(-526f, -58.9001f, 0);
            }
            else
            {
                if (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer").activeSelf)
                    playerList.lable.transform.localPosition = new Vector3(-526.6402f, -58.9001f, 0);
                else
                    playerList.lable.transform.localPosition = new Vector3(-106.6402f, -58.9001f, 0);
            }
            
            playerList.text.enableWordWrapping = false;
            playerList.text.fontSizeMin = 25;
            playerList.text.fontSizeMax = 30;
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            playerList.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            Main.Instance.onPlayerJoinEvents.Add(this);
            Main.Instance.onPlayerLeaveEvents.Add(this);
            firstOpen = false;
            MelonCoroutines.Start(OnUpdate());
        }

        public override void OnDisable()
        {
            playerList.lable.SetActive(false);
            Main.Instance.onPlayerJoinEvents.Remove(this);
            Main.Instance.onPlayerLeaveEvents.Remove(this);
        }

        public override void OnUIInit()
        {
            playerList = new QMLable(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left").transform, -439f, -58.9001f, "PlayerList");
            base.OnUIInit();
        }

        public IEnumerator OnUpdate()
        {
            while (this.toggled)
            {
                try
                {
                    string info = "";
                    
                    for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
                    {
                        VRC.Player player = PlayerWrapper.GetAllPlayers()[i];
                        if (player.GetIsMaster())
                            info += "<color=white>[<color=yellow>H</color>]";
                        if (player.IsBot())
                            info += " [<color=black>B</color>]";
                        info += " [" + player.GetPlatform() + "]";
                        info += " [<color=#FFB300>P</color>] " + player.GetPingColord();
                        info += " [<color=#FFB300>F</color>] " + player.GetFramesColord();
                        info += " <color=#" + ColorUtility.ToHtmlStringRGB(player.GetTrustColor()) + ">" + player.GetAPIUser().displayName + "</color></color>\n";
                    }
                    playerList.text.text = info;
                }
                catch { }
                yield return new WaitForSeconds(0.25f);
            }
            yield break;
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            playerList.text.enableWordWrapping = false;
            playerList.text.fontSizeMin = 25;
            playerList.text.fontSizeMax = 30;
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            playerList.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            playerList.text.color = Color.white;
        }

        public void PlayerLeave(VRC.Player player)
        {
            playerList.text.enableWordWrapping = false;
            playerList.text.fontSizeMin = 25;
            playerList.text.fontSizeMax = 30;
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            playerList.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            playerList.text.color = Color.white;
        }
    }
}
