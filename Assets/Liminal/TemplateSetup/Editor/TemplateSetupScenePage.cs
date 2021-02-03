using Liminal.SDK.Tools;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Liminal.Editor.TemplateSetup
{
    public class TemplateSetupScenePage
        : TemplateSetupPage
    {
        private bool _generating;
        private bool _generated;
        public override bool CanProceed => _generated;
        public override string Name => "Setup Scene";

        public override void DrawPage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(16);
            GUILayout.BeginVertical();
            GUILayout.Space(16);
            GUILayout.Label("Setup Experience", "AM MixerHeader2");
            GUILayout.Space(8);
            GUILayout.Label("Finally we will need to setup your scene for use as a Limapp.", new GUIStyle("AM HeaderStyle") { wordWrap = true });

            GUILayout.Space(16);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            _generated = GameObject.Find("[ExperienceApp]") != null;

            if (!_generating && !_generated)
            {
                if (GUILayout.Button("Setup Scene", GUILayout.MaxWidth(200), GUILayout.Height(50)))
                {
                    _generating = true;
                    var scene = SceneManager.GetActiveScene();
                    EditorSceneManager.MarkSceneDirty(scene);
                    AppTools.SetupAppScene();
                }
            }
            else if (_generated)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    GUILayout.Button("✓ Scene Setup", GUILayout.MaxWidth(200), GUILayout.Height(50));
                }
            }
            else
            {
                GUILayout.Label("Please wait while Unity sets up your scene...", new GUIStyle("MeTimeBlockLeft") { wordWrap = true });
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(16);
            GUILayout.EndHorizontal();
            GUILayout.Space(16);
        }
    }
}