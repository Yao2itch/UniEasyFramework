using System.Security.Policy;
using UnityEditor;
using UnityEngine;

namespace EasyFrameworkEditor
{
    public class EasyframeworkWnd : EditorWindow
    {
        private static EditorWindow _editorWnd;

        private bool _isFoldout = false;
        
        private static string _defaultConfigPath;
        private static string _defaultConfigName;
        
        [MenuItem("EasyFramework/Editor")]
        static void CreateEasyframeworkWnd()
        {
            _editorWnd = EditorWindow.GetWindow(typeof(EasyframeworkWnd));
        }

        private void SetDefaultConfig()
        {
            if ( string.IsNullOrEmpty(_defaultConfigPath) )
            {
                _defaultConfigPath = Application.streamingAssetsPath + "/easyframework";
            }

            if ( string.IsNullOrEmpty(_defaultConfigName))
            {
                _defaultConfigName = "moduleConfig.json";
            }
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            
            DrawConifgInfo();
            
            GUILayout.Label( " * 组件 * ", GUILayout.Width(Screen.width) , GUILayout.Height(14) );
            
            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "组件列表");
            if ( _isFoldout )
            {
                EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(Screen.width),
                    GUILayout.Height(14));

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextField("测试01", GUILayout.Width(Screen.width / 4), GUILayout.Height(14));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button(" Add New Component ");
            GUILayout.Button(" Remove Component ");
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }

        private void DrawConifgInfo()
        {
            SetDefaultConfig();
            
            EditorGUILayout.BeginVertical();
             EditorGUILayout.BeginHorizontal();
              GUILayout.Label("默认组件配置路径:", GUILayout.Width(Screen.width / 2), GUILayout.Height(14) );
                _defaultConfigPath = GUILayout.TextField(_defaultConfigPath, GUILayout.Width(Screen.width / 2), GUILayout.Height(14));
             EditorGUILayout.EndHorizontal();
             EditorGUILayout.BeginHorizontal();
              GUILayout.Label("默认组件配置名称:", GUILayout.Width(Screen.width / 2), GUILayout.Height(14) );
               _defaultConfigName = GUILayout.TextField(_defaultConfigName, GUILayout.Width(Screen.width / 2), GUILayout.Height(14));
             EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}