using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class BackgroundAction : ICommand
    {
        public BackgroundAction(Config config, string[] commandArgs)
        {

        }

        public async UniTask RunCommand(CancellationToken token = default)
        {

        }
    }
}