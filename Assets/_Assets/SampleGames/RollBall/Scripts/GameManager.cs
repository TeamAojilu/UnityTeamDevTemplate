using UnityEngine;
using System.Collections;
using SilCilSystem.SceneLoaders;
using SilCilSystem.Variables;

namespace Samples.RollBall
{
    public class GameManager : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private VariableInt m_count = default;
        [SerializeField] private VariableBool m_isPlaying = default;

        [Header("Settings")]
        [SerializeField] private ReadonlyPropertyBool m_isClear = new ReadonlyPropertyBool(false);
        [SerializeField] private ReadonlyPropertyString m_nextScene = new ReadonlyPropertyString("RollBall");

        private IEnumerator Start()
        {
            m_count.Value = 0;
            m_isPlaying.Value = false;
            
            yield return SceneLoader.WaitLoading;

            m_isPlaying.Value = true;
            yield return new WaitUntil(() => m_isClear.Value);
            m_isPlaying.Value = false;

            yield return new WaitForSeconds(1f);
            SceneLoader.LoadScene(m_nextScene.Value);
        }
    }
}