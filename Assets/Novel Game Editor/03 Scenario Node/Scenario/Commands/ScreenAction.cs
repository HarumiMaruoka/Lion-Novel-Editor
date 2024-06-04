using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class ScreenAction
    {
        public static async UniTask FadeAsync(Config config, string[] args)
        {
            var targetValue = float.Parse(args[0]);
            var duration = float.Parse(args[1]);
            await config.FadeImage.FadeAsync(targetValue, duration);
        }
    }
}