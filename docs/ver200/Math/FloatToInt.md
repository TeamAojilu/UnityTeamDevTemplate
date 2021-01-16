# FloatToInt.CastType

enum

名前空間：SilCilSystem.Math

Assembly：SilCilSystem.Packages

---

float型からint型への変換を行うための列挙型です。
インスペクタ上から指定できるようにするために作成しました。
拡張メソッドによりfloatをintに変換できます。

## メンバ一覧

### 変換方法

|value|description|
|-|-|
|Simple|(int) value|
|FloorToInt|Mathf.Floor|
|CeilToInt|Mathf.CeilToInt|
|RoundToInt|Mathf.RoundToInt|

### 拡張メソッド

|return|Signature|変換前 -> 変換後の型|
|-|-|-|
|int|Cast(this CastType castType, float value)|float -> int|
|Vector2Int|Cast(this CastType castType, Vector2 value)|Vector2 -> Vector2Int|
|Vector3Int|Cast(this CastType castType, Vector3 value)|Vector3 -> Vector3Int|

## 使用例

Castメソッドを呼べば変換できます。

```cs
using UnityEngine;
using SilCilSystem.Math;

public class TestFloatToInt : MonoBehaviour
{
    [SerializeField] private FloatToInt.CastType m_castType = default;
    [SerializeField] private float m_value = -1.5f;

    private void Update()
    {
        int value = m_castType.Cast(m_value);
        Debug.Log(value);
    }
}
```

## 実装

switch構文でそれぞれ処理しています。

```cs
public static int Cast(this CastType castType, float value)
{
    switch (castType)
    {
        default:
        case CastType.Simple: return (int)value;
        case CastType.FloorToInt: return Mathf.FloorToInt(value);
        case CastType.CeilToInt: return Mathf.CeilToInt(value);
        case CastType.RoundToInt: return Mathf.RoundToInt(value);
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
