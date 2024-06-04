using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands.Background
{
    public class BackgroundAction : CommandBase
    {
        private Func<Config, string[], UniTask> _action;
        private string[] _args;

        public BackgroundAction(Config config, string[] commandArgs) : base(config, commandArgs)
        {
            var actionName = commandArgs[0].Trim();
            _action = actionName switch
            {
                "Change" => BackgroundExtensions.Change,
                _ => throw new ArgumentOutOfRangeException()
            };

            _args = commandArgs[1..];
        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {
            await _action(_config, _args);
        }
    }
}