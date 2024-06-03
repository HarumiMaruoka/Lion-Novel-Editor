using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    [Serializable]
    public class CommandRunner
    {
        [SerializeField]
        private TextAsset _commandData;

        private NovelGameController _controller;
        private CommandBase[] _commands;

        public bool IsFinished { get; private set; } = false;

        public void Initialize(NovelGameController controller)
        {
            _controller = controller;
            _commands = CommandLoader.LoadSheet(_commandData.text, _controller.Config);
        }

        public void Play(CancellationToken token = default)
        {
            IsFinished = false;
            var executable = MakeExecutable(_commands);
            RunCommands(executable, token);
        }

        public async void RunCommands(List<List<CommandBase>> executable, CancellationToken token)
        {
            for (int i = 0; i < executable.Count; i++)
            {
                UniTask[] tasks = new UniTask[executable[i].Count];
                for (var j = 0; j < executable[i].Count; j++)
                {
                    tasks[j] = executable[i][j].RunCommand(token);
                }
                await UniTask.WhenAll(tasks);
                if (token.IsCancellationRequested) return;
            }

            IsFinished = true;
        }

        private List<List<CommandBase>> MakeExecutable(CommandBase[] commands)
        {
            bool groupFlag = false;
            List<List<CommandBase>> result = new List<List<CommandBase>>();
            List<CommandBase> group = null;

            group = new List<CommandBase>();
            result.Add(group);

            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i];

                if (command is BeginGroup)
                {
                    group = new List<CommandBase>();
                    result.Add(group);
                    groupFlag = true;
                    command = null;
                }
                else if (command is EndGroup)
                {
                    groupFlag = false;
                    command = null;
                }

                else if (!groupFlag)
                {
                    if (group.Count != 0)
                    {
                        group = new List<CommandBase>();
                        result.Add(group);
                    }
                }

                if (command != null) group.Add(command);
            }

            return result;
        }
    }
}