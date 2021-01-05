using System.Collections;

namespace SilCilSystem.Singletons
{
    /// <summary>シーンの読み込みを行うインターフェース</summary>
    public interface ISceneLoader
    {
        /// <summary>ゲーム開始時処理用コルーチン</summary>
        IEnumerator StartEffect();

        /// <summary>シーン読み込みコルーチン</summary>
        IEnumerator LoadScene(string sceneName);
    }
}