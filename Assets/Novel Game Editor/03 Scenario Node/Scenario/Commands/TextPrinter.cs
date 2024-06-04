using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class TextPrinter
    {
        public static async UniTask Print(Config config, string[] commandArgs)
        {
            string text = commandArgs[0];
            float duration = float.Parse(commandArgs[1]);

            bool isSkipRequested = false;

            try
            {
                // Duration秒掛けて1文字ずつテキストを表示する。
                var showed = "";
                for (int i = 0; i < text.Length; i++)
                {
                    showed += text[i];
                    if (!config.TextBox) return;
                    config.TextBox.text = showed;
                    for (float t = 0; t < duration / text.Length; t += Time.deltaTime)
                    {
                        await UniTask.Yield(config.TextBox.GetCancellationTokenOnDestroy());
                        // クリックしたら即座に全文表示
                        if (Input.GetMouseButtonDown(0))
                        {
                            isSkipRequested = true;
                            break;
                        }
                    }

                    if (isSkipRequested) break;
                }

                await UniTask.Yield(config.TextBox.GetCancellationTokenOnDestroy());
                config.TextBox.text = text;

                // クリック待ち
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: config.TextBox.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}