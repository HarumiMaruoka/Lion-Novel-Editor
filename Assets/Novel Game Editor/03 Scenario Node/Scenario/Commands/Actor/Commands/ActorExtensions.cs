using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor.Scenario.Commands.ActorActions
{
    public static class ActorExtensions
    {
        public static async UniTask Move(this Actor actor, Config config, string[] args)
        {
            var from = actor.RectTransform.anchoredPosition;
            var to = new Vector2(float.Parse(args[0]), float.Parse(args[1]));
            var duration = float.Parse(args[2]);

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                actor.RectTransform.anchoredPosition = Vector2.Lerp(from, to, t / duration);
                try
                {
                    if (!actor) return;
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.RectTransform.anchoredPosition = to;
        }

        public static async UniTask Rotate(this Actor actor, Config config, string[] args)
        {
            var from = actor.RectTransform.localEulerAngles;
            var to = new Vector3(0, 0, float.Parse(args[0]));
            var duration = float.Parse(args[1]);

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                actor.RectTransform.localEulerAngles = Vector3.Lerp(from, to, t / duration);

                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.RectTransform.localEulerAngles = to;
        }

        public static async UniTask Scale(this Actor actor, Config config, string[] args)
        {
            var from = actor.RectTransform.localScale;
            var to = new Vector3(float.Parse(args[0]), float.Parse(args[1]), 1);
            var duration = float.Parse(args[2]);

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                actor.RectTransform.localScale = Vector3.Lerp(from, to, t / duration);
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.RectTransform.localScale = to;
        }

        public static async UniTask PlayAnimation(this Actor actor, Config config, string[] args)
        {
            var animationName = args[0];
            actor.Animator.Play(animationName);
            try
            {
                await UniTask.WaitUntil(() => actor.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1, cancellationToken: actor.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        public static async UniTask ChangeSprite(this Actor actor, Config config, string[] args)
        {
            var index = int.Parse(args[0]);
            var duration = float.Parse(args[1]);

            // �o�b�N�̃X�v���C�g��ύX�Ώۂ̉摜�ɕύX
            actor.ActorBackView.color = new Color(actor.ActorBackView.color.r, actor.ActorBackView.color.g, actor.ActorBackView.color.b, 1);
            actor.ActorBackView.sprite = actor.Sprites[index];

            // �t�����g�̃X�v���C�g�𓧖��ɂ���B
            var frontFrom = actor.ActorFrontView.color;
            var frontTo = new Color(frontFrom.r, frontFrom.g, frontFrom.b, 0);
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                actor.ActorFrontView.color = Color.Lerp(frontFrom, frontTo, t / duration);
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
            actor.ActorFrontView.color = frontTo;

            // �t�����g�̃X�v���C�g��ύX�Ώۂ̉摜�ɕύX
            actor.ActorFrontView.sprite = actor.Sprites[index];

            // �o�b�N�̃X�v���C�g�𓧖��ɂ���B
            actor.ActorBackView.sprite = null;
            actor.ActorFrontView.color = new Color(frontFrom.r, frontFrom.g, frontFrom.b, 1);
            actor.ActorBackView.color = new Color(actor.ActorBackView.color.r, actor.ActorBackView.color.g, actor.ActorBackView.color.b, 0);
        }

        public static async UniTask Flip(this Actor actor, Config config, string[] args)
        {
            var direction = args[0] == "right" || args[0] == "Right" ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            var duration = float.Parse(args[1]);

            var from = actor.RectTransform.localRotation;
            var to = direction;

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                actor.RectTransform.localRotation = Quaternion.Lerp(from, to, t / duration);
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.RectTransform.localRotation = to;
        }

        public static async UniTask Fade(this Actor actor, Config config, string[] args)
        {
            var targetAlpha = float.Parse(args[0]);
            var duration = float.Parse(args[1]);

            var from = actor.ActorFrontView.color.a;
            var to = targetAlpha;

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                var color = actor.ActorFrontView.color;
                color.a = Mathf.Lerp(from, to, t / duration);
                actor.ActorFrontView.color = color;
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.ActorFrontView.color = new Color(actor.ActorFrontView.color.r, actor.ActorFrontView.color.g, actor.ActorFrontView.color.b, to);
        }

        public static async UniTask Shake(this Actor actor, Config config, string[] args)
        {
            var power = float.Parse(args[0]);
            var duration = float.Parse(args[1]);

            var shakeType = ShakeType.None;
            for (int i = 2; i < args.Length; i++)
            {
                shakeType |= (ShakeType)Enum.Parse(typeof(ShakeType), args[i]);
            }

            var from = actor.RectTransform.anchoredPosition;
            var beginRot = actor.RectTransform.localEulerAngles;
            var beginScale = actor.RectTransform.localScale;
            var to = from + UnityEngine.Random.insideUnitCircle * power;

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                var shake = UnityEngine.Random.insideUnitCircle * power;
                var position = actor.RectTransform.anchoredPosition;

                if (shakeType.HasFlag(ShakeType.Position))
                {
                    position = new Vector2(from.x + shake.x, from.y + shake.y);
                }

                if (shakeType.HasFlag(ShakeType.Rotation))
                {
                    actor.RectTransform.localEulerAngles = new Vector3(beginRot.x, beginRot.y, shake.x);
                }

                if (shakeType.HasFlag(ShakeType.Scale))
                {
                    actor.RectTransform.localScale = new Vector3(beginScale.x + shake.y, beginScale.y + shake.y, 1);
                }

                actor.RectTransform.anchoredPosition = position;
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            //���̒l�ɖ߂�
            actor.RectTransform.anchoredPosition = from;
            actor.RectTransform.localEulerAngles = beginRot;
            actor.RectTransform.localScale = beginScale;
        }

        [Flags]
        public enum ShakeType
        {
            None = 0,
            Everything = -1,

            Position = 1,
            Rotation = 2,
            Scale = 4,
        }

        public static async UniTask Jump(this Actor actor, Config config, string[] args)
        {
            var from = actor.RectTransform.anchoredPosition;
            var to = new Vector2(float.Parse(args[0]), float.Parse(args[1])); // �ړI�n
            var height = float.Parse(args[2]); // �W�����v�̍���
            var duration = float.Parse(args[3]); // �W�����v�ɂ����鎞��

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                var position = Vector2.Lerp(from, to, t / duration);
                position.y += Mathf.Sin(t / duration * Mathf.PI) * height;
                actor.RectTransform.anchoredPosition = position;
                try
                {
                    await UniTask.Yield(actor.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            actor.RectTransform.anchoredPosition = to;
        }

        public static async UniTask Reaction(this Actor actor, Config config, string[] args)
        {
            var reactionName = args[0].Trim();
            var reaction = Array.Find(actor.Reactions, r => r.ReactionName == reactionName);
            if (reaction == null)
            {
                Debug.LogError($"Reaction {reactionName} not found.");
                return;
            }

            await reaction.Play();
        }
    }
}