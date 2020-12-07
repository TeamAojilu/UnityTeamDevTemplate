using System.Collections;

namespace SilCilSystem.Components.SceneLoaders
{
    /// <summary>遷移処理の画面切り替え処理をコルーチンで行うインターフェース</summary>
    public interface ISceneTransition 
    {
        /// <summary>非表示にする</summary>
        IEnumerator ToBlack();
        /// <summary>表示する</summary>
        IEnumerator ToClear();
    }
}