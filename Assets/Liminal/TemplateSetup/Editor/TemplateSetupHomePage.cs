using UnityEngine;

namespace Liminal.Editor.TemplateSetup
{
    public class TemplateSetupHomePage
        : TemplateSetupPage
    {
        public override bool CanProceed => true;
        public override string Name => "Welcome";

        public override void DrawPage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(16);
            GUILayout.BeginVertical();
            GUILayout.Space(16);
            GUILayout.Label("Welcome to your new Liminal project!", "AM MixerHeader2");
            GUILayout.Space(8);
            GUILayout.Label("Congratulations on starting a new Liminal project. Before you can get started a few things must be set up.", new GUIStyle("AM HeaderStyle") { wordWrap = true });
            GUILayout.EndVertical();
            GUILayout.Space(16);
            GUILayout.EndHorizontal();
            GUILayout.Space(16);
        }
    }
}