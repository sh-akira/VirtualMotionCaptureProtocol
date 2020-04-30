---
layout: page
title: "Virtual Motion Capture Protocol - Easy to use motion capture protocol in your game, tool, live, and everywhere."
---
Virtual Motion Capture Protocolは**[バーチャルモーションキャプチャー](https://sh-akira.github.io/VirtualMotionCapture/)**の情報を送受信するためのプロトコルです。

# 簡単に使い始められる
Virtual Motion Capture Protocolは、VTuberの人々がVR機器の高度な知識を要すること無く、自前のVR撮影環境を作成できるように作成されました。  
高度な技術が要求されるアバターの制御、VR機器の取り扱いを**[バーチャルモーションキャプチャー](https://sh-akira.github.io/VirtualMotionCapture/)**に任せ、  
利用者は以下の受信ライブラリを使うだけでとてもシンプルにアバター撮影環境を構築することができます。
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity向け
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine向け

また、アバター撮影環境だけではなく、ゲームや、研究などにおいても、既存のアプリケーションに容易に組み込むことが可能です。

もちろん、性能の向上や独自の仕様を実現するために公開されたプロトコルから実装することも可能です。  
受信側のみならず、送信側を拡張・新規に作成することもできます。  
(VR機器の代わりにiPhoneのフェイシャルキャプチャを用いる**[waidayo](https://booth.pm/ja/items/1779185)**の例があります。)

{% include youtubePlayer.html id="DunqgLrUfpI" %}

# シンプルな実装で広い環境に対応
オープンな規格であるOpen Sound ControlとVRMを前提とし、シンプルな数値・文字列情報の送受信のみで構成されています。  
そのため、幅広い環境で動作します。(UnityではWindows, Mac, iOS、またUE4でも利用例があります。)

プロトコルはMITライセンスです。

# 幅広い情報を扱える
基本的な情報である
VRM規格で作成されたVRモデルの
- ボーン情報
- BlendShape情報
の送受信の他

- カメラ制御
- ライト制御
- キーボード入力
- MIDI Note/CC入力
- トラッカー位置
- VRM動的ロード
- キャリブレーション制御
- 設定
- 視線制御

などの高度な情報の送受信にも対応しています。

もちろんこれらの情報の全てに対応する必要はなく、使いたい情報のみ対応して使用することができます。

# 実装が公開されており、実績あり
すでに7作品以上で利用されています。また、リファレンス実装はすべてオープンソースであり、実際に動く作品の中身を確認することができます。

# お問い合わせ
本ページに関して問題がある場合、[Issue](../)よりお知らせください。  

Virtual Motion Capture Protocolは、[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)の製作者であるgpsnmeajpが主にメンテナンスしています。  
[お問い合わせはEVMC4UのDiscordへお願いします。](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity/wiki/Discord)
