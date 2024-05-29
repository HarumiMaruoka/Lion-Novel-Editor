using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class Converter
{
    private static bool _isRunning = false;

    private static string relativePath = "Excel To Csv Extension\\Editor\\ExcelToCsvAll2.exe";
    private static string fullPath = Path.Combine(Application.dataPath.Replace("/", "\\"), relativePath);

    public static void Convert()
    {
        if (_isRunning) { UnityEngine.Debug.Log("Running now..."); return; }

        // 実行する外部プロセスのパス
        string exePath = fullPath;

        // コマンドライン引数を組み立てる
        string arguments = $"\"{FolderSelector.SelectedExcelPath}\" \"{FolderSelector.SelectedCsvPath}\"";

        // プロセスを起動する
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            process.WaitForExit();

            // 必要に応じて、外部プロセスからの出力を取得。
            string output = process.StandardOutput.ReadToEnd();
            UIManager.AddLog(output);
            AssetDatabase.Refresh();
        }
    }

    private static void ProcessExited(object sender, System.EventArgs e)
    {
        _isRunning = false;
    }
}