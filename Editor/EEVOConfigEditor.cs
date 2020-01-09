using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityScript;
using Assembly = System.Reflection.Assembly;

namespace IcEEVO
{
    [CustomEditor(typeof(EEVOConfig))]
    public class EEVOConfigEditor : Editor
    {
        private const string PathKey = "EEVOConfig Path";

        public static EEVOConfig CurrentEEVOConfig;
        private static int _compileErrorCount;
        [InitializeOnLoadMethod]
        static void _init()
        {
            CurrentEEVOConfig = AssetDatabase.LoadAssetAtPath<EEVOConfig>(EditorUserSettings.GetConfigValue(PathKey));

            CompilationPipeline.compilationStarted += o =>
            {
                _compileErrorCount = 0;
                PlayClip(CurrentEEVOConfig.CompileStart_Clip);
            };


            CompilationPipeline.assemblyCompilationFinished += (s, messages) =>
            {
                _compileErrorCount += messages.Count(x=>x.type == CompilerMessageType.Error);
            };
            
            CompilationPipeline.compilationFinished += o =>
            {
                if (_compileErrorCount > 0)
                {
                    PlayClip(CurrentEEVOConfig.CompileCompleteButExistError_Clip);
                }
                else
                {
                    PlayClip(CurrentEEVOConfig.CompileComplete_Clip);
                }
            };
                
            EditorApplication.quitting += () => { PlayClip(CurrentEEVOConfig.CloseUnityEditor_Clip); };
        }

        public static void SetCurrentEEVOConfigAndSave(EEVOConfig config)
        {
            var assetPath = AssetDatabase.GetAssetPath(config);
            
            if (!string.IsNullOrEmpty(assetPath))
            {
                EditorUserSettings.SetConfigValue(PathKey,assetPath);
            }

            CurrentEEVOConfig = config;
        }
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Use"))
            {
                SetCurrentEEVOConfigAndSave((EEVOConfig) target);
            }
            
            base.OnInspectorGUI();
        }
        
        private static MethodInfo _playClipMethod;

        public static void PlayClip(AudioClip clip)
        {
            if (_playClipMethod == null)
            {
                Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
                
                Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");

                _playClipMethod = audioUtilClass.GetMethod(
                    "PlayClip",
                    BindingFlags.Static | BindingFlags.Public,
                    null,
                    new Type[]
                    {
                        typeof(AudioClip),
                        typeof(int),
                        typeof(bool)
                    },
                    null
                );
            }

            if (_playClipMethod != null)
                _playClipMethod.Invoke(
                    null,
                    new object[]
                    {
                        clip, 0, false
                    }
                );
        }
    }
}