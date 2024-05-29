// 日本語対応
using System;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class RootNode : Node, ISingleChild
    {
#if Novel_Game_Editor_Development
        [SerializeField]
#else
        [SerializeField]
        [HideInInspector]
#endif
        private Node _child = null;

        public Node Node => this;
        public Node Child { get => _child; set => _child = value; }

        public override Type NodeViewType => typeof(RootNodeView);

        public void OutputConnect(Node child)
        {
            _child = child;
        }

        public void OutputDisconnect(Node child)
        {
            _child = null;
        }
    }
}