using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ScreenshotTakerUnityEditor : EditorWindow
{
    //add Assets/"Your Folder Name" to save create a folder and save it there
    private string customScreenshotPath = "Assets/"; //custom path to save screenshot
    private string customScreenshotName = "screenshot"; //screenshot name

    [MenuItem("Window/Screenshot Taker UnityEditor")]
    public static void ShowWindow()
    {
        //create and show the Screenshot Taker UnityEditor window
        EditorWindow.CreateWindow<ScreenshotTakerUnityEditor>("Screenshot Taker UnityEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Screenshot Taker UnityEditor", EditorStyles.boldLabel);
        //text field for entering a custom path for saving screenshots
        customScreenshotPath = EditorGUILayout.TextField("Custom Path", customScreenshotPath);
        //text field to enter the name of the screenshot
        customScreenshotName = EditorGUILayout.TextField("Custom Name", customScreenshotName);
        //window button
        if (GUILayout.Button("Take Screenshot"))
        {
            CaptureScreenshot();
        }
    }

    private void CaptureScreenshot()
    {
        //we save it as a png file and we create a customfile name
        string customFileName = customScreenshotName + "" +  ".png";
        string customFilePath = Path.Combine(customScreenshotPath, customFileName);

        //we check if the directory exists if it doesn't we create one and save the screenshot there
        if (!Directory.Exists(customScreenshotPath))
        {
            Directory.CreateDirectory(customScreenshotPath);
            ScreenCapture.CaptureScreenshot(customFilePath);
            Debug.Log("Path did not exist but now has been created! Save the screenshot again.");
            Debug.Log("Refresh to see the screenshot");
            AssetDatabase.Refresh();
        }
      
    }
}



