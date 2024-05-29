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
        private NovelGameController _controller;

        private string _sheet =
            @"[BeginGroup]
              [FadeScreen, 0, 1]
              [PrintText, Hello, 0.1]
              [EndGroup]
              
              [ChangeCaption, Shinohara]
              [PrintText, Hello1, 0.1]
              
              [ChangeCaption, Nakazawa]
              [PrintText, Hello2, 0.1]
              
              [ChangeCaption, Maruoka]
              [PrintText, Hello3, 0.1]
              
              [ChangeCaption,Reo]
              [PrintText, Hello4, 0.1]
              
              [BeginGroup]
              [FadeScreen, 1, 1]
              [ChangeCaption, Nakazawa Reo]
              [PrintText, HelloHelloHello, 0.1]
              [EndGroup]";

        private ICommand[] _commands;

        public bool IsFinished { get; private set; } = false;

        public void Initialize(NovelGameController controller)
        {
            _controller = controller;
            _commands = CommandLoader.LoadSheet(_sheet, _controller.Config);
        }

        public void Play(CancellationToken token = default)
        {
            IsFinished = false;
            var executable = MakeExecutable(_commands);
            RunCommands(executable, token);
        }

        public async void RunCommands(List<List<ICommand>> executable, CancellationToken token)
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

        private List<List<ICommand>> MakeExecutable(ICommand[] commands)
        {
            bool groupFlag = false;
            List<List<ICommand>> result = new List<List<ICommand>>();
            List<ICommand> currentCollection = null;

            currentCollection = new List<ICommand>();
            result.Add(currentCollection);

            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i];

                if (command is BeginGroup)
                {
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
                    if (currentCollection.Count != 0)
                    {
                        currentCollection = new List<ICommand>();
                        result.Add(currentCollection);
                    }
                }

                if (command != null) currentCollection.Add(command);
            }

            return result;
        }
    }
}