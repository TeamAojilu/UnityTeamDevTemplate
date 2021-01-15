# ObjectPool\<T>

T: class, IPooledObject

class

名前空間：SilCilSystem.ObjectPools

---

オブジェクトを使いまわすことで、生成と破棄の処理を軽くするためのクラスです。

## メンバ一覧

### コンストラクタ

|member|description|
|-|-|
|ObjectPool(Func<T> createFunction, int capacity = 16)|createFunctionにはオブジェクトTの新規生成処理を指定します。capacityはオブジェクトTを保持するリストの初期capacityです。|

### メソッド

|member|description|
|-|-|
|T GetInstance()|新規使用できるインスタンスを取得します。|

## 使用例

対象となるオブジェクトにはIPooledObjectインターフェースを継承させます。
bool型のプロパティIsPooled { get; }をプールされている場合にtrueを返すように実装します。

例えば、シューティングゲームの弾などをゲームオブジェクトのアクティブを切り替えることでプーリングをする実装は以下になります。

```cs
using UnityEngine;
using SilCilSystem.ObjectPools;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPooledObject
{
    // アクティブがfalseなら再利用可能なのでtrueを返す.
    public bool IsPooled => !gameObject.activeSelf;

    private void OnEnable()
    {
        // 正面に飛んでいく. スピードはテキトー.
        GetComponent<Rigidbody>().velocity = transform.forward;
    }

    // 何かに当たったら非アクティブに.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        gameObject.SetActive(false);
    }
}
```

弾を生成するときはObjectPoolを使用します。
以下は例えば、1秒ごとに弾を生成するスクリプトです。

```cs
using UnityEngine;
using SilCilSystem.ObjectPools;

public class BulletGenerator : MonoBehaviour
{
    public Bullet m_prefab;
    private float m_timer = 0f;
    private ObjectPool<Bullet> m_pool;

    private void Start()
    {
        // 使いまわしできない場合はプレハブから生成する.
        m_pool = new ObjectPool(() => GameObject.Instantiate(m_prefab));
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer > 1f)
        {
            var bullet = m_pool.GetInstance();
            bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
            bullet.gameObject.SetActive(true);
            m_timer = 0f;
        }
    }
}
```

## 実装

```cs
public class ObjectPool<T> where T : class, IPooledObject
{
    private readonly List<T> m_instances;
    private readonly Func<T> m_createFunction;

    public ObjectPool(Func<T> createFunction, int capacity = 16)
    {
        m_instances = new List<T>(capacity);
        m_createFunction = createFunction;
    }

    public T GetInstance()
    {
        foreach (var instance in m_instances)
        {
            if (instance == null) continue;
            if (instance.IsPooled) return instance;
        }

        var newInstance = m_createFunction.Invoke();
        m_instances.Add(newInstance);
        return newInstance;
    }
}
```

どこまでサポートするかで悩みましたが、最低限の実装だけにしました。
プーリングする対象によってやりたいことが変化するためです。
ようするに、それぞれの対象に特化させる場合にはこのクラスのラッパークラスを書いたほうがいいだろうという判断です。
上述の例であれば、Bulletのtransformの設定やアクティブにするところまでをラッパークラスに記述すればいいでしょう。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
