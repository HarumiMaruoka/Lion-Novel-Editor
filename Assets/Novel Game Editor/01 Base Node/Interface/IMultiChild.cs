// 日本語対応
using System.Collections.Generic;

namespace Glib.NovelGameEditor
{
    public interface IMultiChild : IOutputNode
    {
        List<Node> Children { get; }
    }
}