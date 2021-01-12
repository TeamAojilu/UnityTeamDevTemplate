# Timelineで変数の値を変える

Timeline機能と連携して変数の値を変化させることができます。

## 使用例

変数オブジェクトをTimelineにドラッグ&ドロップすればTrackができます。

### BoolActivation

bool型の変数オブジェクトを用いるとBoolActivationが生成されます。
これはclip内でboolの値がtrueになり、clip外でfalseになります。

![BoolActivationを作成して実行する][fig:BoolActivation]

### Tween

floatなどの変数オブジェクトを用いるとTweenVaraiableが生成されます。
これは特定の値からある値へと徐々に変化させるclipです。

![TweenVariableを作成して実行する][fig:TweenVariable]

### 対応している型

#### Primitive型

- float
- int

#### struct

- Vector2
- Vector2Int
- Vector3
- Vector3Int
- Quaternion
- Color

## 使用上の注意点

CurveTypeの変更などは実行されないと反映されないようです。

## 実装

PlayableAssetとPlayableBehaviourを継承して実現しています。
例えば、Tweenの処理はこんな感じです。

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

{% include ver100/footer.md %}

<!--- 参照 --->

{% include ver100/paths.md %}

[fig:BoolActivation]: Figures/BoolActivation.gif
[fig:TweenVariable]: Figures/TweenVariable.gif
