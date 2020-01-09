using System;
using UnityEngine;

namespace IcEEVO
{
    [CreateAssetMenu(fileName = "New EEVO Config", menuName = "CabinIcarus/IcEEVO", order = 0)]
    public class EEVOConfig : ScriptableObject
    {
        [ShowLabel("Open Unity Editor")]
        public AudioClip OpenUnityEditor_Clip;
        
        [ShowLabel("Unity Editor Get Focus")]
        public AudioClip UnityEditorGetFocus_Clip;
        
        [ShowLabel("Close Unity Editor")]
        public AudioClip CloseUnityEditor_Clip;
        
        [ShowLabel("Open Code IDE")]
        public AudioClip OpenCodeIDE_Clip;
        
        [ShowLabel("Compile Start")]
        public AudioClip CompileStart_Clip;
        
        [ShowLabel("Compile Complete")]
        public AudioClip CompileComplete_Clip;
        
        [ShowLabel("Compile Error","Compile Complete but exist error")]
        public AudioClip CompileCompleteButExistError_Clip;
        
        [ShowLabel("Build Start")]
        public AudioClip BuildStart_Clip;

        [ShowLabel("Build Complete")]
        public AudioClip BuildComplete_Clip;
        
        [ShowLabel("Build Error","Build Complete but exist error")]
        public AudioClip BuildCompleteButExistError_Clip;
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ShowLabel : Attribute
    {
        public GUIContent Label { get; }

        private ShowLabel(GUIContent guiContent)
        {
            Label = guiContent;
        }

        public ShowLabel(string label):this(new GUIContent(label))
        {
        }
        
        public ShowLabel(string label,string tooltip):this(new GUIContent(label,tooltip))
        {
        }
    }
}