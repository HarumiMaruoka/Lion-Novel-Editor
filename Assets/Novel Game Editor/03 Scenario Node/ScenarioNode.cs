using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Glib.NovelGameEditor
{
    [GraphViewContextMenu]
    public class ScenarioNode : Node, IMultiParent, ISingleChild
    {
        public override Type NodeViewType => typeof(ScenarioNodeView);

        [SerializeField]
        private List<Node> _parents = new List<Node>();
        public List<Node> Parents => _parents;

        public Node Node => this;

        [SerializeField]
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

        [SerializeField]
        private TextAsset _talkData;

        [SerializeField]
        private float _displayInterval = 0.1f;
        private float _displayTimer = 0f;

        private TalkData[] _talkDatas;
        private int _talkIndex = 0;
        private int _contentIndex = 0;
        private StringBuilder _content = new StringBuilder();

        public struct TalkData
        {
            public string Caption;
            public string Content;
        }

        public override void OnEnter()
        {
            var csv = _talkData.LoadCsv();
            _talkDatas = new TalkData[csv.Length];
            for (int i = 0; i < csv.Length; i++)
            {
                _talkDatas[i] = new TalkData
                {
                    Caption = csv[i][0],
                    Content = csv[i][1]
                };
            }
        }

        public override void OnUpdate()
        {
            // ˆê•¶Žš‚Ã‚Â•\Ž¦‚·‚éB
            _displayTimer += Time.deltaTime;
            if (_displayTimer > _displayInterval && _contentIndex < _talkDatas[_talkIndex].Content.Length)
            {
                _displayTimer = 0f;
                _content.Append(_talkDatas[_talkIndex].Content[_contentIndex]);
                _contentIndex++;
                _controller.Config.TextBox.text = _content.ToString();
            }
            else if (Input.anyKeyDown)
            {
                if (_contentIndex < _talkDatas[_talkIndex].Content.Length)
                {
                    _contentIndex = _talkDatas[_talkIndex].Content.Length;
                    _controller.Config.TextBox.text = _talkDatas[_talkIndex].Content;
                }
                else if (_talkIndex < _talkDatas.Length)
                {
                    _displayTimer = 0f;
                    _talkIndex++;
                    _content.Clear();
                    _controller.Config.TextBox.text = _content.ToString();
                }
                else
                {
                    _controller.MoveTo(Child);
                }
            }
        }

        public override void OnExit()
        {
            //Debug.Log(
            //    $"Talk Node Exit\n" +
            //    $"Node Name: {_nodeName}");
        }
    }
}