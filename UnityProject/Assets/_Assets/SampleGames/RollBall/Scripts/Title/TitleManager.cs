using System.Collections;
using UnityEngine;
using SilCilSystem.SceneLoader;

namespace RollBall
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