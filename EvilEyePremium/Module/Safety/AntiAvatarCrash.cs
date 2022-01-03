using System;
using System.IO;
using EvilEye.Events;
using EvilEye.SDK;
using UnityEngine;

namespace EvilEye.Module.Safety
{
    internal class AntiAvatarCrash : BaseModule, OnAssetBundleLoadEvent
    {
        string[] blacklistShaders;
        string[] blacklistMesh;
        private int maxAudio;
        private int maxLight;
        private int maxDynamicBonesCollider;
        private int maxPoly;
        private int maxMatirial;
        private int maxCloth;
        private int maxColliders;
        private Shader defaultShader;

        public AntiAvatarCrash() : base("Anti Avatar Crash", "Remove Crashers from Avatars", Main.Instance.safetyAvatarGroup, null, true, true)
        {
            this.maxAudio = Main.Instance.config.getConfigInt("MaxAudioSources", 10);
            this.maxLight = Main.Instance.config.getConfigInt("MaxLightSources", 0);
            this.maxDynamicBonesCollider = Main.Instance.config.getConfigInt("MaxDynamicBoneColliders", 5);
            this.maxMatirial = Main.Instance.config.getConfigInt("MaxMatirials", 20);
            this.maxCloth = Main.Instance.config.getConfigInt("MaxCloth", 1);
            this.maxColliders = Main.Instance.config.getConfigInt("MaxColliders", 0);
            this.maxPoly = Main.Instance.config.getConfigInt("MaxPolys", 200000);
            defaultShader = Shader.Find("VRChat/PC/Toon Lit Cutout");
            blacklistShaders = File.ReadAllLines("EvilEye/BlackList/Avatar/Shader.txt");
            blacklistMesh = File.ReadAllLines("EvilEye/BlackList/Avatar/Mesh.txt");
        }

        public override void OnEnable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Remove(this);
        }

        public bool OnAvatarAssetBundleLoad(GameObject avatar, string avatarID)
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            MeshFilter[] meshFilters = avatar.GetComponentsInChildren<MeshFilter>(true);

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRenderers[i];
                bool destroyed = false;
                if (!skinnedMeshRenderer.sharedMesh.isReadable)
                {
                    UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                    LoggerUtill.Log("[AnitCrash] deleted unreadable Mesh", ConsoleColor.Cyan, true);
                    continue;
                }

                for (int j = 0; j < blacklistMesh.Length; j++)
                {
                    if (skinnedMeshRenderer.name.ToLower().Contains(blacklistMesh[j]))
                    {
                        LoggerUtill.Log("[AnitCrash] deleted blackListed Mesh " + skinnedMeshRenderer.name, ConsoleColor.Cyan, true);
                        UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                int polyCount = 0;
                for (int j = 0; j < skinnedMeshRenderer.sharedMesh.subMeshCount; j++)
                {
                    polyCount += skinnedMeshRenderer.sharedMesh.GetTriangles(j).Length / 3;
                    if (polyCount >= this.maxPoly)
                    {
                        UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                        LoggerUtill.Log("[AnitCrash] deleted Mesh with too many polys", ConsoleColor.Cyan, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                Material[] materials = skinnedMeshRenderer.materials;
                if (materials.Length >= maxMatirial)
                {
                    UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                    LoggerUtill.Log("[AnitCrash] deleted Mesh with " + materials.Length + " materials", ConsoleColor.Cyan, true);
                    continue;
                }

                for (int j = 0; j < materials.Length; j++)
                {
                    Shader shader = materials[j].shader;
                    for (int k = 0; k < blacklistShaders.Length; k++)
                    {
                        if (shader.name.ToLower().Contains(blacklistShaders[k]))
                        {
                            LoggerUtill.Log("[AnitCrash] replaced Shader " + shader.name, ConsoleColor.Cyan, true);
                            shader = defaultShader;
                            continue;
                        }
                    }
                }
            }

            for (int i = 0; i < meshFilters.Length; i++)
            {
                MeshFilter meshFilter = meshFilters[i];
                if (!meshFilter.sharedMesh.isReadable)
                {
                    UnityEngine.Object.DestroyImmediate(meshFilter, true);
                    LoggerUtill.Log("[AnitCrash] deleted unreadable Mesh", ConsoleColor.Cyan, true);
                    continue;
                }

                bool destroyed = false;

                for (int j = 0; j < blacklistMesh.Length; j++)
                {
                    if (meshFilter.name.ToLower().Contains(blacklistMesh[j]))
                    {
                        LoggerUtill.Log("[AnitCrash] deleted blackListed Mesh " + meshFilter.name, ConsoleColor.Cyan, true);
                        UnityEngine.Object.DestroyImmediate(meshFilter, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                int polyCount = 0;
                for (int j = 0; j < meshFilter.sharedMesh.subMeshCount; j++)
                {
                    polyCount += meshFilter.sharedMesh.GetTriangles(j).Length / 3;
                    if (polyCount >= this.maxPoly)
                    {
                        UnityEngine.Object.DestroyImmediate(meshFilter, true);
                        LoggerUtill.Log("[AnitCrash] deleted Mesh with too many polys", ConsoleColor.Cyan, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                MeshRenderer meshRenderer = meshFilter.gameObject.GetComponent<MeshRenderer>();
                Material[] materials = meshRenderer.materials;
                if (materials.Length >= maxMatirial)
                {
                    UnityEngine.Object.DestroyImmediate(meshFilter, true);
                    LoggerUtill.Log("[AnitCrash] deleted Mesh with " + materials.Length + " materials", ConsoleColor.Cyan, true);
                    continue;
                }
                for (int j = 0; j < materials.Length; j++)
                {
                    Shader shader = materials[j].shader;
                    for (int k = 0; k < blacklistShaders.Length; k++)
                    {
                        if (shader.name.ToLower().Contains(blacklistShaders[k]))
                        {
                            LoggerUtill.Log("[AnitCrash] replaced Shader " + shader.name, ConsoleColor.Cyan, true);
                            shader = defaultShader;
                            continue;
                        }
                    }
                }
            }

            AudioSource[] audioSources = avatar.GetComponentsInChildren<AudioSource>();
            if (audioSources.Length >= maxAudio)
            {
                for (int i = 0; i < maxAudio; i++)
                {
                    UnityEngine.Object.DestroyImmediate(audioSources[i].gameObject, true);
                }
                LoggerUtill.Log("[AnitCrash] deleted " + maxAudio + " AudioSources", ConsoleColor.Cyan, true);
            }
            Light[] lights = avatar.GetComponentsInChildren<Light>();
            if (lights.Length >= maxLight)
            {
                for (int i = 0; i < maxLight; i++)
                {
                    UnityEngine.Object.DestroyImmediate(lights[i].gameObject, true);
                }
                LoggerUtill.Log("[AnitCrash] deleted " + maxLight + " Lights", ConsoleColor.Cyan, true);
            }
            Cloth[] cloths = avatar.GetComponentsInChildren<Cloth>();
            if (cloths.Length >= maxCloth)
            {
                for (int i = 0; i < maxCloth; i++)
                {
                    UnityEngine.Object.DestroyImmediate(cloths[i].gameObject, true);
                }
                LoggerUtill.Log("[AnitCrash] deleted " + maxCloth + " Cloth", ConsoleColor.Cyan, true);
            }
            Collider[] collider = avatar.GetComponentsInChildren<Collider>();
            if (collider.Length >= maxColliders)
            {
                for (int i = 0; i < maxColliders; i++)
                {
                    UnityEngine.Object.DestroyImmediate(collider[i].gameObject, true);
                }
                LoggerUtill.Log("[AnitCrash] deleted " + maxColliders + " Colliders", ConsoleColor.Cyan, true);
            }
            DynamicBoneCollider[] dynamicBoneColliders = avatar.GetComponentsInChildren<DynamicBoneCollider>();
            if (dynamicBoneColliders.Length >= maxDynamicBonesCollider)
            {
                for (int i = 0; i < maxDynamicBonesCollider; i++)
                {
                    UnityEngine.Object.DestroyImmediate(dynamicBoneColliders[i].gameObject, true);
                }
                LoggerUtill.Log("[AnitCrash] deleted " + maxDynamicBonesCollider + " DynamicBoneColliders", ConsoleColor.Cyan, true);
            }
            return true;
        }
    }
}
