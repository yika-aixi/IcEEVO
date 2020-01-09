using UnityEditor;
using UnityEngine;

namespace IcEEVO
{
    public class EEVOSettingWindow : EditorWindow
    {
        [MenuItem("CabinIcarus/IcEEVO/Setting")]
        private static void ShowWindow()
        {
            var window = GetWindow<EEVOSettingWindow>();
            window.titleContent = new GUIContent("EEVO Setting");
            window.Show();
        }

        private const string ConfigKey = "Cueent EEVO Config";
        
        private static EEVOConfig _currentConfig;
        
        private void OnGUI()
        {
//            _currentConfig = EditorGUILayout.ObjectField()
        }
    }
    
}