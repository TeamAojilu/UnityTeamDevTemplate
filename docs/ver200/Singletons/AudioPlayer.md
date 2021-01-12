# AudioPlayer

class

名前空間：SilCilSystem.Singletons

継承：SingletonMonoBehaviour\<T>

---

音楽の再生を行うためのシングルトンです。
音源の場所指定はできないため、プレイヤーとの距離によって音量を変えるという処理はできません。
したがって、2DゲームやBGMやシステム音向けです。

## メンバ一覧

### プロパティ

|member|description|
|-|-|
|IAudioClipResources Clips|AudioClipを取得するためのインターフェースを指定|

### メソッド

|member|description|
|-|-|
|static void PlayBGM(string name)|BGMを再生/変更する|
|static void PlaySE(string name)|SEを再生する|

※PlayBGMに無効な文字列を指定すると音量0にフェードアウトします。

### 設定項目

#### 音量

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyFloat|m_defaultBGMVolume|BGMの音量|フェード中はこの値より小さくなる|
|ReadonlyPropertyFloat|m_defaultSEVolume|SEの音量||

#### BGM設定

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyBool|m_loop|trueならループ再生||
|ReadonlyPropertyFloat|m_fadeTime|BGM切り替え時のフェード時間|単位は秒|
|InterpolationCurve|m_fadeCurve|BGM切り替え時のフェードに使う関数||

#### 音源

|type|name|description|note|
|-|-|-|-|
|AudioSource|m_bgmSource|BGM再生用のAudioSource||
|ReadonlyPropertyFloat|m_seSource|SE再生用のAudioSource||

#### イベント

|type|name|description|note|
|-|-|-|-|
|GameEventStringListener|m_playBGM|PlayBGMを呼ぶイベントアセット||
|GameEventStringListener|m_playSE|PlaySEを呼ぶイベントアセット||

## 使用例

基本的にPlayBGMとPlaySEを呼べばいいです。
引数に指定する文字列はIAudioClipResourcesで指定する文字列と同じです。
デフォルトではResourcesフォルダに置いてあるAudioListで指定します。

![AudioListを設定する][fig:AudioList]

あとは音を再生したいタイミングでPlayBGM/PlaySEを呼びましょう
例えば、以下はキーを押すたびに効果音を再生するスクリプトです。

```cs
using UnityEngine;
using SilCilSystem.Singletons;

public class TestAudioPlayer : MonoBehaviour
{
    [SerializeField] private string m_name = "Test";

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // キーを押すたびに音を鳴らす.
            AudioPlayer.PlaySE(m_name);
        }
    }
}
```

## 使用上の注意点

デフォルトではAudioMixerを使用していません。
AudioMixerを使用する場合はm_defaultBGMVolumeやm_defaultSEVolumeなどを変更しないようにするほうがいいと思います。

## 実装

SE再生はPlayOneShotを呼び出しているだけです。

```cs
void _PlaySE(string name)
{
    var clip = Clips?.GetClip(name);
    m_seSource.PlayOneShot(clip);
}
```

BGM再生は次に再生するべきAudioClipを保持した状態で音量を0に変化させていきます。

```cs
void _PlayBGM(string name)
{
    m_nextClip = Clips?.GetClip(name);
    m_multiply = -1f;
}
```

音量が0になったら、AudioClipを差し替えます。

```cs
private void Update()
{
    // 他の処理...

    // BGMの音量が0になったら、次のBGMに切り替え.
    if(m_volumeRate == 0f && m_nextBGM != null)
    {
        m_bgmSource.clip = m_nextBGM;
        m_bgmSource.Play();
        m_multiply = 1f;
        m_nextBGM = null;
    }

    // 他の処理...
}
```

### IAudioClipResourcesの実装

AudioClipはIAudioClipResourcesインターフェースを用いて取得しているので、
独自の処理を実装して指定できます。
Resourcesからロードしたり、Addressableを用いたりなど、状況に応じてカスタマイズできます。

IAudioClipResourcesは以下のメソッドが定義されたインターフェースです。

|member|description|
|-|-|
|AudioClip GetClip(string name)|nameで指定されたAudioClipを返す|

例として、AudioClipをResourcesフォルダからロードして返すClipLoaderを作ってみます。

```cs
using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Singletons;

public class ClipLoader : IAudioClipResources
{
    private Dictionary<string, AudioClip> m_clips = new Dictionary<string, AudioClip>();

    public AudioClip GetClip(string name)
    {
        if (m_clips.ContainsKey(name))
        {
            return m_clips[name];
        }
        else
        {
            // clipを取得.
            AudioClip clip = Resources.Load<AudioClip>(name);
            m_clips[name] = clip;
            return clip;
        }
    }
}
```

あとは作成したクラスをAudioPlayerのClipsに指定すれば使用できます。
適当なスクリプトで設定しましょう。

```cs
using UnityEngine;
using SilCilSystem.Singletons;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        // AudioPlayerに設定.
        AudioPlayer.Instance.Clips = new ClipLoader();
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:AudioList]: Figures/AudioList.gif
