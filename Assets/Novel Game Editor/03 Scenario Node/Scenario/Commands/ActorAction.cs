using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class ActorAction : ICommand
    {
        public ActorAction(Config config, string[] commandArgs)
        {

        }

        public async UniTask RunCommand(CancellationToken token = default)
        {

        }
    }
}