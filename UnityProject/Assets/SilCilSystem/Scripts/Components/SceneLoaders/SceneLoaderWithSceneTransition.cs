using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SilCilSystem.Singletons;

namespace SilCilSystem.Components.SceneLoaders
{
    /// <summary>
    /// ISceneTransitionを実装したコンポーネントを利用するISceneLoaderコンポーネント.
    /// 【注意】ISceneTransitionは子供に設置すること.
    /// </summary>
    [RequireComponent(typeof(SceneLoader))]
    public class SceneLoaderWithSceneTransition : MonoBehaviour, ISceneLoader
    {
        private ISceneTransition Fader => m_fader = m_fader ?? gameObject.GetComponentInChildren<ISceneTransition>(); 
        private ISceneTransition m_fader;

        private void Start()
        {
            SceneLoader.Instance.Loader = this;
        }

        public IEnumerator StartEffect()
        {
            yield return Fader?.ToClear();
        }

        public IEnumerator LoadScene(string sceneName)
        {
            // フェードの時間を無駄にしないように, 呼ばれた段階でロード処理を開始する.
            var operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            // 画面を非表示.
            yield return Fader?.ToBlack();

            // シーンを読み込み.
            yield return new WaitWhile(() => operation.progress < 0.9f);
            operation.allowSceneActivation = true;
            yield return operation;
            
            // 画面を表示.
            yield return Fader?.ToClear();
        }
    }
}