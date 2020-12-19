# SilCilSystem

Unityでゲームを作るのに役に立つかもしれない汎用システム「SilCilSystem」のドキュメントです。

## 要点

SilCilSystemは変数オブジェクトとイベントオブジェクトにより成り立つシステムです。
異なるスクリプト間で変数の値を共有したり、特定のタイミングで処理を呼び出すことができます。
変数/イベントオブジェクトはScriptableObjectを継承したクラスで表現されているため、インスペクタ上での設定が可能です。

変数とイベントの使い方を学べば「SilCilSystem完全に理解した」と言えるでしょう。重要なのは以下の3つです。

- [変数オブジェクト][page:Variable]
- [イベントオブジェクト][page:GameEvent]
- [変数の値が変化したときに処理を呼ぶ][page:OnValueChanged]

今まで他のスクリプトに依存していた部分を変数/イベントオブジェクトへの依存に置き換えることが可能です。
そうすることで得られるメリットは以下の通りです。

- スクリプト間の結合が弱まり、再利用可能なスクリプトが生まれやすくなります。
- 設定はScriptableObjectを介するため、機能ごとに独立したプレハブの作成が可能です。これはシーンの編集を最小限にし、共同作業が行いやすくなります。
- ScriptableObjectなので、異なるシーン間での連携が可能です。前のシーンの結果を受け取ったり、マルチシーン機能で活用したりできます。

当然ですが、デメリットもあります。

- 管理する変数やイベントの数が増えるほど、プロジェクトの複雑度が増加します。
- 再利用可能なように機能を細かく分割したスクリプトを作成すると、Editor上での作業量が増えます。

頼りすぎるとマウス操作が多くなり、ビジュアルスクリプティングのようになっていくでしょう。
デザイナーには易しいかもしれませんが、プログラマには煩わしいものになるに違いありません。
ある程度の規模の開発に使用する場合は注意しましょう。
変数/イベントオブジェクトの利用は異なる機能間での連携にとどめて置くのが良いと思います。

## 機能一覧

SilCilSystemの他の要素は主に変数オブジェクトとイベントオブジェクトを使用した汎用的なクラスの集合です。

あなたが実装しようとしている機能はここに載っているかもしれません。
コードを書く前にチェックしてみてください。

### 変数関連

---

先ほど、重要なのは3つと言いましたが、コンポーネントを書くときはPropertyも使えると便利です。

- [インスペクタ上で変数オブジェクトの使用/不使用を切り替えられるようにする][page:Property]

#### 変数のset

- [時間を測定する/一定間隔で処理を行う][page:Timer]
- [Timelineで変数の値を変える][page:ChangeVariableInTimeline]
- [Tweenコルーチンを用いて変数の値を時間変化させる][page:TweenVariableCoroutine]

#### 変数のget

- [変数の値を表示する][page:DisplayVariables]
- [変数の値をUIやAnimatorのパラメータに反映させる][page:BindingVariable]

### イベント関連

---

#### イベントの実行

- [Animatorの遷移時にイベントを呼ぶ][page:PublishOnState]

#### イベントの登録

- [イベントでゲームオブジェクトのActiveやコンポーネントのenabledを切り替える][page:Activator]
- [DisposeメソッドをゲームオブジェクトのDestroy時に呼ぶ][page:DisposeOnDestroy]
- [Disposeメソッドをシーンのアンロード時に呼ぶ][page:DisposeOnSceneUnLoaded]
- [複数のIDisposableを1つにまとめる][page:CompositeDisposable]

### シングルトン

---

グローバルな処理として利用されると思われる機能はシングルトンで用意しています。

- [シングルトンの実装][page:SingletonMonoBehaviour]
- [シーンのロード][page:SceneLoader]
- [音楽の再生][page:AudioPlayer]

作成したシングルトンはゲーム開始時に自動で生成されるようにEditor拡張されています。
Resources/InitialPrefabsフォルダ下に置かれている全てのPrefabが生成されるようになっています。

![InitialPrefabs][fig:InitialPrefabs]

※生成処理はPrefabGeneratorOnLoad.csに記述してあります。

### Math

---

変数/イベントオブジェクトとの直接的な関係はないですが、補間曲線の取得などの数学に関するものです。
SilCilSystemのコンポーネントなどで使用されています。

- [イージング関数を取得/設定する][page:InterpolationCurve]
- [floatからintへの変換方法を指定する][page:FloatToInt]

## ログ

Unity2019.4で動作確認

---

- 2020.11.14 変数/イベントオブジェクト追加（SilCilSystemの開始）
- 2020.11.22 SceneLoader追加
- 2020.11.30 Property追加
- 2020.12.01 AudioPlayer追加
- 2020.12.13 UI関連のスクリプト追加
- 2020.12.19 拡張メソッドDisposeOnDestroy追加

<!--- footer --->

{% include footer.md %}

<!--- 参照 --->

{% include paths.md %}

[fig:InitialPrefabs]: Figures/InitialPrefabs.png
