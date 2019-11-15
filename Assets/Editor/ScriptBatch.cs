using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ScriptBatch : ScriptableObject
{
    private static string exportPath = string.Empty;

    [MenuItem("MyTools/Android Build With Postprocess")]
    public static void BuildGame()
    {
        exportPath = GetArg("-exportPath");
        if(string.IsNullOrEmpty(exportPath))
        {
            exportPath = Path.Combine(Environment.CurrentDirectory, "Build");
        }

        DirectoryInfo dirInfo = new DirectoryInfo(exportPath);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        string buildFileName = string.Format("{0}-v{1}-{2}-{3}.apk", Application.productName, Application.version, GetToDayString(), GetBuildOrderOfDay());
        string[] levels = FindEnabledEditorScenes();
        BuildReport report = BuildPipeline.BuildPlayer(levels, exportPath + "/" + buildFileName, BuildTarget.Android, BuildOptions.None);
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

    private static int GetBuildOrderOfDay()
    {
        DirectoryInfo di = new DirectoryInfo(exportPath);
        FileInfo[] fis = di.GetFiles("*.apk");
        string searchPattern = GetToDayString();
        int orderCount = 1;
        foreach (FileInfo fi in fis)
        {
            if (fi.Name.Contains(searchPattern))
            {
                orderCount++;
            }
        }
        return orderCount;
    }

    private static string GetToDayString()
    {
        DateTime today = DateTime.Now;
        return string.Format("{0:yyMMdd}", today);
    }

    private static string GetArg(string name)
    {
        var args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}
