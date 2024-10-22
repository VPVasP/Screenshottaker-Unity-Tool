using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;

//How to use: To use go to Window -->Screenshot Taker UnityEditor.
//You can add the path you desire, I recommend to use Assets/ "Your Folder" and save it in the path you want.
//You can also add a name to the screenshot.
//The screenshot is always being saved as a .png file.
//By pressing the take screenshot button the screenshot is being taken but because asset database can be a bit slow i would recommend to click refresh untill it shows up.
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

    private async void CaptureScreenshot()
    {
        //we save it as a png file and we create a customfile name
        string customFileName = customScreenshotName + "" +  ".png";
        string customFilePath = Path.Combine(customScreenshotPath, customFileName);

        //we check if the directory exists if it doesn't we create one and save the screenshot there
        if (!Directory.Exists(customScreenshotPath))
        {
            Directory.CreateDirectory(customScreenshotPath);
        }
        //don't save a screenshot with the same name
        if (File.Exists(customFilePath))
        {
            Debug.LogWarning("A screenshot with the name " + customFileName + "' already exists. Choose a different name.");
            return;
        }
        ScreenCapture.CaptureScreenshot(customFilePath);
        await Task.Run(() => WaitForFile(customFilePath));
    }
    //checking every few milliseconds to see if the file is created
    private void WaitForFile(string filePath)
    {
        while (!File.Exists(filePath))
        {
            System.Threading.Thread.Sleep(100);
            EditorApplication.delayCall += () => AssetDatabase.Refresh();
            Debug.Log("Screenshot saved");
        }
    }
}





