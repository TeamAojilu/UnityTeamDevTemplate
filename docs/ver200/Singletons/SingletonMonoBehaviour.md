# SingletonMonoBehaviour\<T>

abstract

名前空間：SilCilSystem.Singletons

継承：UnityEngine.MonoBehaviour

---

ゲーム内に1つしか存在しないコンポーネントの作成に使用する抽象クラスです。

## メンバ一覧

### メソッド

|member|description|
|-|-|
|protected void OnAwake()|Awakeで呼ばれます。基底クラスのAwakeをvirtualにするとbase.Awakeの呼び出し忘れが起こるのでこういう形にしています。|
|protected void OnDestroyCallback()|OnDestroyで呼ばれます。|

## 使用例

シングルトンを作成する場合はこのクラスを継承したコンポーネントを作成します。
例えば、文字列をDebug.Logで表示するためのシングルトンは以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Singletons;

public class SingletonExample : SingletonMonoBehaviour<SigletonExample>
{
    protected override void OnAwake()
    {
        // Awake時に呼ばれます。
    }

    protected override void OnDestroyCallback()
    {
        // OnDestroy時に呼ばれます。
    }

    public void Write(string text)
    {
        Debug.Log(text);
    }
}
```

呼び出しはInstanceにアクセスします。

```cs
using UnityEngine;

public class SingletonExampleUser : MonoBehaviour
{
    private void Start()
    {
        SingletonExample.Instance.Write("Test");
    }
}
```

## 使用上の注意点

作成したシングルトンは自動で生成されるわけではありません。
スクリプトをアタッチしたGameObjectをシーン上に配置しましょう。

## 実装

Awake内で重複がないかをチェックしています。
すでに別のGameObjectが存在する場合には削除し、存在しない場合はDontDestoryOnLoadを用いてGameObjectが破棄されないように設定します。

```cs
private void Awake()
{
    if(Instance == null)
    {
        Instance = this as T;
        DontDestroyOnLoad(this.gameObject);
        OnAwake();
    }
    else
    {
        Destroy(gameObject);
    }
}
```

明示的にGameObjectを削除した場合にはInstanceにはnullが代入されます。
これはOnDestroyで処理しています。

```cs
private void OnDestroy()
{
    Instance = (Instance == this) ? null : Instance;
    OnDestroyCallback();
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
