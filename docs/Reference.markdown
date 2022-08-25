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
- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://vmc.info/) - VR機器でVRMの3Dモデルをコントロール(ExternalSenderフォルダ内のスクリプト)
- [EVMC4U(受信ライブラリ)](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity) - Unity向けモーション受信アセット(すべてのReceiver系スクリプト)

# 採用事例

# Performer(モーション送信, 姿勢ボーン)アプリケーション
- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://vmc.info/) - 汎用VR機器(OpenVR)を用いたモーションキャプチャー。3点から10点トラキングまで対応。視線トラッキングデバイスなどにも対応。
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ。VMCと併用可能
- [360KinectGum](https://daifuklana.booth.pm/items/2109279) - Kinect v1を用いたフルボディモーションキャプチャ。
- [ThreeDPoseTracker](https://digital-standard.booth.pm/items/3698596) - USBカメラと機械学習を用いたフルボディモーションキャプチャー(Windows)
- [TDPT](https://digital-standard.com/tdptios/) - iPhone単体で機械学習を用いたフルボディモーションキャプチャーを実現(iOS)
- [VSeeFace](https://www.vseeface.icu/) - VSeeFace is a free, highly configurable face and hand tracking VRM avatar puppeteering program.
- [MocapForAll](https://vrlab.akiya-souken.co.jp/product) - PCと複数のウェブカメラによるフルボディモーションキャプチャ
- [Keyboard Stuvio](https://natsunatsu.booth.pm/items/2956377) - WebカメラとMIDIキーボードによる演奏からモーションキャプチャ
- [Webcam Motion Capture](https://webcammotioncapture.info/ja/index.php) - Webカメラや動画による顔・表情・指および腕のモーションキャプチャ。Windows/Mac対応
- [MocapForStreamer](https://akiya-souken.booth.pm/items/3945408) - Webカメラ2台を用いた上半身の簡易モーションキャプチャ
- [VroidPoser](https://github.com/NeilioClown/VroidPoser) - VroidPoser - a pose creator/animator for VSeeFace

# Assistant(拡張情報送信, 表情・視線・一部ボーンなどのみ)アプリケーション
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ(表情・視線情報)
- [QuestOSCTransformSender](https://github.com/sh-akira/QuestOSCTransformSender) - Oculus Quest OSC transform sender for VirtualMotionCapture (トラッカー姿勢情報)
- [Simple Motion Tracker](https://yuki-natsuno-vt.github.io/SimpleMotionTraker/) - Webカメラを使用した顏認識によるヘッドトラッキングやアイトラッキング(トラッカー姿勢情報・視線情報)
- [VMCOculus](https://github.com/denpadokei/VMCOculus) - Oculus版Beat SaberでVMCの頭が動かなくなる問題を解決するMOD(トラッカー姿勢情報)
- [Sknuckle](https://sknuckle.pachelam.com/) - NOITOM Hi5 VR GLOVEでのハンドトラッキングを行う(手ボーン)
- [VMCProtocolRotationCamera](https://github.com/gpsnmeajp/VMCProtocolRotationCamera) - VMCProtocolでバーチャルモーションキャプチャーのカメラを回すだけのツール(カメラ制御)
- [Uni-studio](https://unimotioninfo.wixsite.com/guide) - 全身フルトラッキングモーションシステム「Uni-motion」(トラッカー姿勢情報)
- [kaodayo](https://booth.pm/ja/items/3281659) - iFacialMocap通信仕様をVMCProtocolに変換し、表情情報を送信する

# Marionette(モーション受信)アプリケーション
### ライブラリ/アドオン
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity向けモーション受信アセット(UniVRMと併用して使用する)
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine向けモーション受信プラグイン(VRM4Uと併用して使用する)
- **[VRM4U](https://ruyo.github.io/VRM4U/)** - Unreal Engine 4で動作するVRMファイルのインポーター(単体で対応、必要に合わせてVMC4UEと使い分け)
- **[VMC4B Blender addon for VMCProtocol](https://tonimono.booth.pm/items/3432915)** - Blender向けモーション受信アドオン
- [hVMCP](https://github.com/Cj-bc/hVMCP) - Haskell版VMCProtocol実装(GPL)

### 映像配信・収録ツール
- [Virtual Streamer 360](https://booth.pm/ja/items/1702492) - VR360°配信用のソフトウェア
- [だれでもVsinger](https://honokakaori.booth.pm/items/1768267) - 生歌配信、もしくはMVを一発撮りするために作られたものです
- [Collaboll(コラボル)](https://brother-pv.booth.pm/items/2016717) - VRMモデルとネットワーク通信で、複数人数でバーチャルキャラクター、所謂Vtuber動画を収録する事が出来るアプリです。
- [Oredayo4V](https://github.com/gpsnmeajp/Oredayo4V) - 主にWaidayo向けな高性能VMCProtocolビューア(Windows専用)
- [Oredayo4M](https://github.com/gpsnmeajp/Oredayo4M) - 主にWaidayo向けのクロスプラットフォームな高性能VMCProtocolビューア(Win, Mac, Linux対応)
- [VSeeFace](https://www.vseeface.icu/) - VSeeFace is a free, highly configurable face and hand tracking VRM avatar puppeteering program.
- [VtubeReflect](https://oose.itch.io/vtubereflect) - VtubeReflect captures your desktop and projects light onto your vtuber avatar.
- [VMCSaberTraining](https://fubukisakura.booth.pm/items/2374515) - きれいなセーバーを振り回せるツール
- [VRM Posing Desktop](https://store.steampowered.com/app/1895630/VRM/) - VRMファイルから3Dモデルにポージングさせることができるツール
- [Horror Light](https://halfsode.booth.pm/items/3558017) - 暗い部屋でホラーゲームをやっているようなライティングに特化したアバター映像を出力するツール
- [VUP](https://store.steampowered.com/news/app/1207050/view/3091152381741795245) - VTuber & Animation & motion capture & 3D & Live2D
- [VRoom](https://ojousamaya.booth.pm/items/3949561) - Vtuber向け3D配信部屋アプリ
- [VTuber Plus](https://vtuberplus.com/) - VTuber Plus is a highly customizable tool that allows Twitch viewers to interact with streamers!

### 映像支援ツール
- [VMC-Websocket-OBS](https://github.com/gpsnmeajp/VMC-Websocket-OBS) - VMCProtocolを用いてバーチャルモーションキャプチャーの状態を取得し、Websocketを用いてOBSを制御するソフトウェア
- [ゆかりねっとコネクター](https://www.machanbazaar.com/%e3%82%86%e3%81%8b%e3%82%8a%e3%81%ad%e3%81%a3%e3%81%a8%e3%82%b3%e3%83%8d%e3%82%af%e3%82%bf%e3%83%bc/) - 話した言葉を字幕にして、多言語翻訳まで出来る配信支援アプリです。(キャリブレーション開始コマンドの発行に対応。YNC独自拡張仕様により字幕表示もサポート)
- [VMCbroadcaster_v0.1](https://izm.fanbox.cc/posts/1301580?utm_campaign=manage_post_page&utm_medium=share&utm_source=twitter) - バモキャの映像を見ながらカメラマンが頑張るソフト
- [vmc2bvh](https://github.com/infosia/vmc2bvh) - バーチャルモーションキャプチャーからBVH (Biovision Hierarchy)モーションファイルを生成するツール

### ゲーム
- [パイロットクロス(PilotXross)](https://n-mattun.booth.pm/items/1997616) - VR機器向けに開発したVRフライトゲームです
- [VMCAvatar-BS](https://github.com/nagatsuki/VMCAvatar-BS) - Beat Saber内にアバターを表示するMod

# 開発者向けツール
- ~~[VMCProtocolMultiplexer](https://github.com/gpsnmeajp/VMCProtocolMultiplexer) - VMCProtocolを分配するソフトウェア。複数の入出力を持ち、配送先を自由に設定することができる。~~ (非推奨。VMCProtocolReflectorを使ってください)
- [VMCProtocolMonitor](https://github.com/gpsnmeajp/VMCProtocolMonitor) - MarionetteとしてVMCProtocolの受信内容を表示するソフトウェア。 ごく単純に受信内容をコンソールに流すモードと、VMCProtocolに基づいてブラウザに一覧表示するモードがあります。
- [VMCProtocolModelViewer](https://github.com/gpsnmeajp/VMCProtocolModelViewer) - VMCProtocolの受信内容をVMC互換で表示するソフトウェア。バーチャルモーションキャプチャーとほぼ同等の表示を実現しようとします。
- [VMCProtocolReflector](https://github.com/gpsnmeajp/VMCProtocolReflector) - VMCProtocolを再配信するソフトウェア。1入力多出力で、1つのモーションや表情データを、複数のVMCProtocol対応ソフトウェアに送信することができます。
- [Remote Marionette](https://www.machanbazaar.com/remotemarionette/) - WebRTC(SkyWay)を用いてVMCProtocolを遠隔地に中継するソフトウェア。
- [MotionReplay](https://github.com/emilianavt/MotionReplay) - This is a very simple debugging tool for VMC protocol applications 
- [BlendShapeClip Viewer v1.00](https://halfsode.booth.pm/items/4017038) - パーフェクトシンクの現象再現・修正支援ツール

# 依存ライブラリ
- [UniVRM 0.53(Unity環境 必須)](https://github.com/vrm-c/UniVRM)
- [uOSC(Unity環境 推奨)](https://github.com/hecomi/uOSC)

# 解説記事
- [Xbox360版KinectによるVMCProtocolを使用した姿勢情報の送信、VRMモデル操作について @daifuk-lana](https://qiita.com/daifuk-lana/items/c098fe9977c5e1202acb)
- [PythonからVMC Protocolでばもきゃに情報を送信してみよう](https://takeda-san.hatenablog.com/entry/2021/12/12/005347
- [How to connect Unity and virtual motion capture with EVMC4U](https://styly.cc/tips/evmc4u_rapturn_virtualmotioncapture/)
- [その他Qiitaの記事](https://qiita.com/tags/vmcprotocol)
- [その他Noteの記事](https://note.com/search?q=VMCProtocol)

# 学術研究
- [安価なVR機器を用いた民族舞踊保存手法の検討 - 電子情報通信学会](https://www.ieice.org/)

本項目は見つけ次第掲載しているものです。本ページへの掲載・削除に関しては[Issue](https://github.com/sh-akira/VirtualMotionCaptureProtocol/issues)よりお知らせください。  
