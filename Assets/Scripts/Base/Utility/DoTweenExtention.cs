using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using PathCreation;
using UnityEngine;

namespace Base.Utility
{
    public static class DoTweenExtension
    {
        public static TweenerCore<float, float, FloatOptions> DOMeshBlink(this GameObject target, int count, float duration)
        {
            int countBlink = count; // count blink
            float valueMax = 1;     // change value 0.0f - valueMax
            float coefficient = countBlink * 2 * Mathf.PI / valueMax;
        
            return DOTween.To(
                () => 0f, 
                x => target.SetActive(Mathf.Cos(coefficient * x) > 0), 
                valueMax, 
                duration);
        }

        public static TweenerCore<float, float, FloatOptions> DOMovePath(this GameObject target, PathCreator path, float duration)
        {
            return DOTween.To(
                () => 0.0f,
                t => target.transform.position = path.path.GetPointAtTime(t, EndOfPathInstruction.Stop),
                1.0f,
                duration
                );
        }
    }
}
