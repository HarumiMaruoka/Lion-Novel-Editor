using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class BeginGroup : ICommand
    {
        public BeginGroup(Config config, string[] commandArgs)
        {

        }

        public async UniTask RunCommand(CancellationToken token = default)
        {

        }
    }
}