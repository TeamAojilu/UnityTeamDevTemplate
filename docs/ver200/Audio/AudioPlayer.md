# AudioPlayer

class

名前空間：SilCilSystem.Audio

継承：[SingletonMonoBehaviour\<T>][page:SingletonMonoBehaviour]

---

音楽の再生を行うためのシングルトンです。
BGMの再生と効果音の再生をサポートしています。
効果音に関しては同時再生数に制限があるほか、再生場所指定による3Dサウンド効果の利用が可能です。

## メンバ一覧

### メソッド

|member|description|
|-|-|
|static void PlayBGM(AudioClip clip)|BGMを再生/変更する|
|static void PlaySE(AudioClip clip, Vector3 worldPosition)|SEを再生する, positionの規定値はVector3.zero|

※PlayBGMにnullを渡すと音量0にフェードアウトします。

### 設定項目

#### 音量

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyFloat|DefaultBGMVolume|BGMの音量|フェード中はこの値より小さくなる|
|ReadonlyPropertyFloat|DefaultSEVolume|SEの音量||

#### BGM設定

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyBool|Loop|trueならループ再生||
|ReadonlyPropertyFloat|FadeTime|BGM切り替え時のフェード時間|単位は秒|
|InterpolationCurve|FadeCurve|BGM切り替え時のフェードに使う関数||

#### 音源

|type|name|description|note|
|-|-|-|-|
|AudioSource|BgmSource|BGM再生用のAudioSource||
|AudioSource[]|SeSources|SE再生用のAudioSource|設定した数が同時再生数の限界になります|

※2D/3Dサウンド, AudioMixerの設定はそれぞれのAudioSourceに対して行ってください。

## 使用例

基本的にPlayBGMとPlaySEを呼べばいいです。
音を再生したいタイミングでPlayBGM/PlaySEを呼びましょう。
例えば、以下はキーを押すたびに効果音を再生するスクリプトです。

```cs
using UnityEngine;
using SilCilSystem.Audio;

public class TestAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip m_clip = default;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // キーを押すたびに音を鳴らす.
            AudioPlayer.PlaySE(m_clip);
        }
    }
}
```

## 実装

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

SE再生は再生していないAudioSourceを探し出してPlayを呼び出します。

```cs
void _PlaySE(AudioClip clip, Vector3 worldPosition)
{
    foreach (var item in m_seSources)
    {
        if (item == null || item.isPlaying) continue;
        item.clip = clip;
        item.transform.position = worldPosition;
        item.Play();
        return;
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:AudioList]: Figures/AudioList.gif
