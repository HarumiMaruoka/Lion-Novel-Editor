using System;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public abstract class Node : ScriptableObject
    {
#if Novel_Game_Editor_Development
        [SerializeField]
#else
        [SerializeField]
        [HideInInspector]
#endif
        private NodeViewData _viewData;
        [SerializeField]
        protected string _nodeName;

        protected NovelGameController _controller;

        public abstract Type NodeViewType { get; }

        public NodeViewData ViewData => _viewData ??= new NodeViewData();
        public string NodeName => _nodeName;
        public NovelGameController Controller => _controller;

        public virtual void Initialize(NovelGameController controller)
        {
            _controller = controller;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}