namespace Liminal.Editor.TemplateSetup
{
    using UnityEngine;
    using UnityEditor;

    [InitializeOnLoad]
    public class TemplateSetup
        : MonoBehaviour
    {
        public const string IsSetupKey = "TemplateSetupComplete";

        static TemplateSetup()
        {
            if(EditorPrefs.GetBool(IsSetupKey, false))
                return;

            EditorApplication.update += Init;
        }

        private static void Init()
        {
            EditorApplication.update -= Init;
            TemplateSetupWindow.OpenWindow();
        }
    }
}