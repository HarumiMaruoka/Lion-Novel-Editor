using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public abstract class CommandBase
    {
        protected Config _config;
        protected string[] _commandArgs;

        public CommandBase(Config config, string[] commandArgs)
        {
            _config = config;
            _commandArgs = commandArgs;
        }

        public abstract UniTask RunCommand(CancellationToken token = default);
    }
}