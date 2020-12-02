# SceneLoader

class

名前空間：SilCilSystem.Singletons

継承：SingletonMonoBehaviour\<T>

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

LoadSceneメソッドの呼び出しでシーンを読み込みます。
キーを入力するとシーンを読み込む場合は以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Singletons;

public class Test : MonoBehaviour
{
    [SerializeField] private string m_nextSceneName = "TestScene";

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoader.LoadScene(m_nextSceneName);
        }
    }
}
```

シーンの遷移処理が終了するまで待ちたいことも多いので、WaitLoadingコルーチンを用意しています。

```cs
IEnumetator Start()
{
    // 処理を待ちたいときはコルーチンでyield returnする.
    yield return SceneLoader.WaitLoading;
}
```

## 使用上の注意点

IsBusyがtrueの場合はLoadSceneを呼び出しても何も起こりません。

## 実装

実際のロード処理はLoaderに委譲しています。
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

Loaderはグローバルにアクセスが可能なため、独自の処理を作成し指定できます。
独自に作成する場合はISceneLoaderインターフェースを実装してください。

ISceneLoaderは以下のメソッドが定義されたインターフェースです。

|member|description|
|-|-|
|IEnumerator StartEffect()|SceneLoaderのStartメソッドで呼ばれるコルーチンです。|
|IEnumerator LoadScene(string sceneName)|シーンのロード処理を行うコルーチンです。|

例えば、以下のように、ISceneLoaderを継承したコンポーネントを作成してStartやAwakeメソッドでLoaderに設定して独自の処理を行います。

```cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SilCilSystem.Singletons;

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
        // 例えば、1秒待ってからシーンを読み込み.
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
```
