using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class EndGroup : CommandBase
    {
        public EndGroup(Config config, string[] commandArgs) : base(config, commandArgs)
        {

        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {

        }
    }
}