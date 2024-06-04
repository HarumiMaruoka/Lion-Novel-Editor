using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario
{
    public class CommonActions
    {
        public static async UniTask ChangeCaption(Config config, string[] commandArgs)
        {
            config.Caption.text = commandArgs[0];
        }
    }
}