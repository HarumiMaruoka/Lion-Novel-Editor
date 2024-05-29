using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class ChangeCaption : ICommand
    {
        private Config _config;
        private string _caption;

        public ChangeCaption(Config config, string[] commandArgs)
        {
            _config = config;
            _caption = commandArgs[0];
        }

        public async UniTask RunCommand(CancellationToken token = default)
        {
            _config.Caption.text = _caption;
        }
    }
}