using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class LoadScene : ICommand
    {
        private string _sceneName;

        public LoadScene(Config config, string[] commandArgs)
        {
            _sceneName = commandArgs[0];
        }

        public async UniTask RunCommand(CancellationToken token = default)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}