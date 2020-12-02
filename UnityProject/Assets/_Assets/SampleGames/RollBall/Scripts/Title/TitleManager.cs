using System.Collections;
using UnityEngine;
using SilCilSystem.Singletons;

namespace Samples.RollBall
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private string m_nextScene = "Play";

        private IEnumerator Start() 
        {
            yield return SceneLoader.WaitLoading;
            yield return new WaitUntil(() => Input.anyKeyDown);
            SceneLoader.LoadScene(m_nextScene);
        }
    }
}