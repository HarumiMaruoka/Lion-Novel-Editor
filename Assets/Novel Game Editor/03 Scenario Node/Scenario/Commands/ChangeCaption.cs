using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class ChangeCaption : CommandBase
    {
        private string _caption;

        public ChangeCaption(Config config, string[] commandArgs) : base(config, commandArgs)
        {
            _config = config;
            _caption = commandArgs[0];
        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {
            if (_config.Caption) _config.Caption.text = _caption;
        }
    }
}