using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public class CaptionActions
    {
        public static async UniTask ChangeCaption(Config config, string[] commandArgs)
        {
            config.Caption.text = commandArgs[0];
        }
    }
}