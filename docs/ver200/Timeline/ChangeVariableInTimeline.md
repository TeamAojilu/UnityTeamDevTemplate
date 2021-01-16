# Timelineで変数の値を変える

`Timeline`機能と連携して変数の値を変化させることができます。

## 使用例

変数アセットを`Timeline`にドラッグ&ドロップすれば`Track`ができます。

### BoolActivation

`bool`型の変数アセットを用いると`BoolActivation`が生成されます。
これはクリップ内で値が`true`になり、クリップ外で`false`になります。

![BoolActivationを作成して実行する][fig:BoolActivation]

### Tween

`float`などの変数アセットを用いると`TweenVariable`が生成されます。
これは特定の値からある値へと徐々に変化させるクリップです。

![TweenVariableを作成して実行する][fig:TweenVariable]

### 対応している型

#### Primitive

- float
- int

#### Unityのstruct型

- Vector2
- Vector2Int
- Vector3
- Vector3Int
- Quaternion
- Color

## 使用上の注意点

`CurveType`の変更などは実行されないと反映されないようです。

## 実装

`PlayableAsset`と`PlayableBehaviour`を継承して実現しています。
例えば、`Tween`の処理はこんな感じです。

```cs
public class TweenVariableBehaviour<T> : PlayableBehaviour
{
    public Variable<T> Variable { get; set; }
    public T StartValue { get; set; }
    public T EndValue { get; set; }
    public Func<T, T, float, T> Interpolate { get; set; }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Variable.Value = Interpolate(StartValue, EndValue, 0f);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Variable.Value = Interpolate(StartValue, EndValue, 1f);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var progress = (float)(playable.GetTime() / playable.GetDuration());
        Variable.Value = Interpolate(StartValue, EndValue, progress);
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:BoolActivation]: Figures/BoolActivation.gif
[fig:TweenVariable]: Figures/TweenVariable.gif
