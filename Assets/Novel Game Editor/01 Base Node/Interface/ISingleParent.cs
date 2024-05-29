// 日本語対応
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public interface ISingleParent : IInputNode
    {
        Node Parent { get; set; }
    }
}