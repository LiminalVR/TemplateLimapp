﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Liminal.Editor.TemplateSetup
{
    public class TemplateSetupWindow
        : EditorWindow
    {
        [MenuItem("Window/Template Setup")]
        public static void OpenWindow()
        {
            TemplateSetupWindow window = (TemplateSetupWindow)GetWindow(typeof(TemplateSetupWindow), true, "Liminal App Template Setup", true);
            window.ShowUtility();
        }

        private void OnEnable()
        {
            _pages = new List<TemplateSetupPage>()
            {
                new TemplateSetupHomePage(),
                new TemplateSetupNamePage(Application.companyName, Application.productName),
                new TemplateSetupGeneratePage(),
                new TemplateSetupScenePage(),
            };
        }

        private List<TemplateSetupPage> _pages;

        private int _pageIndex;

        private void OnGUI()
        {
            GUILayout.BeginHorizontal("LODBlackBox");
            for (var i = 0; i < _pages.Count; i++)
            {
                if (GUILayout.Button(_pages[i].Name, i == 0 ? "GUIEditor.BreadcrumbLeft" : "GUIEditor.BreadcrumbMid"))
                    _pageIndex = i;
            }

            GUILayout.EndHorizontal();

            GUI.backgroundColor = new Color(0.31f, 0.31f, 0.31f);
            GUILayout.BeginVertical("ProjectBrowserPreviewBg");
            GUI.backgroundColor = Color.white;
            _pages[_pageIndex].DrawPage();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (_pageIndex > 0)
                if (GUILayout.Button("Back", GUILayout.MaxWidth(64)))
                {
                    PreviousPage();
                }
            GUILayout.FlexibleSpace();
            using (new EditorGUI.DisabledScope(!_pages[_pageIndex].CanProceed))
                if (GUILayout.Button(_pageIndex >= _pages.Count - 1 ? "Finish" : "Next", GUILayout.MaxWidth(200)))
                {
                    NextPage();
                }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void PreviousPage()
        {
            --_pageIndex;
        }

        private void NextPage()
        {
            if (_pageIndex >= _pages.Count - 1)
            {
                EditorPrefs.SetBool(TemplateSetup.IsSetupKey, true);
                Close();
            }

            ++_pageIndex;
        }
    }
}