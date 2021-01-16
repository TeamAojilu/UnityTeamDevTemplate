# SceneLoader

class

名前空間：SilCilSystem.SceneLoaders

継承：[SingletonMonoBehaviour\<T>][page:SingletonMonoBehaviour]

Assembly：SilCilSystem.Packages

---

シーンのロード処理を行うシングルトンです。

## メンバ一覧

## プロパティ

|member|description|
|-|-|
|static bool IsBusy|処理中はtrueになります。|
|static IEnumerator WaitLoading|IsBusyがfalseになるまで待機するコルーチンです。|
|ISceneLoader Loader|シーンのロード処理を行うインターフェースを設定/取得します。|

## メソッド

|member|description|
|-|-|
|static void LoadScene(string sceneName)|シーンのロード処理を開始します。IsBusyがtrueの場合は無効になります。|

## 使用例

`LoadScene`メソッドの呼び出しでシーンを読み込みます。
キーを入力するとシーンを読み込む場合は以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.SceneLoaders;

public class TestSceneLoader : MonoBehaviour
{
    [SerializeField] private string m_nextSceneName = "TestScene";

    private void Update()
    {
        // シーンロード中はIsBusyがtrueになるので処理しない.
        if (SceneLoader.IsBusy == false && Input.anyKeyDown)
        {
            SceneLoader.LoadScene(m_nextSceneName);
        }
    }
}
```

シーンの遷移処理が終了するまで待ちたいことも多いので、`WaitLoading`コルーチンを用意しています。

```cs
using System.Collections;
using UnityEngine;
using SilCilSystem.SceneLoaders;

public class TestWaitSceneLoading : MonoBehaviour
{
    private IEnumerator Start()
    {
        // 処理を待ちたいときはコルーチンでyield returnする.
        yield return SceneLoader.WaitLoading;

        Debug.Log("Scene Loader is free");
    }
}
```

## 使用上の注意点

`IsBusy`が`true`の場合は`LoadScene`を呼び出しても何も起こりません。

## 実装

実際のロード処理は`Loader`に委譲しています。
つまり、シングルトンというよりはサービスロケーターに近い実装になっています。
これは画面エフェクト込みのロードやマルチシーンの対応など、カスタマイズできるようにするためです。

```cs
private IEnumerator LoadSceneCoroutine(string sceneName)
{
    m_isBusy = true;
    yield return Loader?.LoadScene(sceneName);
    m_isBusy = false;
}
```

### ISceneLoaderの実装

`Loader`はグローバルにアクセスが可能なため、独自の処理を作成し指定できます。
独自に作成する場合は`ISceneLoader`インターフェースを実装してください。

`ISceneLoader`は以下のメソッドが定義されたインターフェースです。

|member|description|
|-|-|
|IEnumerator StartEffect()|SceneLoaderのStartメソッドで呼ばれるコルーチンです。|
|IEnumerator LoadScene(string sceneName)|シーンのロード処理を行うコルーチンです。|

例えば、以下のように、`ISceneLoader`を継承したコンポーネントを作成して`Start`や`Awake`メソッドで`Loader`に設定して独自の処理を行います。

```cs
using System.Collections;
using UnityEngine;
using SilCilSystem.SceneLoaders;

public class CustomSceneLoader : MonoBehaviour, ISceneLoader
{
    private void Start()
    {
        // Loaderを自分自身に設定.
        SceneLoader.Instance.Loader = this;
    }

    public IEnumerator StartEffect()
    {
        // SceneLoaderのStartで呼ばれるコルーチンです.
        yield break; // 何もしない.
    }

    public IEnumerator LoadScene(string sceneName)
    {
        // 例えば、1秒待ってからシーン名を表示するだけにする.
        yield return new WaitForSeconds(1f);
        Debug.Log(sceneName);
    }
}
```

**実際に使用する場合は、デフォルトで用意されている`SceneLoader`のプレハブを変更するべきです。**
デフォルトで生成されるプレハブについては[SingletonMonoBehaviour][page:SingletonMonoBehaviour]を参考にしてください。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
