---
layout: post
title:  "プロトコル仕様"
---

# はじめに
対応アプリケーションは本プロトコルの全てに対応しているとは限りません。  
1項目でも利用可能であれば、対応とすることができます。  

基本的に安定した同一ローカルネットワーク内での利用が想定されています。  

# 受信プロトコルについて
以下のプロトコルで受信することができます。

+ OSC Protocolによる単方向UDP通信
+ ポート番号は既定で39539
+ 負荷対策のためbundle化しました。それに伴い、パケットロスは影響が大きくなります。
+ 送信周期は送り手のフレームレート依存
+ 必要・不要にかかわらず送信側は送信し、受信側でフィルタする

現在のプロトコルバージョンはV2.5です。

実装リファレンスであるEVMC4UのExternalReceiverは、不明なアドレス、多すぎる引数は単に無視します。  
少なすぎる引数、型の違う引数は例外を引き起こします。  

データのチェックには以下が便利です。  
[OSCDataMonitor(Github)](https://github.com/kasperkamperman/OSCDataMonitor)  
[OSCDataMonitor(Download)](https://www.kasperkamperman.com/blog/processing-code/osc-datamonitor/)  

## 内容
```
/Address (Type){Value}  
```
の形で記述します。

### 利用可否(Available)
```
/VMC/Ext/OK (int){loaded}  
  
V2.5  
/VMC/Ext/OK (int){loaded} (int){calibration state} (int){calibration mode}  
```
loaded: モデル読み込み前は0、読み込み後は1  
　後述のボーン情報等が送信されているか否かを示す  
calibration state: Uncalibrated = 0, WaitingForCalibrating = 1, Calibrating = 2, Calibrated = 3  
calibration mode: 通常モードは0、MR通常モードは1、MR床補正モードは2  

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
```
VMC側の受信機能が有効なら1,無効なら0
portは有効ならポート番号、無効なら不定値が入る
低頻度で送信される。

### 【低頻度】DirectionalLight位置・色(DirectionalLight transform & color)
```
V2.4
/VMC/Ext/Light (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){color.red} (float){color.green} (float){color.blue} (float){color.alpha} 
```
VMCのDirectionalLightの位置・色  
前半がPosition、後半がQuaternionと色  
低頻度で送信される。

### 【低頻度】VRM基本情報(VRM information)
```
V2.4
/VMC/Ext/VRM (string){path} (string){title} 
```
VMCが読み込んでいるVRMファイルの基本情報  
現在はファイルパスとタイトルのみ  
低頻度で送信される。  
注意: 送信側でVRMライセンス情報その他のユーザーへの提示が終わったファイルのパスを送信することが前提になっています。  

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





# 送信プロトコルについて
VirtualMotionCaptureは以下の以下のプロトコルで受信しています。

+ OSC Protocolによる単方向UDP通信
+ ポート番号は既定で39540

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

### Eye Tracking Target Position
```
V2.3
/VMC/Ext/Set/Eye (int){enable} (float){p.x} (float){p.y} (float){p.z}
```
アイトラッキング目標座標(ルーム内絶対座標)

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
