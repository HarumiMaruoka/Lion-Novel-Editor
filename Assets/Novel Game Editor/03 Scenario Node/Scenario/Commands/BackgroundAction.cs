using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class BackgroundAction : CommandBase
    {
        public BackgroundAction(Config config, string[] commandArgs) : base(config, commandArgs)
        {

        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {

        }
    }
}