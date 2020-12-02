using System;
using UnityEngine;

namespace SilCilSystem.Math
{
    [Serializable]
    public class InterpolationCurve
    {
        [SerializeField] private CurveTypeDefinition.CurveType m_curveType = CurveTypeDefinition.CurveType.Linear;
        [SerializeField] private bool m_useCustomCurve = false;
        [SerializeField] private AnimationCurve m_customCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private Func<float, float> m_curveFunc;

        public float Evaluate(float t)
        {
            m_curveFunc = m_curveFunc ?? ((m_useCustomCurve) ? m_customCurve.Evaluate : m_curveType.GetCurve());
            return m_curveFunc(t);
        }
    }
}