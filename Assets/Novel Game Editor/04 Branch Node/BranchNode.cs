// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Glib.NovelGameEditor
{
    [GraphViewContextMenu]
    public class BranchNode : Node, IMultiParent
    {
#if Novel_Game_Editor_Development
        [SerializeField]
#else
        [SerializeField]
        [HideInInspector]
#endif
        private List<Node> _parents = new List<Node>();

        public Node Node => this;

#if Novel_Game_Editor_Development
        [SerializeField]
#else
        [SerializeField]
        [HideInInspector]
#endif
        private List<BranchElement> _elements = new List<BranchElement>();
        public IReadOnlyList<BranchElement> Elements => _elements;
        public List<Node> Parents => _parents;

        public override Type NodeViewType => typeof(BranchNodeView);

        public void InputConnect(Node parent)
        {
            _parents.Add(parent);
        }

        public void InputDisconnect(Node parent)
        {
            _parents.Remove(parent);
        }

        public BranchElement CreateElement()
        {
            var instance = ScriptableObject.CreateInstance<BranchElement>();
            _elements.Add(instance);
            return instance;
        }

        public bool DeleteElement(BranchElement element)
        {
            return _elements.Remove(element);
        }

        public void MoveTo(Node node)
        {
            _controller.MoveTo(node);
        }

        public BranchElement GetElementAt(int index)
        {
            if (index < 0 || index >= _elements.Count) return null;
            return _elements[index];
        }

        public override void Initialize(NovelGameController controller)
        {
            _controller = controller;
        }

        private HashSet<ChoiceView> _choices = new HashSet<ChoiceView>();

        public override void OnEnter()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                var choiceView = _controller.Config.ChoiceViewManager.GetOrCreateChoiceView(i, _elements[i].Label);
                choiceView.OnClick += OnClick;
                _choices.Add(choiceView);
            }
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {
            foreach (var choice in _choices)
            {
                _controller.Config.ChoiceViewManager.ReturnChoiceView(choice);
                choice.OnClick -= OnClick;
            }
            _choices.Clear();
        }

        public void OnClick(int index)
        {
            var element = _elements[index];
            MoveTo(element.Child);
        }
    }
}