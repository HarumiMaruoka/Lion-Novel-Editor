using Glib.NovelGameEditor.Scenario.Commands.ActorActions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Glib.NovelGameEditor
{
    [Serializable]
    public class Config
    {
        public Text Caption;
        public Text TextBox;
        public Image FadeImage;
        public ChoiceViewManager ChoiceViewManager;

        public Actor[] Actors;

        public Image BackgroundFront;
        public Image BackgroundBack;

        public Sprite[] BackgroundSprites;

        public Actor FindActor(ActorType actorType)
        {
            return Array.Find(Actors, actor => actor.ActorType == actorType);
        }

        public Sprite FindBackgroundSprite(string spriteName)
        {
            return Array.Find(BackgroundSprites, sprite => sprite.name == spriteName.Trim());
        }
    }
}