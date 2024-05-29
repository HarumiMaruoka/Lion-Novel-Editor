// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;

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

        private int _currentElementIndex = 0;

        public override void OnEnter()
        {
            _currentElementIndex = 0;
            Debug.Log(
                $"Branch Node Enter\n" +
                $"Node Name: {_nodeName}");
        }

        public override void OnUpdate()
        {
            // Elementsが有効かどうかを確認する。
            if (_elements == null)
            {
                Debug.Log("Elements is null");
                return;
            }
            if (_elements.Count == 0)
            {
                Debug.Log("Elements is empty");
                return;
            }

            // キー入力によって選択中の要素を変更する。
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _currentElementIndex++;
                if (_currentElementIndex >= _elements.Count)
                {
                    _currentElementIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentElementIndex--;
                if (_currentElementIndex < 0)
                {
                    _currentElementIndex = _elements.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _controller.MoveTo(_elements[_currentElementIndex].Child);
            }
        }

        public override void OnExit()
        {
            Debug.Log(
                $"Branch Node Exit\n" +
                $"Node Name: {_nodeName}");
        }
    }
}