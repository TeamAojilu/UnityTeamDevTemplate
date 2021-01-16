# UpdateMode

enum

名前空間：SilCilSystem.Timers

Assembly：SilCilSystem.Packages

---

更新タイミングを表す列挙型です。
[TimeMethods][page:TimeMethods]や[UpdateDispatcher][page:UpdateDispatcher]で使用します。

## 値

|member|description|
|-|-|
|DeltaTime|UpdateでTime.deltaTimeを使用|
|UnscaledDeltaTime|UpdateでTime.unscaledDeltaTimeを使用|
|FixedDeltaTime|FixedUpdateでTime.fixedDeltaTimeを使用|
|FixedUnscaledDeltaTime|FixedUpdateでTime.fixedUnscaledDeltaTimeを使用|
|LateUpdateDeltaTime|LateUpdateでTime.deltaTimeを使用|
|LateUpdateUnscaledDeltaTime|LateUpdateでTime.unscaledDeltaTimeを使用|

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
