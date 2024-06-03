// 日本語対応
using System;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class BranchElement : Node, ISingleChild
    {
#if Novel_Game_Editor_Development
        [SerializeField]
#else
        [SerializeField]
        [HideInInspector]
#endif
        private Node _child;
        [SerializeField]
        private string _label;

        public string Label => _label;

        public Node Child { get => _child; set => _child = value; }

        public Node Node => this;

        public override Type NodeViewType => typeof(BranchNodeView);
        public override void Initialize(NovelGameController controller)
        {

        }

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