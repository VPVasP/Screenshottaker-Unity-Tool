using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ScreenshotTakerUnityEditor : EditorWindow
{
    private string customScreenshotPath = "Assets";
    private string customScreenshotName = "screenshot";

    [MenuItem("Window/Screenshot Taker UnityEditor")]
    public static void ShowWindow()
    {
        EditorWindow.CreateWindow<ScreenshotTakerUnityEditor>("Screenshot Taker UnityEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Screenshot Taker UnityEditor", EditorStyles.boldLabel);

        customScreenshotPath = EditorGUILayout.TextField("Custom Path", customScreenshotPath);
        customScreenshotName = EditorGUILayout.TextField("Custom Name", customScreenshotName);

        if (GUILayout.Button("Take Screenshot"))
        {
            CaptureScreenshot();
        }
    }

    private void CaptureScreenshot()
    {
        string customFileName = customScreenshotName + "" +  ".png";
        string customFilePath = Path.Combine(customScreenshotPath, customFileName);

        
        if (!Directory.Exists(customScreenshotPath))
        {
            Directory.CreateDirectory(customScreenshotPath);
            Debug.Log("Path did not exist but now has been created! Save the screenshot again.");
            AssetDatabase.Refresh();
        }
        else
        {
            ScreenCapture.CaptureScreenshot(customFilePath);
            Debug.Log("The Screenshot with name " + customFileName + " has been saved at " + customFilePath + ",Please be patient for the Asset Database to Refresh.");
            AssetDatabase.Refresh();
        }
    }
}



