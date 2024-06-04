using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Glib.NovelGameEditor.Scenario.Commands.ActorActions
{
    public class ActorAction : CommandBase
    {
        private Actor _actor;
        private Func<Actor, Config, string[], UniTask> _action;
        private string[] _args;

        public ActorAction(Config config, string[] commandArgs) : base(config, commandArgs)
        {
            var actorType = (ActorType)Enum.Parse(typeof(ActorType), commandArgs[0]);
            _actor = config.FindActor(actorType);

            var actionName = commandArgs[1].Trim();
            _action = actionName switch
            {
                "Move" => ActorExtensions.Move,
                "Rotate" => ActorExtensions.Rotate,
                "Scale" => ActorExtensions.Scale,
                "PlayAnimation" => ActorExtensions.PlayAnimation,
                "ChangeSprite" => ActorExtensions.ChangeSprite,
                "Flip" => ActorExtensions.Flip,
                "Fade" => ActorExtensions.Fade,
                "Shake" => ActorExtensions.Shake,
                "Jump" => ActorExtensions.Jump,
                "Reaction" => ActorExtensions.Reaction,
                _ => throw new ArgumentOutOfRangeException()
            };

            _args = commandArgs[2..];
        }

        public override async UniTask RunCommand(CancellationToken token = default)
        {
            await _action(_actor, _config, _args);
        }
    }

    public enum ActorType
    {
        アリス,
        ボブ,
    }
}