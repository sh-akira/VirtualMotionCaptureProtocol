---
layout: page
title:  "VMC Protocol Performerプロトコル仕様"
subtitle: "プロトコル仕様の詳細をご説明します"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true
---
# Performer(モーション送信側アプリケーション)受信仕様
Marionette → Performer あるいは、Assistant → Performer の流れで送信されるデータの仕様です。

### Virtual Device Transform
```
V2.3
/VMC/Ext/Hmd/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Con/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Tra/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
```
仮想HMD, コントローラ, トラッカーの姿勢情報  
serialは仮想シリアル番号  
前半がPosition、後半がQuaternion  
HMDはトラッカーとして扱われる。  

### Frame Period
```
V2.3
/VMC/Ext/Set/Period (int){Status} (int){Root} (int){Bone} (int){BlendShape} (int){Camera} (int){Devices} 
```
バーチャルモーションキャプチャーからのデータ送信間隔を設定する。  
1/x Frame間隔で送信される。  

### Virtual MIDI CC Value Input
```
V2.3
/VMC/Ext/Midi/CC/Val (int){knob} (float){value}
```
仮想MIDI CCアナログ値入力に関する情報  

### Virtual Camrea Transform&FOV
```
V2.3
/VMC/Ext/Cam (string){Camera} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){fov} 
```
カメラ位置制御  
受信時、強制的にフリーカメラになる  

### VRM BlendShapeProxyValue
```
V2.3
/VMC/Ext/Blend/Val (string){name} (float){value}  
/VMC/Ext/Blend/Apply
```
BlendShapeProxyの値

**VRM0系とVRM1系の非互換性に関する警告**

既存のVMCProtcol準拠のアプリケーションは、VRM0を使用しています。  
VRM0と、VRM1では、プリセットのExpression(VRM0.xではBlendShape)が違うものになります。  

VRM1系のVRM SDKを使用する場合、自動Migration時に、VRM0モデルもVRM1形式に変換されます。

変更点は[VRM公式サイトを参照してください。](https://vrm.dev/vrm1/changed.html)


既存のVRM0形式を用いるVMCProtocolアプリケーションとの互換性を保つため、

+ VRM1系を用いる送信者は、VRM0形式での送信を必ず実装してください。  
  ただしVRM1形式での送信もオプションとして用意することを推奨します。
+ VRM1系を用いる受信者は、VRM0形式で受信した場合、VRM1形式に変換して処理してください。  

|VRM0|VRM1|
|---|---|
|Joy|happy|
|Angry|angry|
|Sorrow|sad|
|Fun|relaxed|
|A|aa|
|I|ih|
|U|ou|
|E|ee|
|O|oh|
|Blink_L|blinkLeft|
|Blink_R|blinkRight|

### Eye Tracking Target Position
```
V2.3
V2.8 (Disruptive change)
/VMC/Ext/Set/Eye (int){enable} (float){p.x} (float){p.y} (float){p.z}
```
アイトラッキング目標座標(Headからの相対座標) (V2.8～)  
※アイトラッキング目標座標(空間内絶対座標) (V2.3～V2.7)  
  

### 【イベント送信】情報送信要求(Request Information)
```
V2.4
/VMC/Ext/Set/Req 
```
VMCに低頻度送信情報の即時送信を要求する。  
応答内容は未定

### 【イベント送信】Response文字列(Response string)
```
V2.4
/VMC/Ext/Set/Res (string){Response} 
```
ユーザースクリプトのレスポンス情報  
ユーザースクリプトのタイミングで送信する  

設定画面を用意できない、作りたくない、遠隔にある機器などの状態を確認するための固定されたJSON/コマンド文字列を想定。
バーチャルモーションキャプチャーの場合は設定画面から閲覧可。  
ファイル読み込みエラーなど。

### 【イベント送信】キャリブレーション(準備)要求(calibration/calibration ready request)
```
V2.5
/VMC/Ext/Set/Calib/Ready  
/VMC/Ext/Set/Calib/Exec (int){mode}   
```
Ready: キャリブレーション準備を要求  
Exec: 指定のモードでキャリブレーションを実行(0=通常,1=MR通常,2=MR床補正)  
  
ReadyとExecの間には十分な時間を開けてください。  

### 【イベント送信】設定ファイル読み込み要求(Request load setting file)
```
V2.5
/VMC/Ext/Set/Config (string){Path} 
```
VMCに指定した設定ファイルを読み込ませる  

### スルー情報(Thru info)
```
V2.6
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (float){arg2} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (int){arg2} 
```
xxxxxxxxxは任意の名前。  
Assistantの独自拡張情報の送信用。  
原則、Performerはこれに該当するメッセージは一切の処理をせず、Marionetteへと中継する。  
ただし、記載がある場合、Performerがなにかに利用したり、中継時に書き換えることもある。  

### DirectionalLight位置・色(DirectionalLight transform & color)
```
V2.9
/VMC/Ext/Light (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){color.red} (float){color.green} (float){color.blue} (float){color.alpha} 
```
VMCのDirectionalLightの位置・色  
前半がPosition、後半がQuaternionと色  
V2.9 リファレンス未実装