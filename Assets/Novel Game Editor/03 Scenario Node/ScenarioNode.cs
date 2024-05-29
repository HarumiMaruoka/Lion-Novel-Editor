using Glib.NovelGameEditor.Scenario.Commands;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    [GraphViewContextMenu]
    public class ScenarioNode : Node, IMultiParent, ISingleChild
    {
        #region Parent Child Connection
        public override Type NodeViewType => typeof(ScenarioNodeView);

        public Node Node => this;

        [SerializeField]
        [HideInInspector]
        private List<Node> _parents = new List<Node>();
        public List<Node> Parents => _parents;

        [SerializeField]
        [HideInInspector]
        private Node _child;
        public Node Child { get => _child; set => _child = value; }

        public void InputConnect(Node parent)
        {
            _parents.Add(parent);
        }

        public void InputDisconnect(Node parent)
        {
            _parents.Remove(parent);
        }

        public void OutputConnect(Node child)
        {
            Child = child;
        }

        public void OutputDisconnect(Node child)
        {
            Child = null;
        }
        #endregion

        [SerializeField]
        private CommandRunner _commandRunner;

        public override void Initialize(NovelGameController controller)
        {
            _commandRunner.Initialize(controller);
        }

        public override void OnEnter()
        {
            _commandRunner.Play();
        }

        public override void OnUpdate()
        {
            if (_commandRunner.IsFinished)
            {
                _controller.MoveTo(Child);
            }
        }

        public override void OnExit()
        {

        }
    }
}