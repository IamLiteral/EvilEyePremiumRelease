using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.Module
{
    abstract class BaseModule
    {
        private static int copyAmount = 0;
        private static QMButtonGroup lastcetagory;

        public string name;
        public bool toggled;
        public bool save;

        string discription;
        QMButtonGroup category;
        bool isToggle;
        Sprite image;

        public BaseModule(string name, string discription, QMButtonGroup category, Sprite image = null, bool isToggle = false, bool save = false)
        {
            this.name = name;
            this.discription = discription;
            this.category = category;
            this.isToggle = isToggle;
            this.save = save;
            this.image = image;
            this.OnUIInit();
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnUIInit()
        {
            category.buttonamount++;
            if (category.buttonamount > 4)
            {
                if (lastcetagory == null)
                {
                    lastcetagory = new QMButtonGroup(category.buttonGroup.transform.parent, category.buttonGroup.name + copyAmount);
                }
                category = lastcetagory;
                copyAmount++;
            }
            else
            {
                lastcetagory = null;
            }

            if (isToggle)
            {
                QMToggleButton qMToggleButton = new QMToggleButton(category.buttonGroup.transform, name, discription, new Action<bool>((bool state) =>
                {
                    this.toggled = state;
                    if (state)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                    Main.Instance.onEventEventArray = Main.Instance.onEventEvents.ToArray();
                    Main.Instance.onPlayerJoinEventArray = Main.Instance.onPlayerJoinEvents.ToArray();
                    Main.Instance.onPlayerLeaveEventArray = Main.Instance.onPlayerLeaveEvents.ToArray();
                    Main.Instance.onRPCEventArray = Main.Instance.onRPCEvents.ToArray();
                    Main.Instance.onSendOPEventArray = Main.Instance.onSendOPEvents.ToArray();
                    Main.Instance.onUdonEventArray = Main.Instance.onUdonEvents.ToArray();
                    Main.Instance.onUpdateEventArray = Main.Instance.onUpdateEvents.ToArray();
                    Main.Instance.OnAssetBundleLoadEventArray = Main.Instance.OnAssetBundleLoadEvents.ToArray();
                    Main.Instance.onSceneLoadedEventArray = Main.Instance.onSceneLoadedEvents.ToArray();
                    Main.Instance.onWorldInitEventArray = Main.Instance.onWorldInitEvents.ToArray();
                }));
                if (save)
                {
                    if (Main.Instance.config.getConfigBool(name))
                    {
                        qMToggleButton.SetToggle(true);
                    }
                }
            }
            else
            {
                new QMSingleButton(category.buttonGroup.transform, name, discription, image, delegate
                {
                    OnEnable();
                });
            }
        }
    }
}
