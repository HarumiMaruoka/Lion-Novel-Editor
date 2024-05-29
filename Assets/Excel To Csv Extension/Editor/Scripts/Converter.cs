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

        // ���s����O���v���Z�X�̃p�X
        string exePath = fullPath;

        // �R�}���h���C��������g�ݗ��Ă�
        string arguments = $"\"{FolderSelector.SelectedExcelPath}\" \"{FolderSelector.SelectedCsvPath}\"";

        // �v���Z�X���N������
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

            // �K�v�ɉ����āA�O���v���Z�X����̏o�͂��擾�B
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