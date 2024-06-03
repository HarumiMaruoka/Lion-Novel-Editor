using System;
using UnityEngine;
using UnityEngine.UI;

namespace Glib.NovelGameEditor.Scenario.Commands.ActorActions
{
    public struct ActorInfo
    {
        public ActorType ActorType;
        public Image View;
        public RectTransform Transform;
        public Sprite Sprite;
        public string Name;
    }
}