using UnityEngine;

namespace SilCilSystem.Singletons
{
    [DefaultExecutionOrder(EditorConstants.SingletonExecutionOrder)]
    /// <summary>
    /// 1つしか存在しないMonoBehaviour
    /// 【挙動】既に存在する場合は削除、そうでなければDontDestroyOnLoadをコール
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>唯一のインスタンス</summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Awakeで呼ばれる
        /// 【実装】削除される場合は呼ばれない
        /// 【注意】Instanceの判定はAwakeでやるので、それ以外の初期化処理を記述すること
        /// 【作成理由】Awakeをvirtualにしてしまうと、overrideした時にbase.Awakeをコールし忘れると機能しなくなる
        /// </summary>
        protected abstract void OnAwake();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this.gameObject);
                OnAwake();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// OnDestroyで呼ばれる
        /// 【注意】InstanceをnullにするのはOnDestroyで呼ぶのでそれ以外の処理を記述すること
        /// 【作成理由】OnDestroyをvirtualにしてしまうと、overrideした時にbase.OnDestroyをコールし忘れると機能しなくなる
        /// </summary>
        protected abstract void OnDestroyCallback();

        private void OnDestroy()
        {
            Instance = (Instance == this) ? null : Instance;
            OnDestroyCallback();
        }
    }
}