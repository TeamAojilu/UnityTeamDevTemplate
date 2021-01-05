using System.Collections;
using UnityEngine;

namespace SilCilSystem.Singletons
{
    /// <summary>
    /// シーンをロードするシングルトン.
    /// 【実装】Loaderが実際の処理を担っているので、どちらかというとサービスロケータに近い
    /// </summary>
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        /// <summary>処理中はtrue【注意】trueならLoadScene呼び出しは無効</summary>
        public static bool IsBusy => Instance?.m_isBusy == true;

        /// <summary>【注意】処理中はIsBusyがtrueになり、他の呼び出しをキャンセル</summary>
        public static void LoadScene(string sceneName)
        {
            if (IsBusy) return;
            Instance.StartCoroutine(Instance.LoadSceneCoroutine(sceneName));
        }

        /// <summary>処理が終わるまで待機するコルーチン</summary>
        public static IEnumerator WaitLoading => m_waitLoading;
        private static WaitWhile m_waitLoading = new WaitWhile(() => IsBusy); // 何度もnewしないようにキャッシュしておく.
        
        /// <summary>シーン遷移処理の移譲先</summary>
        public ISceneLoader Loader { get; set; }

        private bool m_isBusy = true;

        private IEnumerator Start()
        {
            m_isBusy = true;
            yield return null; // Loaderのセットを待つために1フレーム待機する.
            yield return Loader?.StartEffect();
            m_isBusy = false;
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            m_isBusy = true;
            yield return Loader?.LoadScene(sceneName);
            m_isBusy = false;
        }

        protected override void OnAwake() { }
        protected override void OnDestroyCallback() { }
    }
}