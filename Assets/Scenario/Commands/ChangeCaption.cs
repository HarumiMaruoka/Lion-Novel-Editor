using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class ChangeCaption : ICommand
    {
        private string _caption;

        public ChangeCaption(Config config, string[] commandArgs)
        {
            _caption = commandArgs[0];
        }

#pragma warning disable 1998
        public async UniTask RunCommand(CancellationToken token = default)
        {

        }
#pragma warning restore 1998
    }
}