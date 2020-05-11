using UnityEditor;
using UnityEngine;

namespace EasyFrameworkEditor
{
    public class EasyframeworkWnd : EditorWindow
    {
        private static EditorWindow _editorWnd;

        private bool _isFoldout = false;
        
        [MenuItem("EasyFramework/Editor")]
        static void CreateEasyframeworkWnd()
        {
            _editorWnd = EditorWindow.GetWindow(typeof(EasyframeworkWnd));
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label( " * 组件 * ", GUILayout.Width(Screen.width) , GUILayout.Height(14) );
            
            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "组件列表");
            if ( _isFoldout )
            {
                EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(Screen.width),
                    GUILayout.Height(Screen.height));

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextField("测试01", GUILayout.Width(Screen.width / 4), GUILayout.Height(14));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button(" Add New Component ");
            GUILayout.Button(" Remove Component ");
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }
    }
}