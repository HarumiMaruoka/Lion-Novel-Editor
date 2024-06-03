using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class FadeScreen : CommandBase
    {
        private float _targetValue;
        private float _duration;

        public FadeScreen(Config config, string[] commandArgs) : base(config, commandArgs)
        {
            _targetValue = float.Parse(commandArgs[0]);
            _duration = float.Parse(commandArgs[1]);
        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {
            await _config.FadeImage.FadeAsync(_targetValue, _duration, token);
        }
    }
}