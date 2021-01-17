# PublishOnState

class

名前空間：SilCilSystem.StateMachines

継承：UnityEngine.StateMachineBehaviour

Assembly：SilCilSystem.Packages

---

アニメーションステート遷移時にイベントを発行するクラスです。
Unityのアニメーション機能と[イベントアセット][page:GameEvent]を連携させることが可能です。

`PublishOnState`を使用することでアニメーションとロジックを紐づけることが可能です。
例えば、キャラクターの攻撃モーションに合わせて音を鳴らしたり、エフェクト用のゲームオブジェクトを生成することができます。

## 設定項目

ステートの開始時と終了時のタイミングで処理を呼ぶことができます。
引数なしのイベントを呼ぶ場合は、`GameEvent`, `UnityEngine.Events.UnityEvent`が利用できます。
引数ありのイベントを呼ぶ場合には、以下の2通りが可能です。

- 引数を固定値でイベントを呼ぶ -> `UnityEvent`でイベントアセットを`Publish`
- 引数に`Animator`のパラメータを使用する -> `GameEventInfoInt`, `GameEventInfoFloat`, `GameEventInfoBool`を使用

※`GameEventInfo~`系はインスペクタ上での設定を可能にするためのサブクラスです。

### ステート開始時にイベントを呼ぶ

|type|name|description|note|
|-|-|-|-|
|GameEvent|OnStateEnter|引数なしのイベントアセット||
|GameEventInfoInt[]|OnStateEnterInt|int型を引数にとるイベントアセット|引数にAnimatorのint型パラメータを使用|
|GameEventInfoFloat[]|OnStateEnterFloat|int型を引数にとるイベントアセット|引数にAnimatorのfloat型パラメータを使用|
|GameEventInfoBool[]|OnStateEnterBool|int型を引数にとるイベントアセット|引数にAnimatorのbool型パラメータを使用|
|UnityEvent|OnStateEnterEvent|メソッドを指定|引数を固定してイベントを呼びたい場合などに使用可能|

### ステート終了時にイベントを呼ぶ

|type|name|description|note|
|-|-|-|-|
|GameEvent|OnStateExit|引数なしのイベントアセット||
|GameEventInfoInt[]|OnStateExitInt|int型を引数にとるイベントアセット|引数にAnimatorのint型パラメータを使用|
|GameEventInfoFloat[]|OnStateExitFloat|int型を引数にとるイベントアセット|引数にAnimatorのfloat型パラメータを使用|
|GameEventInfoBool[]|OnStateExitBool|int型を引数にとるイベントアセット|引数にAnimatorのbool型パラメータを使用|
|UnityEvent|OnStateExitEvent|メソッドを指定|引数を固定してイベントを呼びたい場合などに使用可能|

## 使用例

### ステートマシンとして利用する

Unityの`Animator`はその名の通り、アニメーションのための機能です。
通常は上記のように音を鳴らしたりといったエフェクト用の処理を呼ぶことが多いと思いますが、
（使い勝手が良いかは置いておいて）有限ステートマシンとして利用することも可能です。
`Animator`のパラメータに[変数アセットをバインドさせる][page:BindingVariable]ことでゲームロジックを実装することも可能です。
（正直お勧めはしませんが。）

ここでは、制限時間10秒以内にスコアを10以上獲得したらゲームクリアになるロジックをコーディング無しで組んでみます。

![AnimatorControllerの要素][fig:LogicOutline]

|State|説明|
|-|-|
|Start|初期化処理を行う|
|Playing|ゲームをプレイ中|
|GameClear|ゲームをクリアした|
|GameOver|ゲームオーバーになった|

トリガーが呼ばれると`Playing`に遷移してゲームが開始され、
`Time`が0になれば`GameOver`、`Score`が10になれば`GameClear`に遷移するように設定します。

![StartからPlayへの遷移条件][fig:LogicStartToPlay]
![PlayからGameOverへの遷移条件][fig:LogicPlayToGameOver]
![PlayからGameClearへの遷移条件][fig:LogicPlayToGameClear]

※遷移条件は\>, \<, =の3つで≧などが選べないため、「0.0001より小さい」や「9より大きい」としています。

`Time`, `Score`, `IsPlaying`のパラメータに対応する変数を作成します。
`Start`で変数の値を`PublishOnState`を用いて初期化します。

![Startの変数設定][fig:LogicStartState]

プレイ中は`IsPlaying`の値が`true`になるように`Playing`にも`PublishOnState`を設定します。

![Playの変数設定][fig:LogicPlayState]

続いて、コンポーネントを設定します。
`Animator`コンポーネントと変数アセットをバインドします。

![Animatorと変数のバインド][fig:LogicAnimatorComponent]

時間の測定は[Timerコンポーネント][page:Timer]を使用します。

![Timerコンポーネントの設定][fig:LogicTimerComponent]

## 実装

`UnityEngine.StateMachineBehaviour`を継承して、イベントを実行しています。
例えば、ステート開始時のイベントは以下のようになります。

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

`GameEventInfo~`系では`Animator`から値を取得してイベントを呼ぶようになっています。
例えば、`int`のパラメータを使用する場合は`GetInteger`を使用します。

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
