// 日本語対応
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class NovelGameController : MonoBehaviour
    {
        [SerializeField]
        private NovelNodeGraph _nodeGraph;
        [SerializeField]
        private Config _config;

        private Node _current = null;

        public Config Config => _config;

        public void Play()
        {
            if (!_nodeGraph)
            {
                Debug.LogWarning("Warning: NovelNodeGraph not assigned.");
                return;
            }

            foreach (var node in _nodeGraph.Nodes)
            {
                node.Initialize(this);
            }

            _current = _nodeGraph.RootNode.Child;
            _current.OnEnter();
        }

        private void Stop()
        {
            _current = null;
        }

        private void Update()
        {
            if (_current != null)
            {
                _current.OnUpdate();
            }
        }

        public void MoveTo(Node node)
        {
            _current.OnExit();
            _current = node;
            if (_current != null) _current.OnEnter();
        }
    }
}