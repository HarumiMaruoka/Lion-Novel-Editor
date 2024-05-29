﻿using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class PrintText : ICommand
    {
        private Config _config;
        private string _text;
        private float _duration;

        public PrintText(Config config, string[] commandArgs)
        {
            _config = config;
            _text = commandArgs[0];
            _duration = float.Parse(commandArgs[1]);
        }

        public async UniTask RunCommand(CancellationToken token = default)
        {
            // Duration秒掛けて1文字ずつテキストを表示する。
            var text = "";
            for (int i = 0; i < _text.Length; i++)
            {
                text += _text[i];
                _config.TextBox.text = text;
                for (float t = 0; t < _duration / _text.Length; t += Time.deltaTime)
                {
                    await UniTask.Yield(token);
                }

                // クリックしたら即座に全文表示
                if (Input.GetMouseButtonDown(0)) break;
            }

            await UniTask.Yield(token);
            _config.TextBox.text = _text;

            // クリック待ち
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);
        }
    }
}