---
layout: page
title: "Virtual Motion Capture Protocol (VMCProtocol 公式ページ)"
subtitle: "ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true
#Easy to use motion capture protocol in your game, tool, live, and everywhere.
---
Virtual Motion Capture Protocolは**[バーチャルモーションキャプチャー](https://vmc.info/)**の情報を送受信するためのプロトコルです。( VMCプロトコル 、ばもきゃプロトコル、OSC/VMC Protocol と呼ばれることもあります。)  
ハッシュタグは **[#VMCProtocol](https://twitter.com/search?q=%23VMCProtocol)**

**[English](/english)**

# ロゴ
VMC Protocol対応アプリケーションは以下のロゴを使用することができます。  

![logo](vmpc_logo_128x128.png)  
[より大きなサイズの画像が必要な場合はこちらからダウンロードできます。](vmpc_logo_1024x1024.png)

# 簡単に使い始められる
Virtual Motion Capture Protocolは、VTuberの人々がVR機器の高度な知識を要すること無くVR撮影環境を自作することを可能とするために作成されました。  
高度な技術が要求されるアバターの制御、VR機器の取り扱いを**[バーチャルモーションキャプチャー](https://vmc.info/)**に任せ、  
利用者は以下の受信アセット/アドオンを使うだけでとてもシンプルにアバター撮影環境を構築することができます。
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity向けアセット
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine向けアセット
- **[VMC4B Blender addon for VMCProtocol](https://tonimono.booth.pm/items/3432915)** - Blender向けモーション受信アドオン

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

# 本プロトコルを受信する利点
- 簡単に使える
- バーチャルモーションキャプチャー他のソフトにアバターの動きを任せることができる
- 新デバイスが出た際などにも、送信元ソフトが対応すれば他に対応する必要がない

# 本プロトコルを送信する利点
- 様々なアプリケーションの表示機能や拡張を利用できる

# 実装が公開されており、実績あり
すでに13作品以上で利用されています。また、リファレンス実装はすべてオープンソースであり、実際に動く作品の中身を確認することができます。

# お問い合わせ
本ページに関して問題がある場合、[Issue](https://github.com/sh-akira/VirtualMotionCaptureProtocol/issues)よりお知らせください。  

Virtual Motion Capture Protocolは、[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)の製作者であるgpsnmeajpが主にメンテナンスしています。  

**注意: EVMC4UおよびVMCProtocolの製作者(gpsnmeajp)は、バーチャルモーションキャプチャーの製作者(sh-akira)ではありません。バーチャルモーションキャプチャーのdiscordはFanbox/Patreonに記載されています。**  
**Note: The creator of EVMC4U and VMCProtocol (gpsnmeajp) is not the creator of Virtual Motion Capture (sh-akira). The virtual motion capture discord is listed on Fanbox/Patreon.**

[お問い合わせはEVMC4UのDiscordへお願いします。](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity/wiki/Discord)
