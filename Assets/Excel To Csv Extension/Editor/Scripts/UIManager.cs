using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// �{�^���Ƌ@�\�����т�����A�e�L�X�g�r���[�ƃe�L�X�g�f�[�^�����т�������������Ă��܂��B
public class UIManager
{
    public void Bind(VisualElement root)
    {
        // �ݒ�̓ǂݍ��݁B
        var settings = Resources.Load<ExcelToCsvSettings>("Excel To Csv Settings");

        if (settings)
        {
            FolderSelector.Initialize(settings.ExcelFolderPath, settings.CsvFolderPath);

            FolderSelector.OnExcelFolderChanged += settings.RecordExcelFolderPath;
            FolderSelector.OnCsvFolderChanged += settings.RecordCsvFolderPath;
        }

        // Get Buttons
        var excelChoiceButton = root.Q<Button>("ExcelFolder-Choice-Button");
        var csvChoiceButton = root.Q<Button>("CsvFolder-Choice-Button");
        var runConvertButton = root.Q<Button>("Run-Convert-Button");

        // Add function to the buttons.
        excelChoiceButton.clicked += FolderSelector.SelectExcelFolder;
        csvChoiceButton.clicked += FolderSelector.SelectCsvFolder;
        runConvertButton.clicked += Converter.Convert;

        // Get folder name labels
        var excelFolderNameView = root.Q<Label>("Excel-Folder-Name");
        var csvFolderNameView = root.Q<Label>("Csv-Folder-Name");

        // Apply a string to the label.
        ApplyFolderNameView(excelFolderNameView, FolderSelector.SelectedExcelPath);
        ApplyFolderNameView(csvFolderNameView, FolderSelector.SelectedCsvPath);

        FolderSelector.OnExcelFolderChanged += changedPath => ApplyFolderNameView(excelFolderNameView, changedPath);
        FolderSelector.OnCsvFolderChanged += changedPath => ApplyFolderNameView(csvFolderNameView, changedPath);

        // Get path labels
        var excelFolderPathView = root.Q<Label>("Excel-Path-View");
        var csvFolderPathView = root.Q<Label>("Csv-Path-View");

        // Apply a string to the label.
        ApplyPathView(excelFolderPathView, FolderSelector.SelectedExcelPath);
        ApplyPathView(csvFolderPathView, FolderSelector.SelectedCsvPath);

        FolderSelector.OnExcelFolderChanged += changedPath => ApplyPathView(excelFolderPathView, changedPath);
        FolderSelector.OnCsvFolderChanged += changedPath => ApplyPathView(csvFolderPathView, changedPath);

        // Get Log Parent
        _logParent = root.Q<ScrollView>("Log-ScrollView");

        // Get Log Clear Button
        var logClearButton = root.Q<Button>("Log-Clear-Button");
        logClearButton.clicked += ClearLog;
    }

    private void ApplyFolderNameView(Label view, string path)
    {
        var prefix = "Folder name: ";
        var folderName = Path.GetFileName(path);

        if (string.IsNullOrEmpty(folderName))
        {
            view.text = prefix + "Unselected folder.";
        }
        else
        {
            view.text = prefix + folderName;
        }
    }

    private void ApplyPathView(Label view, string path)
    {
        var prefix = "Path: ";
        if (string.IsNullOrEmpty(path))
        {
            view.text = prefix + "Unselected folder.";
        }
        else
        {
            view.text = prefix + path;
        }
    }

    private static ScrollView _logParent = null;

    public static void AddLog(string str)
    {
        if (_logParent == null)
        {
            Debug.Log("Log Parent is missing.");
            return;
        }

        var logElement = new Label();
        logElement.text = str;

        _logParent.contentContainer.Insert(0, logElement);
    }

    private void ClearLog()
    {
        _logParent.contentContainer.Clear();
    }
}