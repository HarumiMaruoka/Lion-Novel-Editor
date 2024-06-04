using System;
using UnityEngine;
using UnityEngine.UI;

namespace Glib.NovelGameEditor.Scenario.Commands.ActorActions
{
    public class Actor : MonoBehaviour
    {
        public ActorType ActorType;
        public Sprite[] Sprites;
        public Animator Animator;
        public Image ActorFrontView;
        public Image ActorBackView;
        public RectTransform RectTransform;
        public Reaction[] Reactions;
    }
}

public enum ActorType
{
    アリス,
    ボブ,
}