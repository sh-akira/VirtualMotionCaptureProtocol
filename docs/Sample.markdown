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
Unity 2018.4.19f1で動作を確認しています。

**[サンプル(github)](https://github.com/sh-akira/VirtualMotionCaptureProtocol/tree/master/sample)**

# ライセンス
このサンプルスクリプトのライセンスはソースコード先頭に記載されています。

# 依存ライブラリ
- [UniVRM 0.53(Unity環境 必須)](https://github.com/vrm-c/UniVRM)
- [uOSC(Unity環境 推奨)](https://github.com/hecomi/uOSC)

# SampleBonesSend.cs
Performer - モーション送信側サンプルです。  
VRMモデルのroot位置、ボーン、BlendShapeProxyを送信します。

uOSC Clientとともに使用します。

ModelにVRMモデルのGameObjectを設定してください。

# SampleBonesReceive.cs
Marionette - モーション受信側サンプルです。  
VRMモデルへのroot位置適用、ボーン適用、BlendShapeProxy適用を行います。

uOSC Serverとともに使用します。

ModelにVRMモデルのGameObjectを設定してください。

# SampleTrackerSend.cs
Assistant - 主にモーションの処理はせず、補助的な情報をPerformerに送信するサンプルです。  
仮想トラッカー、BlendShapeProxyの送信を行います。

uOSC Clientとともに使用します。

送信したいデバイス種別、GameObjectのTransform、任意のシリアル番号を入力すると仮想デバイスとして送信します。

BlendShapeProxy名、値(0.0～1.0)を入力すると、BlendShapeProxy値として送信します。

# CameraPositionSend.cs
Assistant - 主にモーションの処理はせず、補助的な情報をPerformerに送信するサンプルです。  
アタッチしたオブジェクトの位置をカメラ位置として送信します。

uOSC Clientとともに使用します。

