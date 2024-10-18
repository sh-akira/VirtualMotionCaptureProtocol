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

# リファレンス実装 Reference Implementation
**すべてのVMCProtocolソフトウェアは、下記リファレンス実装と送受信できることを確認することを強く推奨します。**  
**一部のソフトウェアでしか動作しない(例えばボーン情報が破損している)データを送信している例を確認しています。**  
It is highly recommended that all VMCProtocol software be verified to be able to send and receive from/to the reference implementations below.  
We have identified examples of data being sent that only works with some software (e.g., corrupted bone information).

- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://vmc.info/) - VR機器でVRMの3Dモデルをコントロール
- [EVMC4U(受信ライブラリ)](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity) - Unity向けモーション受信アセット【VRM0/VRM1両対応】

# 採用事例 / Usecases

# Performer(モーション送信, 姿勢ボーン)アプリケーション / Bone sender
- [バーチャルモーションキャプチャー - VirtualMotionCapture](https://vmc.info/) - 汎用VR機器(OpenVR)を用いたモーションキャプチャー。3点から10点トラキングまで対応。視線トラッキングデバイスなどにも対応。
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ。VMCと併用可能【VRM0/VRM1両対応】
- [360KinectGum](https://daifuklana.booth.pm/items/2109279) - Kinect v1を用いたフルボディモーションキャプチャ。
- [ThreeDPoseTracker](https://digital-standard.booth.pm/items/3698596) - USBカメラと機械学習を用いたフルボディモーションキャプチャー(Windows)【VRM0/VRM1両対応】
- [TDPT](https://digital-standard.com/tdptios/) - iPhone単体で機械学習を用いたフルボディモーションキャプチャーを実現(iOS)【VRM0/VRM1両対応】
- [VSeeFace](https://www.vseeface.icu/) - VSeeFace is a free, highly configurable face and hand tracking VRM avatar puppeteering program.
- [MocapForAll](https://vrlab.akiya-souken.co.jp/product) - PCと複数のウェブカメラによるフルボディモーションキャプチャ
- [Keyboard Stuvio](https://natsunatsu.booth.pm/items/2956377) - WebカメラとMIDIキーボードによる演奏からモーションキャプチャ
- [Webcam Motion Capture](https://webcammotioncapture.info/ja/index.php) - Webカメラや動画による顔・表情・指および腕のモーションキャプチャ。Windows/Mac対応
- [MocapForStreamer](https://akiya-souken.booth.pm/items/3945408) - Webカメラ2台を用いた上半身の簡易モーションキャプチャ
- [VroidPoser](https://github.com/NeilioClown/VroidPoser) - VroidPoser - a pose creator/animator for VSeeFace
- [Free Webcam Hand Tracking Software with VMC for VSeeFace](https://booth.pm/en/items/4275972) - Web cam hand tracking
- [VRigUnity](https://github.com/Kariaro/VRigUnity) - Unity VRM Visualizer with Mediapipe Holistic integration
- [Tracking World](http://deatrathias.net/TW/) - A tool to manipulate a model using VR
- [VRM Posing Desktop / VRMポージング・デスクトップ (Windows/Mac)](https://store.steampowered.com/app/1895630/VRM/) - VRMファイルから3Dモデルに様々なポージングさせることができるツール。単体での高度な撮影機能の他、姿勢や表情の送信も対応。
- [VRM Posing Mobile / VRMポージング・モバイル (iOS)](https://apps.apple.com/jp/app/vrm-posing-mobile/id1601640655) - VRMファイルから3Dモデルに様々なポージングさせることができるツール。単体での高度な撮影機能の他、姿勢や表情の送信も対応。
- [VRM Posing Mobile / VRMポージング・モバイル (Android)](https://play.google.com/store/apps/details?id=com.soarhap.vrmposing&hl=ja&gl=US&pli=1) - VRMファイルから3Dモデルに様々なポージングさせることができるツール。単体での高度な撮影機能の他、姿勢や表情の送信も対応。
- [XR Animator](https://xr-animator.booth.pm/items/4513654) - AIベース (MediaPipe) の全身モーション キャプチャ
- [LuppetX](https://luppet.jp/) - WebカメラとLeapMotionだけで使える3DバーチャルYoutuber向けトラッキングシステム
- [iFacialMocap Powered by NVIDIA Broadcast](https://apps.microsoft.com/store/detail/ifacialmocap-powered-by-nvidia-broadcast/9N01FGS2ZK3X) - NVIDIA RTXシリーズGPUとNVIDIA ARSDKを用いて、Webカメラによる高度なフェイストラッキングを実現します
- [VMCProtocol Mixer / VMCProtocolミキサー](https://daifuklana.booth.pm/items/4857936) - VMCProtocolで送信された表情やボーンその他の情報を任意に合成し、表示する。有料版では4K画像撮影機能や送信機能あり。【VRM0/VRM1両対応】
- [MCP2VMCP](https://github.com/gpsnmeajp/MCP2VMCP) - mocopiのモーションをVMCProtocolで送信します。【VRM0/VRM1両対応】
- [Dollars MoCap](https://docs.dollarsmocap.com/Dollars-MONO/engines/) - カメラベースのモーションキャプチャーソリューション
- [Rebocap](https://doc.rebocap.com/ja_JP/third_party_software_access/VMC/) - 慣性センサーベースのモーションキャプチャーシステム


# Assistant(拡張情報送信, 表情・視線・一部ボーンなどのみ)アプリケーション / Faicial or other sender
- [waidayo/face2vmc](https://booth.pm/ja/items/1779185) - iPhoneを用いたフェイシャルキャプチャ(表情・視線情報)【VRM0/VRM1両対応】
- [QuestOSCTransformSender](https://github.com/sh-akira/QuestOSCTransformSender) - Oculus Quest OSC transform sender for VirtualMotionCapture (トラッカー姿勢情報)
- [Simple Motion Tracker](https://yuki-natsuno-vt.github.io/SimpleMotionTraker/) - Webカメラを使用した顏認識によるヘッドトラッキングやアイトラッキング(トラッカー姿勢情報・視線情報)
- [VMCOculus](https://github.com/denpadokei/VMCOculus) - Oculus版Beat SaberでVMCの頭が動かなくなる問題を解決するMOD(トラッカー姿勢情報)
- [Sknuckle](https://sknuckle.pachelam.com/) - NOITOM Hi5 VR GLOVEでのハンドトラッキングを行う(手ボーン)
- [VMCProtocolRotationCamera](https://github.com/gpsnmeajp/VMCProtocolRotationCamera) - VMCProtocolでバーチャルモーションキャプチャーのカメラを回すだけのツール(カメラ制御)
- [Uni-studio](https://unimotioninfo.wixsite.com/guide) - 全身フルトラッキングモーションシステム「Uni-motion」(トラッカー姿勢情報)
- [kaodayo](https://booth.pm/ja/items/3281659) - iFacialMocap通信仕様をVMCProtocolに変換し、表情情報を送信する
- [T.I.T.S(Twitch Integrated Throwing System)](https://remasuri3.itch.io/tits) - Twitch統合物投げシステム
- [SVIFT - Suvi's VTuber Integration For Twitch](http://suvidriel.com/) - Allows integration of Twitch to VRChat and VSeeFace through OSC and VMC-protocols
- [Uni-motion / Uni-studio](https://unimotioninfo.wixsite.com/guide/upperbodymode) - IMUトラッカーの情報を仮想トラッカー情報として送信
- [VRCFTtoVMCP](https://github.com/tkns3/VRCFTtoVMCP) - VRCFaceTracking からフェイストラッキングデータを受け取り、そのデータを PerfectSync の Blendshape に変換して VMCProtocol で送信する Windows アプリ
- [ContactGlove](https://docs.diver-x.jp/mocopi/howtouse.html) - 手袋型コントローラから、手首の姿勢情報や指情報、コントローラ情報を送信可能
- [StretchSense Studio Glove](https://stretchsense.my.site.com/defaulthelpcenter26Sep/s/article/VMC-Virtual-Motion-Capture-Streaming-from-Hand-Engine2?language=en_US) - 手袋型キャプチャデバイスから、手首の姿勢情報や指情報を送信可能

# Coordinator(中間加工)アプリケーション / Relay Mix apps
- ~~[VMCProtocolMultiplexer](https://github.com/gpsnmeajp/VMCProtocolMultiplexer) - VMCProtocolを分配するソフトウェア。複数の入出力を持ち、配送先を自由に設定することができる。~~ (非推奨。VMCProtocolReflectorを使ってください)
- **[VMCProtocolReflector](https://github.com/gpsnmeajp/VMCProtocolReflector)** - VMCProtocolを再配信するソフトウェア。1入力多出力で、1つのモーションや表情データを、複数のVMCProtocol対応ソフトウェアに送信することができます。
- [Remote Marionette](https://www.machanbazaar.com/remotemarionette/) - WebRTC(SkyWay)を用いてVMCProtocolを遠隔地に中継するソフトウェア。
- **[VMCProtocol Mixer / VMCProtocolミキサー](https://daifuklana.booth.pm/items/4857936)** - VMCProtocolで送信された表情やボーンその他の情報を任意に合成し、表示する。有料版では4K画像撮影機能や送信機能あり。【VRM0/VRM1両対応】
- [MultiTrackOSCSender](https://booth.pm/zh-tw/items/5167553) - IMU方式のモーションキャプチャーで発生しがちな位置ズレを自動リセットするツール
- [VRMPlaybackClient](https://github.com/kevinjycui/VRMPlaybackClient) - 音声付きあるいはなしでの、VMCProtocolモーションを記録・再生するソフトウェア

# Marionette(モーション受信)アプリケーション / Receiver apps
### ライブラリ/アドオン / Library and addon
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity向けモーション受信アセット(UniVRMと併用して使用する)【VRM0/VRM1両対応】
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine向けモーション受信プラグイン(VRM4Uと併用して使用する)
- **[VRM4U](https://ruyo.github.io/VRM4U/)** - Unreal Engine 4で動作するVRMファイルのインポーター(単体で対応、必要に合わせてVMC4UEと使い分け) [接続手順](https://ruyo.github.io/VRM4U/08_vmc/)【VRM0/VRM1両対応】
- **[VMC4B Blender addon for VMCProtocol](https://tonimono.booth.pm/items/3432915)** - Blender向けモーション受信アドオン
- [hVMCP](https://github.com/Cj-bc/hVMCP) - Haskell版VMCProtocol実装(GPL)

### 映像配信・収録ツール / Live stream apps
- [Virtual Streamer 360](https://booth.pm/ja/items/1702492) - VR360°配信用のソフトウェア
- [だれでもVsinger](https://honokakaori.booth.pm/items/1768267) - 生歌配信、もしくはMVを一発撮りするために作られたものです
- [Collaboll(コラボル)](https://brother-pv.booth.pm/items/2016717) - VRMモデルとネットワーク通信で、複数人数でバーチャルキャラクター、所謂Vtuber動画を収録する事が出来るアプリです。
- [Oredayo4V](https://github.com/gpsnmeajp/Oredayo4V) - 主にWaidayo向けな高性能VMCProtocolビューア(Windows専用)
- [Oredayo4M](https://github.com/gpsnmeajp/Oredayo4M) - 主にWaidayo向けのクロスプラットフォームな高性能VMCProtocolビューア(Win, Mac, Linux対応)
- [VSeeFace](https://www.vseeface.icu/) - VSeeFace is a free, highly configurable face and hand tracking VRM avatar puppeteering program.
- [VtubeReflect](https://oose.itch.io/vtubereflect) - VtubeReflect captures your desktop and projects light onto your vtuber avatar.
- [VMCSaberTraining](https://fubukisakura.booth.pm/items/2374515) - きれいなセーバーを振り回せるツール
- [Horror Light](https://halfsode.booth.pm/items/3558017) - 暗い部屋でホラーゲームをやっているようなライティングに特化したアバター映像を出力するツール
- [VUP](https://store.steampowered.com/news/app/1207050/view/3091152381741795245) - VTuber & Animation & motion capture & 3D & Live2D
- [VRoom](https://ojousamaya.booth.pm/items/3949561) - Vtuber向け3D配信部屋アプリ
- [VTuber Plus](https://vtuberplus.com/) - VTuber Plus is a highly customizable tool that allows Twitch viewers to interact with streamers!
- [VNyan](https://suvidriel.itch.io/vnyan) - VNyan is a 3D VTuber Front end application for bringing your VTubing to the next level. ノードベースでプログラミング可能なVtuber向け統合演出環境
- [DAN SING](https://store.steampowered.com/app/1688750/AMV_Maker_for_Vroid_VRM_and_MMD_Mac_Supported/) - Music video tool using avatars, アバターを使ったミュージックビデオ撮影ツール
- [Animaze by FaceRig](https://www.animaze.us/manual/vmc-guide) -  Livestream, video chat, and record videos as incredible 2D and 3D avatars. (VMC Protocol only supports 3D avatars). 元祖バーチャルキャラクター撮影ツール(VMCProtocolは3Dのみ対応)
- [VMCProtocol Mixer / VMCProtocolミキサー](https://daifuklana.booth.pm/items/4857936) - VMCProtocolで送信された表情やボーンその他の情報を任意に合成し、表示する。有料版では4K画像撮影機能や送信機能あり。【VRM0/VRM1両対応】
- [VMagicMirror](https://baku-dreameater.booth.pm/items/1272298) - VRMをキーボードとマウスだけで動かせるWindows向けソフト
- [ケヤキスタジオ](https://keyaki-kaihatsu.booth.pm/items/5381402) - アバター映像制作アプリ。モーションキャプチャから編集、シーンの作り込み、AR撮影など。Windows, iOS, Android対応
- [Talking Head Anime 3 with Switchable Images](https://gitlab.com/pale_color/talking-head-anime-3-with-switchable-images) - 1枚絵を生成AIを使って動かせるようにするツール
- [AvaKit](https://note.com/avakit/n/n3eefc991f86a?magazine_key=m4853412f8bad) - カメラベースのモーションキャプチャー・配信ソフトだが、VMCProtocolも入力として利用可能
- [Warudo](https://store.steampowered.com/app/2079120/Warudo/?l=japanese) - 3D VTubingソフトウェア。フルボディトラッキングソースとしてVMCProtocolを利用可能
- [OBSKUR](https://www.obskur.com/) - ストリーマー向け統合配信環境ソフトウェア。3D VTuber機能のトラッキングソースとしてVMCProtocolを利用可能
- [VCamStream](https://booth.pm/ja/items/5063567) - 手元カメラ配信など3Dアバターと実写映像を構成する配信向けソフト【VRM0/VRM1両対応】
- [VRMFrontView](https://booth.pm/ja/items/5960908) - バーチャルモーションキャプチャーなどで収録している映像に合わせて、別視点の映像を収録するための補助表示ツール。単独撮影も可能。

### 映像支援ツール / Live stream helper apps
- [VMC-Websocket-OBS](https://github.com/gpsnmeajp/VMC-Websocket-OBS) - VMCProtocolを用いてバーチャルモーションキャプチャーの状態を取得し、Websocketを用いてOBSを制御するソフトウェア
- [ゆかりねっとコネクター](https://www.machanbazaar.com/%e3%82%86%e3%81%8b%e3%82%8a%e3%81%ad%e3%81%a3%e3%81%a8%e3%82%b3%e3%83%8d%e3%82%af%e3%82%bf%e3%83%bc/) - 話した言葉を字幕にして、多言語翻訳まで出来る配信支援アプリです。(キャリブレーション開始コマンドの発行に対応。YNC独自拡張仕様により字幕表示もサポート)
- [VMCbroadcaster_v0.1](https://izm.fanbox.cc/posts/1301580?utm_campaign=manage_post_page&utm_medium=share&utm_source=twitter) - バモキャの映像を見ながらカメラマンが頑張るソフト
- [vmc2bvh](https://github.com/infosia/vmc2bvh) - バーチャルモーションキャプチャーからBVH (Biovision Hierarchy)モーションファイルを生成するツール
- [AmariMocapTools](https://amarinoa.booth.pm/items/5118343) - VMCProtocolモーションデータからBVHモーションファイルを収録するツール


### ゲーム / Games
- [パイロットクロス(PilotXross)](https://n-mattun.booth.pm/items/1997616) - VR機器向けに開発したVRフライトゲームです
- [VMCAvatar-BS](https://github.com/nagatsuki/VMCAvatar-BS) - Beat Saber内にアバターを表示するMod

# 開発者向けツール / For developver apps
- [VMCProtocolMonitor](https://github.com/gpsnmeajp/VMCProtocolMonitor) - MarionetteとしてVMCProtocolの受信内容を表示するソフトウェア。 ごく単純に受信内容をコンソールに流すモードと、VMCProtocolに基づいてブラウザに一覧表示するモードがあります。
- [VMCProtocolModelViewer](https://github.com/gpsnmeajp/VMCProtocolModelViewer) - VMCProtocolの受信内容をVMC互換で表示するソフトウェア。バーチャルモーションキャプチャーとほぼ同等の表示を実現しようとします。
- [MotionReplay](https://github.com/emilianavt/MotionReplay) - This is a very simple debugging tool for VMC protocol applications 
- [BlendShapeClip Viewer v1.00](https://halfsode.booth.pm/items/4017038) - パーフェクトシンクの現象再現・修正支援ツール

# その他 / Other
- [VMCPViewerVR](https://github.com/gpsnmeajp/VMCPViewerVR) - VMCProtocol経由で送られてきた姿勢を反映し、VRゴーグルで鑑賞するためのソフト
- [Roid1 URDF を VMCプロトコル で動かすもの](https://github.com/kirurobo/Roid1_VMCProtocol) - サーボロボットをモーションデータで制御
- [DesktopFullTrackingVMCP](https://kirisamenanoha.booth.pm/items/5030353) - デスクトップフルトラアプリ
- [SlimeVR](https://github.com/SlimeVR/SlimeVR-Server) - オープンソースのボディトラッカー
- [TMARelay](https://booth.pm/ja/items/5056237) - VMCProtocolの姿勢情報をVRChat OSCに変換して送信するソフトウェア
- [VBTTools](https://github.com/tbbsakura/VBTTools) - VRMモデルの姿勢をVirtual Motion Tracker(VMT)やOpenVR-OpenTrackに送信して、SteamVRに仮想HMD/コントローラとして認識させるツール


# 依存ライブラリ / Dependency
- [UniVRM 0.53(Unity環境 必須)](https://github.com/vrm-c/UniVRM)
- [uOSC(Unity環境 推奨)](https://github.com/hecomi/uOSC)

# 解説記事・関連記事 / Article
- [トラッキングできたら アバターを動かしたい！](https://speakerdeck.com/sh_akira/toratukingudekitara-abatawodong-kasitai)
- [3周年のばもきゃと歩んだ人生を振り返る～2020年～](https://akira.fanbox.cc/posts/2477224)
- [Xbox360版KinectによるVMCProtocolを使用した姿勢情報の送信、VRMモデル操作について @daifuk-lana](https://qiita.com/daifuk-lana/items/c098fe9977c5e1202acb)
- [PythonからVMC Protocolでばもきゃに情報を送信してみよう](https://takeda-san.hatenablog.com/entry/2021/12/12/005347)
- [How to connect Unity and virtual motion capture with EVMC4U](https://styly.cc/tips/evmc4u_rapturn_virtualmotioncapture/)
- [VMCProtcolを使って連鎖的にいろんなソフトを繋いで Blenderに表情からつま先まで全身のモーションを流し込む方法 @KEI_unr ](https://qiita.com/KEI_unr/items/63badaba173ef59659ce)
- [EVMC4UでUnityとバーチャルモーションキャプチャーをつなぐ方法 styly.cc](https://styly.cc/ja/tips/evmc4u_rapturn_virtualmotioncapture/)
- [TDPT + VMCプロトコル on WebRTC](https://www.slideshare.net/hironroinakae/tdpt-vmc-on-webrtc)
- [VRoidってすげえ！って話(その2)　バーチャルモーションキャプチャー(VMCプロトコル)｜たつ @Tatsu_cp](https://note.com/tatsu_cpt/n/n233bb340bf3f)
- [【VRChat】VMC&EVMC4Uを使ってモーションエモートを作ってみた！！](https://keiki002.com/vr/vmc-emote/)
- [VroidStudio Unity - Sending to Unity using OSC/VMC Receiver](https://www.youtube.com/watch?v=Pl-XMcIm1qI)
- [How to create your own VTuber app in Unity Engine with OSC/VMC](https://www.youtube.com/watch?v=UiRfnriKmBY)
- [ツールを駆使してVTuberになろう - ギャップロ](https://gaprot.jp/2021/04/06/vtuber-toos/)
- [【Unity】VRecのBlendShapeをパーフェクトシンクに対応させる](https://wakushika-blog.com/unity-vrec-perfectsink/)
- [Unity上のモーションでUE4のモデルを動かしてみた](https://dev.classmethod.jp/articles/vmc_to_mop_ue4_mocap_test/)
- [WebカメラだけでmediapipeとVSeeFace(without Leap Motion)でVRMモデルを動かす実験メモ](https://angelpinpoint.seesaa.net/article/483827280.html)
- [パーフェクトシンクであそぼう！](https://hinzka.hatenablog.com/entry/2020/08/15/145040)
- [バーチャルモーションキャプチャーへのプルリクまとめと、内部構造メモ](https://note.com/gpsnmeajp/n/n7d741691a126)
- [Tauriでばもきゃと連携する話： Webでもトラッカーの情報を扱いたい!](https://qiita.com/kouta_vr/items/46f897481bd00ffe1665)
- [VRM10のLookAtの仕様によってVMCProtocolの送信時に困った話](https://qiita.com/daifuk-lana/items/a9cc4af7b4143ca994af)
- [waidayoを使って手軽にUnityやUE4でフェイシャルキャプチャ(表情取得)しよう](https://qiita.com/gpsnmeajp/items/5bb00157a39436039fed)
- [その他Qiitaの記事](https://qiita.com/tags/vmcprotocol)
- [その他Noteの記事](https://note.com/search?q=VMCProtocol)

# メディア記事 / Media
- [ソニー、小型モーションキャプチャ「mocopi」の使い方。連携で“ガチ運用”も - AV Watch](https://av.watch.impress.co.jp/docs/news/1460099.html)
- [iPhoneを使ったモーションキャプチャーツール「waidayo」配信 - MoguLive](https://www.moguravr.com/waidayo/)
- [今すぐVTuberになれる！ お手軽ツール37選を徹底紹介 - MoguLive](https://www.moguravr.com/vtuber-tools/)

# 学術研究 / Academic research
- [安価なVR機器を用いた民族舞踊保存手法の検討 - 電子情報通信学会](https://www.ieice.org/)

本項目は見つけ次第掲載しているものです。本ページへの掲載・削除に関しては[Issue](https://github.com/sh-akira/VirtualMotionCaptureProtocol/issues)よりお知らせください。  
