# SilCilSystem ver2.0

Unity2019.4で動作確認

## 要点

SilCilSystemは変数アセットとイベントアセットにより成り立つシステムです。
異なるスクリプト間で変数の値を共有したり、特定のタイミングで処理を呼び出すことができます。
変数/イベントアセットはScriptableObjectを継承しているため、インスペクタ上での設定が可能です。

変数/イベントアセットの使い方を学べば「SilCilSystem完全に理解した」と言えるでしょう。重要なのは以下の3つです。

- [変数アセット][page:Variable]
- [イベントアセット][page:GameEvent]
- [変数の値が変化したときに処理を呼ぶ][page:OnValueChanged]

また、必須ではありませんが`Property`も使えると便利です。

- [Property - インスペクタ上で変数アセットの使用/不使用を切り替えられるようにする][page:Property]

変数/イベントアセットを使用することで得られるメリットは以下の通りです。

- スクリプト間の結合が弱まり、再利用可能なスクリプトが生まれやすくなります。
- 設定はScriptableObjectを介するため、機能ごとに独立したプレハブの作成が可能です。これはシーンの編集を最小限にし、共同作業が行いやすくなります。
- ScriptableObjectなので、異なるシーン間での連携が可能です。前のシーンの結果を受け取ったり、マルチシーン機能で活用したりできます。

当然ですが、デメリットもあります。

- 管理する変数やイベントの数が増えるほど、プロジェクトの複雑度が増加します。
- 再利用可能なように機能を細かく分割したスクリプトを作成すると、Editor上での作業量が増えます。

頼りすぎるとマウス操作が多くなり、ビジュアルスクリプティングのようになっていくでしょう。
デザイナーには易しいかもしれませんが、プログラマには煩わしいものになるに違いありません。
ある程度の規模の開発に使用する場合は注意しましょう。
変数/イベントアセットの利用は異なる機能間での連携にとどめて置くのが良いと思います。

## 機能一覧

SilCilSystemの他の要素は主に変数/イベントアセットを利用したスクリプトの集合です。

あなたが実装しようとしている機能はここに載っているかもしれません。
コードを書く前にチェックしてみてください。

### ドラッグ&ドロップ

- [スクリプトをアタッチしたゲームオブジェクトを生成する][page:MonoBehaviourDragDrop]
- [変数アセットに関連するゲームオブジェクトを生成する][page:VariableDragDrop]
- [変数アセットを変換する/カスタマイズする][page:VariableAttribute]

### 時間

- [一定時間後/一定間隔で処理を行う][page:TimeMethods]
- [毎フレーム処理を呼ぶ][page:UpdateDispatcher]
- [時間を測定する][page:Timer]
- [Tweenコルーチンを用いて変数の値を時間変化させる][page:TweenVariableCoroutine]
- [Timelineで変数の値を変える][page:ChangeVariableInTimeline]

### 変数アセットのバインド

- [変数アセットの値をUIやAnimatorのパラメータに反映させる][page:BindingVariable]
- [変数アセットの値を表示する][page:DisplayVariables]
- [ゲームオブジェクトのActiveやコンポーネントのenabledを切り替える][page:Activator]
- [Animatorの遷移時にイベントを呼ぶ][page:PublishOnState]

### シーン

- [シーンをロードする][page:SceneLoader]

### 音楽

- [AudioClipを再生する][page:AudioPlayer]

### シングルトン

- [シングルトンの実装][page:SingletonMonoBehaviour]

### オブジェクトプール

- [オブジェクトプール][page:ObjectPool]

### 数学

- [イージング関数を取得/設定する][page:InterpolationCurve]
- [floatからintへの変換方法を指定する][page:FloatToInt]
- [値の比較方法を指定する][page:Comparison]

### IDisposable

- [ゲームオブジェクトが破棄されたときにDisposeを呼ぶ][page:DisposeOnDestroy]
- [シーン遷移時にDisposeを呼ぶ][page:DisposeOnSceneUnLoaded]
- [IDisposableを生成する][page:DelegateDispose]
- [複数のIDisposableを1つにまとめる][page:CompositeDisposable]

### エディタ拡張

- [MonoBehaviourDragDrop][page:MonoBehaviourDragDrop]
- [VariableDragDrop][page:VariableDragDrop]
- [Variable, OnAttached][page:VariableAttribute]
- [NonEditable][page:NonEditableAttribute]
- [EnumLabel][page:EnumLabelAttribute]

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
