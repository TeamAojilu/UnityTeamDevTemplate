# VariableAsset

abstract

名前空間：SilCilSystem.Variables.Base

継承：UnityEngine.ScriptableObject

Assembly：SilCilSystem

---

[変数アセット][page:Variable]と[イベントアセット][page:GameEvent]の基底クラスです。

## 実装

ScriptableObjectをそのまま継承した抽象クラスです。
他のScriptableObjectと区別するために使用しています。

```cs
public abstract class VariableAsset : ScriptableObject 
{
#if UNITY_EDITOR
    // メモ用の変数. ビルド時には含まれない.
    [SerializeField, TextArea] internal string m_description = default;
#endif
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
