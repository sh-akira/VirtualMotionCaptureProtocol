---
layout: page
title: "VMC Protocol サンプル"
subtitle: "プロトコル実装のサンプルです"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true
---

プロトコル実装のサンプルは以下にあります。  
Unity 2021.3.8.f1で動作を確認しています。

A sample protocol implementation can be found below.  
Confirmed to work with Unity 2021.3.8.f1.

**[Sample (github)](https://github.com/sh-akira/VirtualMotionCaptureProtocol/tree/master/sample)**

# ライセンス / Licence
このサンプルスクリプトのライセンスはソースコード先頭に記載されています。  
The license for this sample script is listed at the top of the source code.

# 依存ライブラリ / Dependent library
- VRM0:[UniVRM 0.99](https://github.com/vrm-c/UniVRM)
- VRM1:[VRM1](https://github.com/vrm-c/UniVRM)
- [uOSC](https://github.com/hecomi/uOSC)

# SampleBonesSendBundle.cs
Performer - モーション送信側サンプル(高速)です。  
VRMモデルのroot位置、ボーン、BlendShapeProxyを送信します。

uOSC Clientとともに使用します。
パケットをまとめて送る(bundle化する)ため、比較的高速です。

ModelにVRMモデルのGameObjectを設定してください。
filepathにVRMモデルのファイルパスを設定すると、自動読み込み対応アプリケーションは読み込みを行います。

VRMのランタイムロード機能も搭載しています。

Performer - Motion sender samples (fast).  
Send the root position, bones and BlendShapeProxy of the VRM model.

Used with uOSC Client.  
It is relatively fast because packets are sent together (bundled).

Set the VRM model GameObject to Model.  
If you set the file path of the VRM model in filepath, the auto-loading application will load it.

VRM runtime loading implemented.

![runtimeload](runtimeload.png)

# SampleBonesSend.cs
Performer - モーション送信側サンプル(低速)です。  
VRMモデルのroot位置、ボーン、BlendShapeProxyを送信します。

uOSC Clientとともに使用します。
細かくパケットを送信するため、低速になることがあります。

ModelにVRMモデルのGameObjectを設定してください。
filepathにVRMモデルのファイルパスを設定すると、自動読み込み対応アプリケーションは読み込みを行います。

**このスクリプトの利用は推奨しません！！**  
このスクリプトは、学習用であり、必要な処理を欠いているため、通信バッファの詰まりを引き起こして、動作に異常な遅れを生じる場合があります。  
SampleBonesSendBundle.csの方を利用することを強く推奨します。  

Performer - Motion sender samples (slow).  
Send the root position, bones and BlendShapeProxy of the VRM model.

Used with uOSC Client.  
Since it sends packets in detail, it may become slow.

Set the VRM model GameObject to Model.  
If you set the file path of the VRM model in filepath, the auto-loading application will load it.

**NOT RECOMMENDED USE THIS SCRIPT!!**  
This script is for learning purposes only and lacks necessary processing, which can cause communication buffers to clog up and cause unusual delays in operation.  
It is strongly recommended to use SampleBonesSendBundle.cs.  

# SampleBonesReceive.cs
Marionette - モーション受信側サンプルです。  
VRMモデルへのroot位置適用、ボーン適用、BlendShapeProxy適用を行います。

**これは最低限の実装です。特別な理由がない限り、EVMC4Uを利用することを推奨します。**

uOSC Serverとともに使用します。

ModelにVRMモデルのGameObjectを設定してください。

Marionette - Motion receiver example.  
Apply root position, bone, and BlendShapeProxy to VRM model.

**This is a minimal implementation. It is recommended to use EVMC4U unless there is a special reason.**

Used with uOSC Server.
Set the VRM model GameObject to Model.

# SampleTrackerSend.cs
Assistant - 主にモーションの処理はせず、補助的な情報をPerformerに送信するサンプルです。  
仮想トラッカー、BlendShapeProxyの送信を行います。

uOSC Clientとともに使用します。

送信したいデバイス種別、GameObjectのTransform、任意のシリアル番号を入力すると仮想デバイスとして送信します。

BlendShapeProxy名、値(0.0～1.0)を入力すると、BlendShapeProxy値として送信します。

Assistant - An example that does not primarily process motion, but rather sends auxiliary information to the Performer.  
Send virtual tracker, BlendShapeProxy.

Used with uOSC Client.

Enter the device type you want to send, GameObject Transform, and any serial number. Send as a virtual device.

Enter the BlendShapeProxy name and value (0.0 to 1.0). Send as a BlendShapeProxy value.

# CameraPositionSend.cs
Assistant - 主にモーションの処理はせず、補助的な情報をPerformerに送信するサンプルです。  
アタッチしたオブジェクトの位置をカメラ位置として送信します。

uOSC Clientとともに使用します。

Assistant - An example that does not primarily process motion, but rather sends auxiliary information to the Performer.  
Sends the position of the attached object as the camera position.

Used with uOSC Client.
