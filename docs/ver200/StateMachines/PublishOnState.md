# PublishOnState

class

名前空間：SilCilSystem.StateMachines

継承：UnityEngine.StateMachineBehaviour

---

AnimationState遷移時にイベントを発行するクラスです。
UnityのAnimator機能と[イベントアセット][page:GameEvent]を連携させることが可能です。

## 設定項目

Stateの開始時と終了時のタイミングで処理を呼ぶことができます。
引数なしのイベントを呼ぶ場合は、GameEvent, UnityEngine.Events.UnityEventが利用できます。
引数ありのイベントを呼ぶ場合には、以下の2通りが可能です。

- 引数を固定値でイベントを呼ぶ -> UnityEventでイベントアセットをPublish
- 引数にAnimatorのパラメータを使用する -> GameEventInfoInt, GameEventInfoFloat, GameEventInfoBoolを使用

※GameEventInfo~系はインスペクタ上での設定を可能にするためのprivateなサブクラスです。

### State開始時にイベントを呼ぶ

|type|name|description|note|
|-|-|-|-|
|GameEvent|OnStateEnter|引数なしのイベントアセット||
|GameEventInfoInt[]|OnStateEnterInt|int型を引数にとるイベントアセット|引数にAnimatorのint型パラメータを使用|
|GameEventInfoFloat[]|OnStateEnterFloat|int型を引数にとるイベントアセット|引数にAnimatorのfloat型パラメータを使用|
|GameEventInfoBool[]|OnStateEnterBool|int型を引数にとるイベントアセット|引数にAnimatorのbool型パラメータを使用|
|UnityEvent|OnStateEnterEvent|メソッドを指定|引数を固定してイベントを呼びたい場合などに使用可能|

### State終了時にイベントを呼ぶ

|type|name|description|note|
|-|-|-|-|
|GameEvent|OnStateExit|引数なしのイベントアセット||
|GameEventInfoInt[]|OnStateExitInt|int型を引数にとるイベントアセット|引数にAnimatorのint型パラメータを使用|
|GameEventInfoFloat[]|OnStateExitFloat|int型を引数にとるイベントアセット|引数にAnimatorのfloat型パラメータを使用|
|GameEventInfoBool[]|OnStateExitBool|int型を引数にとるイベントアセット|引数にAnimatorのbool型パラメータを使用|
|UnityEvent|OnStateExitEvent|メソッドを指定|引数を固定してイベントを呼びたい場合などに使用可能|

## 使用例

### State開始時に音を鳴らす

PublishOnStateを使用することでアニメーションとロジックを紐づけることが可能です。
例えば、キャラクターの攻撃モーションに合わせて音を鳴らしたり、エフェクト用のゲームオブジェクトを生成することができます。

ここでは、State遷移時に音を鳴らしてみましょう。
まずはAnimatorControllerのStateを選択してPublishOnStateをアタッチします。
そして、OnStateEnterEventに[AudioPlayer][page:AudioPlayer]のイベントアセットであるPlaySEを設定すれば音を鳴らすことができます。

![State開始時に効果音を鳴らす][fig:PlaySEOnState]

※引数で指定している"RollBall_GameClear"はサンプルゲームで使用している効果音です。

同様のことはアニメーション機能でもできますが、呼び出したい処理を記述したスクリプトをゲームオブジェクトにアタッチする必要があります。
状況に応じて使い分けるのが良いでしょう。

### ステートマシンとして利用する

UnityのAnimator機能はその名の通り、アニメーションのための機能です。
通常は上記のように音を鳴らしたりといったエフェクト用の処理を呼ぶことが多いと思いますが、
（使い勝手が良いかは置いておいて）有限ステートマシンとして利用することも可能です。
Animatorのパラメータに[変数アセットをバインドさせる][page:BindingVariable]ことでゲームロジックを実装することも可能です。

ここでは、制限時間10秒以内にスコアを10以上獲得したらゲームクリアになるロジックをコーディング無しで組んでみます。

![AnimatorControllerの要素][fig:LogicOutline]

|State|説明|
|-|-|
|Start|初期化処理を行う|
|Playing|ゲームをプレイ中|
|GameClear|ゲームをクリアした|
|GameOver|ゲームオーバーになった|

Triggerが呼ばれるとPlayingに遷移してゲームが開始され、
Timeが0になればGameOver、Scoreが10になればGameClearに遷移するように設定します。

![StartからPlayへの遷移条件][fig:LogicStartToPlay]
![PlayからGameOverへの遷移条件][fig:LogicPlayToGameOver]
![PlayからGameClearへの遷移条件][fig:LogicPlayToGameClear]

※遷移条件は\>, \<, =の3つで≧などが選べないため、「0.0001より小さい」や「9より大きい」としています。

Time, Score, IsPlayingのパラメータに対応する変数を作成します。
Startで変数の値をPublishOnStateを用いて初期化します。

![Startの変数設定][fig:LogicStartState]

プレイ中はIsPlayingの値がtrueになるようにPlayingにもPublishOnStateを設定します。

![Playの変数設定][fig:LogicPlayState]

続いて、コンポーネントを設定します。
Animatorコンポーネントと変数アセットをバインドします。

![Animatorと変数のバインド][fig:LogicAnimatorComponent]

時間の測定は[Timerコンポーネント][page:Timer]を使用します。

![Timerコンポーネントの設定][fig:LogicTimerComponent]

## 実装

UnityEngine.StateMachineBehaviourを継承して、イベントをPublishしているだけです。
例えば、State開始時のイベントは以下のようになります。

```cs
public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    m_onStateEnter?.Publish();
    foreach (var info in m_onStateEnterInt) info?.Publish(animator);
    foreach (var info in m_onStateEnterFloat) info?.Publish(animator);
    foreach (var info in m_onStateEnterBool) info?.Publish(animator);
    m_onStateEnterEvent?.Invoke();
}
```

GameEventInfo~系ではanimatorから値を取得してイベントを呼ぶようになっています。
例えば、intのパラメータを使用する場合はGetIntegerを使用します。

```cs
private class GameEventInfoIntの例
{
    [SerializeField] private string m_parameterName = default;
    [SerializeField] private GameEventInt m_event = default;

    public void Publish(Animator animator)
    {
        var value = animator.GetInteger(m_parameterName);
        m_event?.Publish(value);
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:PlaySEOnState]: Figures/PlaySEOnState.gif
[fig:LogicOutline]: Figures/Logic_Outline.gif
[fig:LogicStartToPlay]: Figures/Logic_StartToPlay.png
[fig:LogicPlayToGameClear]: Figures/Logic_PlayToGameClear.png
[fig:LogicPlayToGameOver]: Figures/Logic_PlayToGameOver.png
[fig:LogicStartState]: Figures/Logic_StartState.png
[fig:LogicPlayState]: Figures/Logic_PlayingState.png
[fig:LogicAnimatorComponent]: Figures/Logic_AnimatorComponent.png
[fig:LogicTimerComponent]: Figures/Logic_TimerComponent.png
