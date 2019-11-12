using UnityEditor;
using System.Diagnostics;
using System.Collections.Generic;

public class ScriptBatch
{
    [MenuItem("MyTools/Android Build With Postprocess")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = FindEnabledEditorScenes();

        string buildExportName = string.Format("{0}.apk", UnityEngine.Application.productName);


        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/" + buildExportName, BuildTarget.Android, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        //FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");

        // Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = path + buildExportName;
        proc.Start();
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }
}
