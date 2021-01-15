# Comparison.CompareType

enum

名前空間：SilCilSystem.Math

---

値を比較するための列挙型です。
インスペクタ上から指定できるようにするために作成しました。

## メンバ一覧

### 比較方法

|value|description|
|-|-|
|EqualTo|一致している場合true|
|NotEqualTo|一致していない場合true|
|LessThan|値が小さい場合true|
|LessThanOrEqualTo|値が小さいか一致している場合true|
|GreaterThan|値が大きい場合true|
|GreaterThanOrEqualTo|値が大きいか一致している場合true|

### 拡張メソッド

|return|signature|description|note|
|-|-|-|
|bool|Compare\<T>(this CompareType compareType, T value1, T value2)|value1とvalue2を比較|TはIComparable\<T>|
|bool|Compare\<T>(this CompareType compareType, T value1, T value2, IComparer\<T> comparer)|value1とvalue2を比較||
|bool|CompareTo\<T>(this T value1, T value2, CompareType compareType)|value1とvalue2を比較|TはIComparable\<T>|
|bool|Compare\<T>(this T value1, T value2, CompareType compareType, IComparer\<T> comparer)|value1とvalue2を比較||

## 使用例

Compare\<T>メソッド、もしくはCompareTo\<T>メソッドを利用します。
floatやint, stringなどはIComparable\<T>を継承しているので、そのまま使えます。
継承されていないものに関してはIComparer\<T>を継承したものを指定しましょう。

```cs
using UnityEngine;
using SilCilSystem.Math;

public class Test : MonoBehaviour
{
    [SerializeField] private Comparison.CompareType m_compareType = default;
    [SerializeField] private float m_value1 = 0f;
    [SerializeField] private float m_value2 = 1f;

    private void Update()
    {
        bool result = m_compareType.Compare(m_value1, m_value2);
        
        // こっちでもOK.
        // bool result = m_value1.CompareTo(m_value2, m_compareType);
        
        Debug.Log(result);
    }
}
```

## 実装

switch構文でそれぞれ処理しています。

```cs
public static bool Compare<T>(this CompareType compareType, T value1, T value2) where T : IComparable<T>
{
    switch (compareType)
    {
        case CompareType.EqualTo: return value1.CompareTo(value2) == 0;
        case CompareType.NotEqualTo: return value1.CompareTo(value2) != 0;
        case CompareType.LessThan: return value1.CompareTo(value2) < 0;
        case CompareType.LessThanOrEqualTo: return value1.CompareTo(value2) <= 0;
        case CompareType.GreaterThan: return value1.CompareTo(value2) > 0;
        case CompareType.GreaterThanOrEqualTo: return value1.CompareTo(value2) >= 0;
        default: return false;
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
