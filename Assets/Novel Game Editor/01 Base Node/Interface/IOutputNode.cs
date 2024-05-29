// 日本語対応

namespace Glib.NovelGameEditor
{
    /// <summary> Outputポートが存在するノードであることを表現する。 </summary>
    public interface IOutputNode
    {
        Node Node { get; }
        void OutputConnect(Node child);
        void OutputDisconnect(Node child);
    }
}