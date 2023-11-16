using System.Collections;
using UnityEngine;

namespace Tweening
{
    public enum EasingType
    {
        easeInSine,
        easeOutSine,
        easeInOutSine,
        easeOutBounce,
    }
    
    public class Tweening : MonoSingleton<Tweening>
    {
        private void DOMove()
        {
            
        }
        
        private IEnumerator Move(Transform transform, float duration, EasingType easing)
        {
            float currentTime = 0;
            float percentTime = currentTime / duration;
            switch (easing)
            {
                
            }

            float additive;
            while (percentTime <= 1)
            {
                currentTime += Time.deltaTime;
                percentTime = currentTime / duration;
                yield return null;
            }
        }
        
        // Sine
        private float easeInSine(float x)
        {
            return 1 - Mathf.Cos((x * Mathf.PI) / 2);
        }
        private float easeOutSine(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2);
        }
        private float easeInOutSine(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }
        
        // Cubic
        private float easeInCubic(float x)
        {
            return x * x * x;
        }
        private float easeOutCubic(float x)
        {
            return 1 - Mathf.Pow(1 - x, 3);
        }
        private float easeInOutCubic(float x)
        {
            return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }
        
        // Quint
        private float easeInQuint(float x)
        {
            return x * x * x * x * x;
        }
        private float easeOutQuint(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }
        private float easeInOutQuint(float x)
        {
            return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        }
        
        // Circ
        private float easeInCirc(float x)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
        }
        private float easeOutCirc(float x)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
        }
        private float easeInOutCirc(float x)
        {
            return x < 0.5
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
        }
        
        // Elastic
        private float easeInElastic(float x)
        {
            float c4 = (2 * Mathf.PI) / 3;
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }
        private float easeOutElastic(float x)
        {
            float c4 = (2 * Mathf.PI) / 3;

            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }
        private float easeInOutElastic(float x)
        {
            float c5 = (2 * Mathf.PI) / 4.5f;

            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5f
                        ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
                        : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
        }
        
        // Quad
        private float easeInQuad(float x)
        {
            return x * x;
        }
        private float easeOutQuad(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }
        private float easeInOutQuad(float x)
        {
            return x < 0.5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }
        
        // Quart
        private float easeInQuart(float x)
        {
            return x * x * x * x;
        }
        private float easeOutQuart(float x)
        {
            return 1 - Mathf.Pow(1 - x, 4);
        }
        private float easeInOutQuart(float x)
        {
            return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
        }
        
        // Expo
        private float easeInExpo(float x)
        {
            return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
        }
        private float easeOutExpo(float x)
        {
            return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
        }
        private float easeInOutExpo(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
        }
        
        // Back
        private float easeInBack(float x)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return c3 * x * x * x - c1 * x * x;
        }
        private float easeOutBack(float x)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
        }
        private float easeInOutBack(float x)
        {
            float c1 = 1.70158f;
            float c2 = c1 * 1.525f;

            return x < 0.5
                ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
        
        // Bounce
        private float easeInBounce(float x)
        {
            return 1 - easeOutBounce(1 - x);
        }
        private float easeOutBounce(float x)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;

            if (x < 1 / d1) {
                return n1 * x * x;
            } else if (x < 2 / d1) {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            } else if (x < 2.5f / d1) {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            } else {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }
        private float easeInOutBounce(float x)
        {
            return x < 0.5
                ? (1 - easeOutBounce(1 - 2 * x)) / 2
                : (1 + easeOutBounce(2 * x - 1)) / 2;
        }
        
        
    }
}