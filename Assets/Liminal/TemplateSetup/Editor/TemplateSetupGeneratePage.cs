using System;
using System.Linq;
using Liminal.SDK.Core;
using Liminal.SDK.Tools;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Liminal.Editor.TemplateSetup
{
    public class TemplateSetupGeneratePage
        : TemplateSetupPage
    {
        private bool _generating;
        private static bool _generated;
        public override string Name => "Generate Scripts";
        public override bool CanProceed => _generated;

        [DidReloadScripts]
        private static void CheckSetup()
        {
            var baseType = typeof(ExperienceApp);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.FirstOrDefault(x => x.FullName.Contains("Assembly-CSharp,"));

            if (assembly != null)
            {
                var types = assembly.GetTypes();
                _generated = types.Any(t => t != baseType && baseType.IsAssignableFrom(t));
                EditorPrefs.SetInt("GeneratedScripts", _generated ? 1 : 0);
            }
        }

        public override void DrawPage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(16);
            GUILayout.BeginVertical();
            GUILayout.Space(16);
            GUILayout.Label("Setup Experience", "AM MixerHeader2");
            GUILayout.Space(8);
            GUILayout.Label("Now we will need to generate some things to setup your project for use as a Limapp.", new GUIStyle("AM HeaderStyle") { wordWrap = true });

            GUILayout.Space(16);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (!_generating && !_generated)
            {
                if (GUILayout.Button("Setup Project", GUILayout.MaxWidth(200), GUILayout.Height(50)))
                {
                    _generating = true;
                    AppTools.GenerateScripts();
                }
            }
            else if (_generated)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    GUILayout.Button("✓ Project Setup", GUILayout.MaxWidth(200), GUILayout.Height(50));
                }
            }
            else
            {
                GUILayout.Label("Please wait while Unity creates the necessary script files...", new GUIStyle("MeTimeBlockLeft") { wordWrap = true });
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