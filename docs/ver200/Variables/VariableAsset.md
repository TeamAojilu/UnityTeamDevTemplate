# VariableAsset

abstract

名前空間：SilCilSystem.Variables.Base

継承：UnityEngine.ScriptableObject

---

[変数アセット][page:Variable]と[イベントアセット][page:GameEvent]の基底クラスです。

## 実装

ScriptableObjectをそのまま継承した抽象クラスです。
他のScriptableObjectと区別するために使用しています。

```cs
using UnityEngine;

namespace SilCilSystem.Variables.Base
{
    public abstract class VariableAsset : ScriptableObject{ }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
