using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class CommandRunner : MonoBehaviour
    {
        // Sample code
        [SerializeField]
        private NovelGameController _controller;

        // Sample code
        public void Start()
        {
            var sheet =
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

            Play(sheet, _controller.Config);
        }

        public void Play(string sheetData, Config config)
        {
            var commands = CommandLoader.LoadSheet(sheetData, config);
            var executable = MakeExecutable(commands);
            RunCommands(executable);
        }


        public async void RunCommands(List<List<ICommand>> executable)
        {
            var token = this.GetCancellationTokenOnDestroy();
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