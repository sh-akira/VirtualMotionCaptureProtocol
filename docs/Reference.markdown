---
layout: page
title:  "VMC Protocol リファレンス実装と使用例"
subtitle: "利用可能なアプリケーションをご紹介します"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true

---

# リファレンス実装
- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://sh-akira.github.io/VirtualMotionCapture/) - VR機器でVRMの3Dモデルをコントロール(ExternalSenderフォルダ内のスクリプト)
- [EVMC4U(受信ライブラリ)](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity) - Unity向けモーション受信アセット(すべてのReceiver系スクリプト)

# 採用事例

# Performer(モーション送信)アプリケーション
- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://sh-akira.github.io/VirtualMotionCapture/) - 汎用VR機器(OpenVR)を用いたモーションキャプチャー。3点から10点トラキングまで対応。視線トラッキングデバイスなどにも対応。
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ。VMCと併用可能
- [360KinectGum](https://daifuklana.booth.pm/items/2109279) - Kinect v1を用いたフルボディモーションキャプチャ。
- [ThreeDPoseTracker](https://qiita.com/yukihiko_a/items/43d09db5628334789fab) - USBカメラと機械学習を用いたフルボディモーションキャプチャー。

# Assistant(拡張情報送信)アプリケーション
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ。VMCと併用可能
- [QuestOSCTransformSender](https://github.com/sh-akira/QuestOSCTransformSender) - Oculus Quest OSC transform sender for VirtualMotionCapture

# Marionette(モーション受信)アプリケーション
### ライブラリ
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity向けモーション受信アセット
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine向けモーション受信プラグイン

### 映像配信・収録ツール
- [Virtual Streamer 360](https://booth.pm/ja/items/1702492) - VR360°配信用のソフトウェア
- [だれでもVsinger](https://honokakaori.booth.pm/items/1768267) - 生歌配信、もしくはMVを一発撮りするために作られたものです
- [Collaboll(コラボル)](https://brother-pv.booth.pm/items/2016717) - VRMモデルとネットワーク通信で、複数人数でバーチャルキャラクター、所謂Vtuber動画を収録する事が出来るアプリです。
- [Oredayo4V](https://github.com/gpsnmeajp/Oredayo4V) - 主にWaidayoの撮影を支援するために作成された高性能VMCProtocolビューア

### 映像支援ツール
- [VMC-Websocket-OBS](https://github.com/gpsnmeajp/VMC-Websocket-OBS) - VMCProtocolを用いてバーチャルモーションキャプチャーの状態を取得し、Websocketを用いてOBSを制御するソフトウェア
- [ゆかりねっとコネクター](https://www.machanbazaar.com/%e3%82%86%e3%81%8b%e3%82%8a%e3%81%ad%e3%81%a3%e3%81%a8%e3%82%b3%e3%83%8d%e3%82%af%e3%82%bf%e3%83%bc/) - 話した言葉を字幕にして、多言語翻訳まで出来る配信支援アプリです。(キャリブレーション開始コマンドの発行に対応。YNC独自拡張仕様により字幕表示もサポート)

### ゲーム
- [パイロットクロス(PilotXross)](https://n-mattun.booth.pm/items/1997616) - VR機器向けに開発したVRフライトゲームです
- [VMCAvatar-BS](https://github.com/nagatsuki/VMCAvatar-BS) - Beat Saber内にアバターを表示するMod

# 開発者向けツール
- [VMCProtocolMultiplexer](https://github.com/gpsnmeajp/VMCProtocolMultiplexer) - VMCProtocolを分配するソフトウェア。複数の入出力を持ち、配送先を自由に設定することができる。
- [VMCProtocolMonitor](https://github.com/gpsnmeajp/VMCProtocolMonitor) - MarionetteとしてVMCProtocolの受信内容を表示するソフトウェア。 ごく単純に受信内容をコンソールに流すモードと、VMCProtocolに基づいてブラウザに一覧表示するモードがあります。
- [VMCProtocolModelViewer](https://github.com/gpsnmeajp/VMCProtocolModelViewer) - VMCProtocolの受信内容をVMC互換で表示するソフトウェア。バーチャルモーションキャプチャーとほぼ同等の表示を実現しようとします

# 依存ライブラリ
- [UniVRM 0.53(Unity環境 必須)](https://github.com/vrm-c/UniVRM)
- [uOSC(Unity環境 推奨)](https://github.com/hecomi/uOSC)
- [Rug.OSC(C#環境 推奨)](https://bitbucket.org/rugcode/rug.osc/src/master/)

# 解説記事
- [Xbox360版KinectによるVMCProtocolを使用した姿勢情報の送信、VRMモデル操作について @daifuk-lana](https://qiita.com/daifuk-lana/items/c098fe9977c5e1202acb)

本項目は見つけ次第掲載しているものです。本ページへの掲載・削除に関しては[Issue](https://github.com/sh-akira/VirtualMotionCaptureProtocol/issues)よりお知らせください。  
