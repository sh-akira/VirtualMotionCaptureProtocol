---
layout: page
title:  "VMC Protocol Marionetteプロトコル仕様"
subtitle: "プロトコル仕様の詳細をご説明します"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true
---

# Marionette(モーション受信側アプリケーション)受信仕様
Performer → Marionetteの流れで送信されるデータの仕様です。

## 内容
```
基本仕様
/Address (Type){Value}  

拡張仕様(Version)
/Address (Type){Value} (Type){Value}  
```
の形で記述します。

### 利用可否(Available)
```
/VMC/Ext/OK (int){loaded}  
  
V2.5  
/VMC/Ext/OK (int){loaded} (int){calibration state} (int){calibration mode}  
  
V2.7  
/VMC/Ext/OK (int){loaded} (int){calibration state} (int){calibration mode} (int){tracking stataus}  
```
loaded: モデル読み込み前は0、読み込み後は1  
　後述のボーン情報等が送信されているか否かを示す  

calibration state

+ Uncalibrated = 0
+ WaitingForCalibrating = 1
+ Calibrating = 2
+ Calibrated = 3  

calibration mode
+ 通常モードは0
+ MR通常モードは1
+ MR床補正モードは2  

tracking stataus
+ 正常=1
+ 不可=0

**V2.7 リファレンス未実装**

### 送信側相対時刻(Time)
```
/VMC/Ext/T (float){time}  
```
送信側の現在の相対時刻
通信できているかを確認するのに主に使用する

### Root姿勢(Root Transform)
```
v2.0
/VMC/Ext/Root/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  

v2.1
/VMC/Ext/Root/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){s.x} (float){s.y} (float){s.z} (float){o.x} (float){o.y} (float){o.z}  
```
p=位置
q=回転(Quaternion)
s=MR合成用スケール
o=MR合成用オフセット

モデルのrootとなるオブジェクトの絶対姿勢  
nameは"root"固定。(Boneと合わせるため)  
前半がPosition、後半がQuaternion  
受信側ではLoal姿勢として扱うことを推奨する  
  
v2.1より、MR合成用のスケールが追加された。  
これを使うことでアバターの位置やサイズを現実の身体のサイズに合わせることができる。  
  
### Bone姿勢(Bone Transform)
```
/VMC/Ext/Bone/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
```
モデルのrootとなるオブジェクトのLocal姿勢  
nameはUnityEngineのHumanBodyBonesに沿った型名  
前半がPosition、後半がQuaternion  
  
※HumanBodyBonesすべてが送信される。LastBoneも含む。  
これにより指の動きやEyeボーンなども送信される。  

### VRM BlendShapeProxyValue
```
/VMC/Ext/Blend/Val (string){name} (float){value}  
/VMC/Ext/Blend/Apply
```
BlendShapeProxyの値。送信側のVRMモデルに含まれるものすべてが送信され、  
一連の内容が送信された後、Applyが送信される。  
UniVRMの仕様上、AccumulateValueで蓄えた後、Applyを行うためこのようになっている。  
これにより、表情やリップシンクなども送信される。  

### Camera位置・FOV(Camrea Transform&FOV)
```
V2.1
/VMC/Ext/Cam (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){fov} 
```
VMCの選択中のカメラの絶対位置と回転、FOV  
前半がPosition、後半がQuaternion  
VMC側でFOVに合わせて位置が調整されている。  
  
### Controller Input
```
V2.1
/VMC/Ext/Con (int){active} (string){name} (int){IsLeft} (int){IsTouch} (int){IsAxis} (float){Axis.x} (float){Axis.y} (float){Axis.z} 
```
コントローラのボタン入力に関する情報。キー名に関する詳細定義はないため、部分一致や、キーコンフィグなどで対応してください。  
active=1で押下、0で開放、2でAxis変化  

### 【イベント送信】keyboard Input
```
V2.1
/VMC/Ext/Key (int){active} (string){name} (int){keycode}
```
キーボード入力に関する情報。キー名に関する詳細定義はないため、部分一致や、キーコンフィグなどで対応してください。  
active=1で押下、0で開放  

### 【イベント送信】MIDI Note Input
```
V2.2
/VMC/Ext/Midi/Note (int){active} (int){channel} (int){note} (float){velocity}
```
MIDIノート入力に関する情報  
active=1でオン、0でオフ  

### 【イベント送信】MIDI CC Value Input
```
V2.2
/VMC/Ext/Midi/CC/Val (int){knob} (float){value}
```
MIDI CCアナログ値入力に関する情報  
数値変化が連続的に送信される。

### 【イベント送信】MIDI CC Button Input
```
V2.2
/VMC/Ext/Midi/CC/Bit (int){knob} (int){active}
```
MIDI CCデジタル値入力に関する情報  
デジタル変化時のみ送信される。(アナログ値は0.5を閾値とする)  
active=1でオン、0でオフ  

### Device Transform
```
V2.2
/VMC/Ext/Hmd/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Con/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Tra/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  

V2.3
/VMC/Ext/Hmd/Pos/Local (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Con/Pos/Local (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Tra/Pos/Local (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  

```
HMD, コントローラ, トラッカーの姿勢情報(通常=アバターのスケールに合わせて調整済みの座標, Local=生座標)  
serialはOpenVRのシリアル番号  
前半がPosition、後半がQuaternion  

### 【低頻度】受信有効情報(Receive enable)
```
v2.4
/VMC/Ext/Rcv (int){enable:0 or 1} (int){port:0～65535}  
  
v2.7
/VMC/Ext/Rcv (int){enable:0 or 1} (int){port:0～65535} (string){IP Address}  
```
VMC側の受信機能が有効なら1,無効なら0
portは有効ならポート番号、無効なら不定値が入る
低頻度で送信される。  
**V2.7 リファレンス未実装**

### 【低頻度】DirectionalLight位置・色(DirectionalLight transform & color)
```
V2.4
/VMC/Ext/Light (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){color.red} (float){color.green} (float){color.blue} (float){color.alpha} 
```
VMCのDirectionalLightの位置・色  
前半がPosition、後半がQuaternionと色  
低頻度で送信される。

### 【低頻度】ローカルVRM基本情報(Local VRM information)
```
V2.4
/VMC/Ext/VRM (string){path} (string){title}  
  
V2.7
/VMC/Ext/VRM (string){path} (string){title} (string){Hash} 
```
VMCが読み込んでいるVRMファイルの基本情報  
ファイルパスとタイトル、現在読み込まれているVRMデータのハッシュ値(SHA-256)    
低頻度で定期送信される。  
  
pathは、ローカルファイルパス(例: C:\default.vrm )  
  
パスをどう扱うかは受信側依存だが、処理できない場合は無視すること。  
ネットワーク経由でパスを受け取った場合、ローカルにそのファイルが存在しない可能性があることを考慮すること。  
異なるOSから送信されうることも想定すること。  
(初回はファイル読み込み失敗エラーを出しても良いが、定期送信されるため変化のない2回目以降は無視するべきである)  
  
通常、フルパスが送信され、フルパスで一致するものを読み込む前提であるが、  
スマートフォンなどファイルアクセスに制限がある環境では、ファイル名のみチェックして一致するものを読み込むなどしても良い。  
  
オンラインサービスなどの読み込み情報は、/VMC/Ext/Remote を使用すること。  
本メッセージが送信されている場合ローカルファイルが読み込まれているため、/VMC/Ext/Remote は定期送信されない。
  
自動ロードを実装する場合は、変化を受信側でチェックすること。  
送信側は、あるアバターを読み込んでから、次のアバターを読み込むまで無意味に送信データを変化させてはならない。  
  
注意: 送信側でVRMライセンス情報その他のユーザーへの提示が終わったファイルのパスを送信することが前提になっています。  


**V2.7 リファレンス未実装**

### 【低頻度】リモートVRM基本情報(Remote VRM information)
```
V3.0
/VMC/Ext/Remote (string){service} (string){json} 
```
VMCが読み込んでいるオンラインサービス上あるいは、リモートデバイス上のアバターの読み込みに関する情報。  
低頻度で定期送信される。  
  
serviceは小文字にすること。  
受信側がどのサービスに対応するかは規定しない。  
  
jsonは、json形式の文字列である。型や内容はserviceによって定まる。  
対応しないserviceを受け取った場合、jsonは処理せず無視すること。  
  
ローカルファイルの読み込み情報は、/VMC/Ext/VRM を使用すること。  
本メッセージが送信されている場合リモートアバターが読み込まれているため、/VMC/Ext/VRM は定期送信されない。  
  
自動ロードを実装する場合は、変化を受信側でチェックすること。  
送信側は、あるアバターを読み込んでから、次のアバターを読み込むまで無意味に送信データを変化させてはならない。  
  
注意: 送信側でVRMライセンス情報その他のユーザーへの提示が終わったアバターの情報を送信することが前提になっています。  
  
**V3.0 リファレンス未実装**  

```
サービス例
/VMC/Ext/Remote vroidhub {"characterModelId":"123456789456"}
```


### 【低頻度】Option文字列(Option string)
```
V2.4
/VMC/Ext/Opt (string){option} 
```
ユーザースクリプトに渡したいオプション情報  
ユーザーが操作したとき、および送信要求を受け取ったときに送信する  
低頻度で送信される。

設定画面を用意できない、作りたくない、遠隔にある機器などを制御するための固定されたJSON/コマンド文字列を想定。  
オプション、挙動の変更、設定ファイルの読み込み指示など。
バーチャルモーションキャプチャーの場合は設定画面から入力可。  

### 【低頻度】背景色(background color)
```
V2.4
/VMC/Ext/Setting/Color (float){r} (float){g} (float){b} (float){a} 
```
背景色情報  
低頻度で送信される。  


### 【低頻度】ウィンドウ属性情報(Window attribute)
```
V2.4
/VMC/Ext/Setting/Win (int){IsTopMost} (int){IsTransparent} (int){WindowClickThrough} (int){HideBorder} 
```
ウィンドウ属性情報。1=true,0=false  
低頻度で送信される。  

### 【低頻度】設定ファイルパス(Loaded setting path)
```
V2.5
/VMC/Ext/Config (string){path} 
```
現在読み込まれている設定ファイルのパス    
低頻度で送信される。

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

