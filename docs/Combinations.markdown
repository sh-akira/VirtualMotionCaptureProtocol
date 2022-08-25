---
layout: page
title:  "VMC Protocol 組み合わせ"
subtitle: "アプリケーションの代表的な組み合わせをご紹介します"
description: "VMCProtocol - ゲーム、ツール、配信環境など、あらゆる場所で使いやすいモーションキャプチャプロトコル仕様"
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true

---

# 組み合わせ (Combinations)
よくコミュニティで使用される代表的な組み合わせをご紹介します。  

English version is prepering.  
[Please use Google Translate.](https://protocol-vmc-info.translate.goog/?_x_tr_sl=ja&_x_tr_tl=en&_x_tr_hl=ja&_x_tr_pto=wapp)

## 注意
これらのソフトウェアの動作を保証するものではありません。  
また、掲載されているものは一例です。

## VR機器(VMC) → Uinty

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## 表情(iPhone) → VR機器(VMC) → Uinty

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39540|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで受信した表情とミックスして、VMCProtocolで送信します|39539|39540|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## 表情(iPhone) → Uinty

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39539|-|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## 全身の動き(iPhone) → Uinty

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|TDPT|iPhoneで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## 全身の動き(Webcam) → Uinty

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|ThreeDPoseTracker|Webカメラで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## 表情(iPhone) → VMC

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39540|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで受信した表情とミックスして表示します|-|39540|

## 全身の動き(Webcam) → VMC

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|ThreeDPoseTracker|Webカメラで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39540|-|
|バーチャルモーションキャプチャー|VMCProtocolを受信して、仮想トラッカー情報からVRMモデルの動きに変換します|-|39540|

## 全身の動き(iPhone) → VMC

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|TDPT|iPhoneで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39540|-|
|バーチャルモーションキャプチャー|VMCProtocolを受信して、仮想トラッカー情報からVRMモデルの動きに変換します|-|39540|

## 表情(Webcam)/手(LeapMotion) → Unity

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|VSeeFace|Webカメラで撮影した表情や、LeapMotionでの手の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VMC4B|VMCProtocolを受信して、Blender上のモデルの動きに変換します|-|39539|

## 表情(iPhone) → VSeeFace → Unity

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39540|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きと、LeapmotionなどをミックスしてVMCProtocolで送信します|39539|39540|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39539|

## VR機器(VMC) → VSeeFace → Unity

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きと、LeapmotionなどをミックスしてVMCProtocolで送信します|39540|39539|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39540|

## 表情(iPhone) → VR機器(VMC) → VSeeFace → Unity

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39540|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで受信した表情とミックスして、VMCProtocolで送信します|39541|39540|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きと、LeapmotionなどをミックスしてVMCProtocolで送信します|39542|39541|
|EVMC4U|VMCProtocolを受信して、Unity上のVRMモデルの動きに変換します|-|39542|

## VR機器(VMC) → Unreal Engine

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VMC4UE|VMCProtocolを受信して、Unreal Engine上のVRMモデルの動きに変換します|-|39539|

## VR機器(VMC) → Blender

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VMC4B|VMCProtocolを受信して、Blender上のモデルの動きに変換します|-|39539|

## 全身の動き(iPhone) → Blender

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|TDPT|iPhoneで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VMC4B|VMCProtocolを受信して、Blender上のモデルの動きに変換します|-|39539|

## 全身の動き(Webcam) → Blender

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|ThreeDPoseTracker|Webカメラで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VMC4B|VMCProtocolを受信して、Blender上のモデルの動きに変換します|-|39539|

## 表情(iPhone) → VSeeFace

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|waidayo|Face IDで取得した表情を、VRMモデルの表情に変換して、VMCProtocolで送信します|39540|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きを表示します|-|39540|

## VR機器(VMC) → VSeeFace

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|バーチャルモーションキャプチャー|VR機器での体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きと、Leapmotionなどをミックスして表示します|-|39539|

## 全身の動き(iPhone) → VSeeFace

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|TDPT|iPhoneで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きを表示します|-|39539|

## 全身の動き(Webcam) → VSeeFace

|アプリ|説明|送信ポート|受信ポート|
|-|-|-|-|
|ThreeDPoseTracker|Webカメラで撮影した体の動きを、VRMモデルの動きに変換して、VMCProtocolで送信します|39539|-|
|VSeeFace|VMCProtocolで受信したVRMモデルの動きを表示します|-|39539|

## And more!
[組み合わせは莫大なため全てを紹介することはできません！](Reference)


