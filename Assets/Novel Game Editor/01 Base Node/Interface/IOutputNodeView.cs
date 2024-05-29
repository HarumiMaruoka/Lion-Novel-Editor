// 日本語対応
using UnityEditor.Experimental.GraphView;

namespace Glib.NovelGameEditor
{
    public interface IOutputNodeView : INodeView
    {
        Port OutputPort { get; }
        IOutputNode OutputConnectable { get; }
    }
}