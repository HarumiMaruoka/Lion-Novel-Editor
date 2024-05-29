// 日本語対応
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public interface ISingleChild : IOutputNode
    {
        Node Child { get; set; }
    }
}