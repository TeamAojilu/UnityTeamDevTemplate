using UnityEngine;

namespace SilCilSystem
{
    /// <summary>
    /// ゲーム開始時にプレハブを自動生成するためのクラス.
    /// シングルトンを自動生成するために作成.
    /// Resourcesを使っているので、ビルド時は最初のシーンにプレハブを置いてしまうほうがいいと思う.
    /// </summary>
    internal static class PrefabGeneratorOnLoad
    {
        private const string Path = "InitialPrefabs";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            foreach (var prefab in Resources.LoadAll<GameObject>(Path))
            {
                GameObject.Instantiate(prefab);
            }
        }
    }
}