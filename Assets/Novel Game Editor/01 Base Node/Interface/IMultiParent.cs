// 日本語対応
using System.Collections.Generic;

namespace Glib.NovelGameEditor
{
    public interface IMultiParent : IInputNode
    {
        List<Node> Parents { get; }
    }
}