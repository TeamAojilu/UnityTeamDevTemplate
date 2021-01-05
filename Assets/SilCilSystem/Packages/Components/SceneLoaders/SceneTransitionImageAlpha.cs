using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SilCilSystem.Math;

namespace SilCilSystem.Components.SceneLoaders
{
    /// <summary>UI/ImageのAlpha値を使用して画面遷移処理を行うコンポーネント</summary>
    public class SceneTransitionImageAlpha : MonoBehaviour, ISceneTransition
    {
        [SerializeField] private float m_fadeTime = 0.5f;
        [SerializeField] private InterpolationCurve m_curve = default;

        private Image Image => m_image = m_image ?? GetComponentInChildren<Image>();
        private Image m_image = default;
        
        public IEnumerator ToBlack() => SetAlphaCoroutine(0f, 1f, true);

        public IEnumerator ToClear() => SetAlphaCoroutine(1f, 0f, false);

        private IEnumerator SetAlphaCoroutine(float startAlpha, float endAlpha, bool activeAtEnd = true)
        {
            Image.gameObject.SetActive(true);
            SetAlpha(startAlpha);
         
            float timer = 0f;
            while(timer < m_fadeTime)
            {
                timer += Time.deltaTime;
                SetAlpha(m_curve.Evaluate(Mathf.Lerp(startAlpha, endAlpha, timer / m_fadeTime)));
                yield return null;
            }
            
            SetAlpha(endAlpha);
            Image.gameObject.SetActive(activeAtEnd);
        }

        private void SetAlpha(float alpha)
        {
            var color = Image.color;
            color.a = alpha;
            Image.color = color;
        }
    }
}