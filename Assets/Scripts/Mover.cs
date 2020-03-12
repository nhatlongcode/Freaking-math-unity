using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Connexion.Miscellaneous
{
    public class Mover : MonoBehaviour
    {
        public AnimationCurve animationCurve;

        private IEnumerator Move(Vector2 m_origin, Vector2 m_dest, float duration)
        {
            float elapsed = 0f;

            while (true)
            {
                elapsed += Time.deltaTime;
                float percent = Mathf.Clamp01(elapsed / duration);

                float curvePercent = animationCurve.Evaluate(percent);
                var pos = Vector2.LerpUnclamped(m_origin, m_dest, curvePercent);
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);

                if (elapsed > duration)
                    break;

                yield return null;
            }
        }

        public void MoveTo(Vector2 m_dest, float duration)
        {
            StartCoroutine(Move(transform.position, m_dest, duration));
        }
    }
}