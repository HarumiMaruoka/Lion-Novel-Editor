using Cysharp.Threading.Tasks;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public interface ICommand
    {
        UniTask RunCommand(CancellationToken token = default);
    }
}