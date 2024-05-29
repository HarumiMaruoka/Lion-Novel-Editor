using Cysharp.Threading.Tasks;
using System.Threading;
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
            // Duration秒掛けてテキストを表示する
            for (float t = 0; t < _duration; t += Time.deltaTime)
            {
                // _config.TextBox.text = _text;
                await UniTask.Yield();
            }
        }
    }
}