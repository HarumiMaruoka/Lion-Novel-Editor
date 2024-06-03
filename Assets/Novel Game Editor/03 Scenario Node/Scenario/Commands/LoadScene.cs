using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class LoadScene : CommandBase
    {
        private string _sceneName;

        public LoadScene(Config config, string[] commandArgs) : base(config, commandArgs)
        {
            _sceneName = commandArgs[0];
        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}