using EvilEye.Events;
using EvilEye.Module;
using EvilEye.Module.Exploit;
using EvilEye.Module.Movement;
using EvilEye.Module.PlayerModule;
using EvilEye.Module.Render;
using EvilEye.Module.Safety;
using EvilEye.Module.Settings;
using EvilEye.Module.World;
using EvilEye.Modules.Exploits;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using EvilEye.SDK.PhotonSDK;
using ExitGames.Client.Photon;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.Core;
using EvilEye.Module.Spoofer;

namespace EvilEye
{
    public class Main
    {
        public static Main Instance;
        public Config config = new Config();
        public QuickMenuStuff quickMenuStuff;

        public QMButtonGroup worldButtonGroup;
        public QMButtonGroup worldToggleGroup;
        public QMButtonGroup worldHackGroup;

        public QMButtonGroup playerToggleGroup;
        public QMButtonGroup playerButtonGroup;

        public QMButtonGroup movementGroup;

        public QMButtonGroup exploitEventGroup;
        public QMButtonGroup exploitRpcGroup;
        public QMButtonGroup exploitAvatarGroup;
        public QMButtonGroup exploitLocalGroup;

        public QMButtonGroup spooferGroup;

        public QMButtonGroup safetyPhotonGroup;
        public QMButtonGroup safetyAvatarGroup;

        public QMButtonGroup rendererEsp;
        public QMButtonGroup rendererUI;

        public QMButtonGroup settingsButtons;
        public QMButtonGroup settingsLogger;
        public QMButtonGroup settingsMisc;

        private List<BaseModule> modules = new List<BaseModule>();
        public List<OnPlayerJoinEvent> onPlayerJoinEvents = new List<OnPlayerJoinEvent>();
        public List<OnAssetBundleLoadEvent> OnAssetBundleLoadEvents = new List<OnAssetBundleLoadEvent>();
        public List<OnPlayerLeaveEvent> onPlayerLeaveEvents = new List<OnPlayerLeaveEvent>();
        public List<OnUpdateEvent> onUpdateEvents = new List<OnUpdateEvent>();
        public List<OnUdonEvent> onUdonEvents = new List<OnUdonEvent>();
        public List<OnEventEvent> onEventEvents = new List<OnEventEvent>();
        public List<OnRPCEvent> onRPCEvents = new List<OnRPCEvent>();
        public List<OnSendOPEvent> onSendOPEvents = new List<OnSendOPEvent>();
        public List<OnSceneLoadedEvent> onSceneLoadedEvents = new List<OnSceneLoadedEvent>();
        public List<OnWorldInitEvent> onWorldInitEvents = new List<OnWorldInitEvent>();

        public OnPlayerJoinEvent[] onPlayerJoinEventArray = new OnPlayerJoinEvent[0];
        public OnAssetBundleLoadEvent[] OnAssetBundleLoadEventArray = new OnAssetBundleLoadEvent[0];
        public OnPlayerLeaveEvent[] onPlayerLeaveEventArray = new OnPlayerLeaveEvent[0];
        public OnUpdateEvent[] onUpdateEventArray = new OnUpdateEvent[0];
        public OnUdonEvent[] onUdonEventArray = new OnUdonEvent[0];
        public OnEventEvent[] onEventEventArray = new OnEventEvent[0];
        public OnRPCEvent[] onRPCEventArray = new OnRPCEvent[0];
        public OnSendOPEvent[] onSendOPEventArray = new OnSendOPEvent[0];
        public OnSceneLoadedEvent[] onSceneLoadedEventArray = new OnSceneLoadedEvent[0];
        public OnWorldInitEvent[] onWorldInitEventArray = new OnWorldInitEvent[0];

        public static void OnApplicationStart()
        {
            Main.Instance = new Main();
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            LoggerUtill.DisplayLogo();

            Task.Run(() =>
            {
                Patches.Init();
            });
        }

        public static void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        Process.Start("VRChat.exe", Environment.CommandLine);
                        OnApplicationQuit();
                    }
                }
                if(Input.GetKeyDown(KeyCode.T))
                {
                    PlayerWrapper.Tele2MousePos();
                    LoggerUtill.Log("[OnKeyPress.Alt + T] Damn i broke my back lifting you, good god lose some weight!", ConsoleColor.DarkMagenta);
                }
            }
            
            for (int i = 0; i < Main.Instance.onUpdateEventArray.Length; i++)
                Main.Instance.onUpdateEventArray[i].OnUpdateEvent();
        }

        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            for(int i = 0;  i < Main.Instance.onSceneLoadedEventArray.Length; i++)
                Main.Instance.onSceneLoadedEventArray[i].OnSceneWasLoadedEvent(buildIndex, sceneName);   
        }

        public static void OnUIInit()
        {
            GameObject.Destroy(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners"));

            Main.Instance.quickMenuStuff = new QuickMenuStuff(); 
            QMTab mainTab = new QMTab("EvilEye", "EvilEye", "Watching You", null);

            QMNestedButton worldButton = new QMNestedButton(mainTab.menuTransform, "World");
            QMNestedButton playerButton = new QMNestedButton(mainTab.menuTransform, "Player");
            QMNestedButton movementButton = new QMNestedButton(mainTab.menuTransform, "Movement");
            QMNestedButton exploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits");
            QMNestedButton spoofersButton = new QMNestedButton(mainTab.menuTransform, "Spoofers");
            QMNestedButton safetyButton = new QMNestedButton(mainTab.menuTransform, "Safety");
            QMNestedButton rendererButton = new QMNestedButton(mainTab.menuTransform, "Renderer");
            QMNestedButton botButton = new QMNestedButton(mainTab.menuTransform, "Bot");
            QMNestedButton settingsButton = new QMNestedButton(mainTab.menuTransform, "Settings");

            Main.Instance.movementGroup = new QMButtonGroup(movementButton.menuTransform, "Movement");
            Main.Instance.modules.Add(new Speed());
            Main.Instance.modules.Add(new Fly());

            Main.Instance.playerButtonGroup = new QMButtonGroup(playerButton.menuTransform, "PlayerButton");
            Main.Instance.modules.Add(new AviToID());
            Main.Instance.modules.Add(new CopyUserID());

            Main.Instance.playerToggleGroup = new QMButtonGroup(playerButton.menuTransform, "PlayerToggle");
            Main.Instance.modules.Add(new XBoxMic());
            Main.Instance.modules.Add(new LoudMic());
            Main.Instance.modules.Add(new HeadFlipper());
            Main.Instance.modules.Add(new HideSelf());

            new QMHeader(exploistButton.menuTransform, "Events");
            Main.Instance.exploitEventGroup = new QMButtonGroup(exploistButton.menuTransform, "Event");

            new QMHeader(exploistButton.menuTransform, "Rpcs");
            Main.Instance.exploitRpcGroup = new QMButtonGroup(exploistButton.menuTransform, "Rpc");
            Main.Instance.modules.Add(new EventSpammer());
            Main.Instance.modules.Add(new EmojiLagger());
            Main.Instance.modules.Add(new UdonSpam());
            Main.Instance.modules.Add(new Event9());

            new QMHeader(exploistButton.menuTransform, "Avatars");
            Main.Instance.exploitAvatarGroup = new QMButtonGroup(exploistButton.menuTransform, "Avatar");
            Main.Instance.modules.Add(new AssetBundleCrash());
            Main.Instance.modules.Add(new QuestCrash());

            new QMHeader(exploistButton.menuTransform, "Local");
            Main.Instance.exploitLocalGroup = new QMButtonGroup(exploistButton.menuTransform, "Local");
            Main.Instance.modules.Add(new GhostWalk());
            Main.Instance.modules.Add(new FreezePlayer());
            Main.Instance.modules.Add(new VRCA());
            Main.Instance.modules.Add(new VRCW());


            new QMHeader(safetyButton.menuTransform, "Photon");
            Main.Instance.safetyPhotonGroup = new QMButtonGroup(safetyButton.menuTransform, "PhotonSafety");
            Main.Instance.modules.Add(new RateLimit());
            Main.Instance.modules.Add(new AntiInvalidEvent());
            Main.Instance.modules.Add(new AntiInvalidRpc());
            Main.Instance.modules.Add(new AntiBot());

            new QMHeader(safetyButton.menuTransform, "Avatar");
            Main.Instance.safetyAvatarGroup = new QMButtonGroup(safetyButton.menuTransform, "AvatarSafety");
            Main.Instance.modules.Add(new AntiAvatarCrash());

            Main.Instance.spooferGroup = new QMButtonGroup(spoofersButton.menuTransform, "Spoofer");
            Main.Instance.modules.Add(new PingSpoofer());
            Main.Instance.modules.Add(new FPSSpoofer());

            Main.Instance.rendererEsp = new QMButtonGroup(rendererButton.menuTransform, "Esp");
            Main.Instance.modules.Add(new CapsuleEsp());

            new QMHeader(rendererButton.menuTransform, "UI");
            Main.Instance.rendererUI = new QMButtonGroup(rendererButton.menuTransform, "Ui");
            Main.Instance.modules.Add(new PlayerList());
            Main.Instance.modules.Add(new DebugLog());
            Main.Instance.modules.Add(new CustomNameplates());
            Main.Instance.modules.Add(new ObjectESP());
            Main.Instance.modules.Add(new TriggerESP());

            Main.Instance.worldButtonGroup = new QMButtonGroup(worldButton.menuTransform, "WorldButton");
            Main.Instance.modules.Add(new JoinByID());
            Main.Instance.modules.Add(new Rejoin());
            Main.Instance.modules.Add(new CopyWID());
            Main.Instance.worldToggleGroup = new QMButtonGroup(worldButton.menuTransform, "WorldToggle");
            Main.Instance.modules.Add(new MasterLock());
            Main.Instance.modules.Add(new ItemOrbit());
            Main.Instance.modules.Add(new FreezePickups());

            new QMHeader(worldButton.menuTransform, "WorldHacks");
            Main.Instance.worldHackGroup = new QMButtonGroup(worldButton.menuTransform, "WorldHack");
            new WorldHackManager();

            Main.Instance.settingsButtons = new QMButtonGroup(settingsButton.menuTransform, "Settings");
            Main.Instance.modules.Add(new FPSUnlocker());

            new QMHeader(settingsButton.menuTransform, "Loggers");
            Main.Instance.settingsLogger = new QMButtonGroup(settingsButton.menuTransform, "Loggers");
            Main.Instance.modules.Add(new OPSendLogger());
            Main.Instance.modules.Add(new PlayerLogger());
            Main.Instance.modules.Add(new UdonLogger());
            Main.Instance.modules.Add(new EventLogger());
            Main.Instance.modules.Add(new RpcLogger());
            Main.Instance.modules.Add(new UnityLogger());

            new QMHeader(settingsButton.menuTransform, "Misc");
            Main.Instance.settingsMisc = new QMButtonGroup(settingsButton.menuTransform, "Mics");
            Main.Instance.modules.Add(new ConsoleClear());
            Main.Instance.modules.Add(new QuickRestart());

            new QMHeader(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").transform, "EvilEye");
            QMButtonGroup EvilEyeSelection = new QMButtonGroup(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").transform, "EvilEye");
            
            new QMSingleButton(EvilEyeSelection.buttonGroup.transform, "VRCA", "Download Users VRCA", null, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                WebClient webClient = new WebClient
                {
                    Headers =
                    {
                        "User-Agent: Other"
                    }
                };
                webClient.DownloadFileAsync(new Uri(avatar.assetUrl), "EvilEye/VRCA/" + avatar.name);
                LoggerUtill.Log("Downloaded Selected User VRCA Completed");
            });
            new QMSingleButton(EvilEyeSelection.buttonGroup.transform, "ForceClone", "ForceClone", null, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (avatar.releaseStatus == "public")
                    PlayerWrapper.ChangeAvatar(avatar.id);
            });

            new QMSingleButton(EvilEyeSelection.buttonGroup.transform, "Get UserID", "UserID", null, delegate
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                if (SelectedPlayer.id != "")
                    SDK.Misc.SetClipboard(SelectedPlayer.id);
            });

            new QMSingleButton(EvilEyeSelection.buttonGroup.transform, "AvatarID", "AvatarID", null, delegate
            {
                ApiAvatar SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (SelectedPlayer.id != "")
                    SDK.Misc.SetClipboard(SelectedPlayer.id);
            });

            QMButtonGroup EvilEyeSelection2 = new QMButtonGroup(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").transform, "EvilEye");

            new QMSingleButton(EvilEyeSelection2.buttonGroup.transform, "Teleport", "Teleport to selected user.", null, delegate
            {
             
                    PlayerWrapper.Teleport(PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0));
                    
            });

            LoggerUtill.Log("[UI] Done", ConsoleColor.Green);
            LoggerUtill.LogDebug("<color=green>Started</color>");
        }



        public static void OnGUI()
        {

        }

        public static void OnApplicationQuit()
        {
            foreach (BaseModule module in Main.Instance.modules)
            {
                if (module.save)
                {
                    Main.Instance.config.setConfigBool(module.name, module.toggled);
                }
            }

            Process.GetCurrentProcess().Kill();
        }
       
    }
}
