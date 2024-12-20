---
layout: page
title:  "VMC Protocol specification"
subtitle: "english version specification"
description: "VMCProtocol - Easy-to-use motion capture protocol specifications for games, tools, distribution environments, etc. "
image: vmpc_logo_128x128.png
hero_image: image.gif
hero_height: is-fullwidth
hero_darken: true
---

# What's
Virtual Motion Capture Protocol (VMCProcotol, OSC/VMC Protocol) is avatar motion communication protocol for [virtual motion capture](https://vmc.info/).

You can easily move your avatar by using an easy-to-use library without implementing the handling of VR devices. 

[You can send and receive motions to and from various applications. ](Reference).

It is a simple implementation using Open Sound Control and VRM, and can communicate with various environments such as Windows, Mac, Linux, and iOS on machine internal or local network. 

{% include youtubePlayer.html id="DunqgLrUfpI" %}

# Library/Asset
- **[EVMC4U](https://github.com/gpsnmeajp/EasyVirtualMotionCaptureForUnity)** - Unity
- **[VMC4UE](https://github.com/HAL9HARUKU/VMC4UE)** - Unreal Engine with [VRM4U](https://ruyo.github.io/VRM4U/)
- **[VRM4U](https://ruyo.github.io/VRM4U/)** - Unreal Engine
- **[VMC4B Blender addon for VMCProtocol](https://tonimono.booth.pm/items/3432915)** - Blender
- **[Godot XR VMC Tracker](https://github.com/Malcolmnixon/GodotXRVmcTracker)** - Godot Engine 4.3 with [Godot VRM Importer](https://godotengine.org/asset-library/asset/2031). [video](https://www.youtube.com/watch?v=eE0UGosv7ek)


or you can implement protocol your self.

# Logo
VMC Protocol-enabled applications can use the following logos. 

![logo](vmpc_logo_128x128.png)  
[larger logo](vmpc_logo_1024x1024.png)

# [Sample](Sample)
[Sample implementation](Sample)

# [Combinations(Combinations)
[Application Combinations](Combinations)

# Licence
MIT Licence

# Information included
Basic information

VR model created by VRM standard
- Bone information
- BlendShape information

Advanced information(optional)

- Camera control
- Light control
- Keyboard input
- MIDI Note / CC input
- Tracker position
- VRM dynamic loading
- Calibration control
- Configuration
- Gaze control

it is not necessary to correspond to all of this information, and you can use only the information you want to use. 

# Glossary
+ **Server** - OSC Server (Receiver)
+ **Client** - OSC Client (Sender)

These terms cause confusion about the actual role. 
So, The VMC protocol defines the following specific terms.

+ **Marionette** - Receive motion and draw screen. (Required)  
It works server for Performer, commonly port 39539.  
(Eg: EVMC4U, VMC4UE)
+ **Performer** - Process motion and IK. Send all bones and more info to Marionette. (Required)  
It works client to Marionette, commonly send to 39539.  
And It works server for Assistant(optional), commonly port 39540.  
(Eg: Virtual Motion Capture, Waidayo, VSeeFace, MocapForAll, TDPT)
+ **Assistant** - Send some bones, facial expressions, etc to Performer. (optional) 
It works client to Performer, commonly send to 39540.  
(Eg: Waidayo, Sknuckle, Simple Motion Tracker, Uni-studio)

![flow](flow_en.gif)
![layer](layer.png)

# Communication format 
+ Open Sound Control(OSC) over UDP/IP
+ Use appropriate osc type.
+ Use UTF-8. (Data includes non ascii type)
+ port numbers commonly use 39539, and 39540. but recommended it can change for user experience.
+ Packets may be bundled.
+ The transmission period is undefined. And some messages do not periodically.  
+ The recipient should discard unnecessary messages. You don't have to process every message. 
+ Which message to send or receive depends on both implementations. 
+ Unknown addresses and too many arguments should be ignored.
+ If you detect an argument that has too few arguments or an argument of a different type than defined as an extended specification, it must an ignored.

[See here for PerfectSync used for advanced facial expression tracking](https://hinzka.hatenablog.com/entry/2020/08/15/145040)

# OSC Debugging
Recommended tools  
[VMCProtocolMonitor](https://github.com/gpsnmeajp/VMCProtocolMonitor)

[OSCDataMonitor(Github)](https://github.com/kasperkamperman/OSCDataMonitor)  
[OSCDataMonitor(Download)](https://www.kasperkamperman.com/blog/processing-code/osc-datamonitor/)  


# Sample
```
Basic spec
/Address (Type){Value}  

Advanced spec with version
/Address (Type){Value} (Type){Value}  
```

# Marionette(Motion receiver application)
Performer → Marionette specifications.

### Available
```
/VMC/Ext/OK (int){loaded}  
  
V2.5  
/VMC/Ext/OK (int){loaded} (int){calibration state} (int){calibration mode}  
  
V2.7  
/VMC/Ext/OK (int){loaded} (int){calibration state} (int){calibration mode} (int){tracking status}  
```
loaded: 0=not loaded、1=loaded  

calibration state

+ Uncalibrated = 0
+ WaitingForCalibrating = 1
+ Calibrating = 2
+ Calibrated = 3  

calibration mode
+ Normal = 0
+ MR Normal = 1
+ MR Floor fix = 2  

tracking status
+ OK=1
+ Bad=0

**V2.7 spec is not implemented in Virtual motion capture**

### Relative time
```
/VMC/Ext/T (float){time}  
```

Current relative time of the sender.  
Mainly used to check if communication is possible.

### Root Transform
```
v2.0
/VMC/Ext/Root/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  

v2.1
/VMC/Ext/Root/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){s.x} (float){s.y} (float){s.z} (float){o.x} (float){o.y} (float){o.z}  
```
p=Position  
q=Quaternion  
s=MR Scale  
o=MR Offset  

Model root absolute position.  
name is "root"。  

It is recommended to treat it as a Loal posture on the receiving side.   
  
From v2.1, a scale for MR synthesis has been added.
By using this, the position and size of the avatar can be adjusted to the actual body size. 
  
### Bone Transform
```
/VMC/Ext/Bone/Pos (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
```
  
name is HumanBodyBones type name(UnityEngine).  
p=Position  
q=Quaternion  
  
All HumanBodyBones will be send. (Include eye bone and finger bones)


**Notice regarding VRM1**  
In VRM1, there are normalized bone poses controlled by ControlRig and original bone poses.  
[Depending on the state of ControlRig, the pose information obtained using the same animator.GetBoneTransform may differ.](https://vrm.dev/api/vrm1_controlrig/#%E8%A9%B3%E7%B4%B0)  

The recommended approach as VMCProtocol is to use **non-normalized (original)** bone information without ControlRig applied.  
(This is the pose obtained with instance.Humanoid.GetBoneTransform)  

While it is not prohibited to send normalized bones (instance.Runtime.ControlRig.GetBoneTransform), it is not recommended.  
If you choose to do so, handle it as an option setting that is disabled by default.

### VRM BlendShapeProxyValue
```
/VMC/Ext/Blend/Val (string){name} (float){value}  
/VMC/Ext/Blend/Apply
```

BlendShapeProxy value in VRM model.
After all have been sent, **apply** will be sent.

Facial expressions and lip sync are sent this.

Note that due to changes in the UniVRM specification, it is Case Sensitive.  
In permissive applications (including those using EVMC4U), case sensitivity is ignored, mainly to maintain compatibility, but otherwise it does not work properly.  

[See here for PerfectSync used for advanced facial expression tracking](https://hinzka.hatenablog.com/entry/2020/08/15/145040)

https://docs.unity3d.com/ja/Packages/com.unity.xr.arkit@5.1/api/UnityEngine.XR.ARKit.ARKitBlendShapeLocation.html

**VRM0 vs VRM1 Incompatibility Warning**

Existing VMCProtcol compliant applications use VRM0.  
VRM0 and VRM1 have different preset Expressions (VRM0.x : BlendShape).

When using a VRM1-based VRM SDK, VRM0 models are also converted to VRM1 format during automatic migration.

Please refer to the [VRM official website for changes. ](https://vrm.dev/vrm1/changed.html)

To maintain compatibility with existing VMCProtocol applications that use the VRM0 format,

+ Senders using VRM1 system must implement transmission in VRM0 format.
   However, it is recommended to prepare transmission in VRM1 format as an option.
+ Recipients using VRM1 system should convert to VRM1 format and process when receiving in VRM0 format.

|VRM0 Preset BlendShape|VRM1 Preset Expressions|
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

### Camrea Transform&FOV
```
V2.1
/VMC/Ext/Cam (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){fov} 
```

p=Position  
q=Quaternion  
fov=FOV  
  
### Controller Input
```
V2.1
/VMC/Ext/Con (int){active} (string){name} (int){IsLeft} (int){IsTouch} (int){IsAxis} (float){Axis.x} (float){Axis.y} (float){Axis.z} 
```

Controller button and axis. (Its dependent to sender applications.)  
active 1=press, 0=release, 2=change Axis.  

### 【Event send】keyboard Input
```
V2.1
/VMC/Ext/Key (int){active} (string){name} (int){keycode}
```

Keyboard input. (Its dependent to sender applications.)  
active 1=press, 0=release.  

### 【Event send】MIDI Note Input
```
V2.2
/VMC/Ext/Midi/Note (int){active} (int){channel} (int){note} (float){velocity}
```

MIDI note in.  
active 1=press, 0=release.  

### 【Event send】MIDI CC Value Input
```
V2.2
/VMC/Ext/Midi/CC/Val (int){knob} (float){value}
```

MIDI CC value.  

### 【Event send】MIDI CC Button Input
```
V2.2
/VMC/Ext/Midi/CC/Bit (int){knob} (int){active}
```

MIDI CC buttons.  
active 1=press, 0=release.  

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

Tracker device transform.  

Pos = avatar scale.  
Pos/Local = device raw scale.  
serial = OpenVR Serial No.    
p=Position  
q=Quaternion   

### 【low frequency】Receive enable
```
v2.4
/VMC/Ext/Rcv (int){enable:0 or 1} (int){port:0～65535}  
  
v2.7
/VMC/Ext/Rcv (int){enable:0 or 1} (int){port:0～65535} (string){IP Address}  
```

VMC side receive function is enabled or not.
1=enable, 0=disable.  
**V2.7 spec is not implemented in Virtual motion capture**

### 【low frequency】DirectionalLight transform & color
```
V2.4
/VMC/Ext/Light (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){color.red} (float){color.green} (float){color.blue} (float){color.alpha} 
```

VMC DirectionalLight position and color.  
p=Position  
q=Quaternion   
color=color  

### 【low frequency】Local VRM information
```
V2.4
/VMC/Ext/VRM (string){path} (string){title}  
  
V2.7
/VMC/Ext/VRM (string){path} (string){title} (string){Hash} 
```

VMC loaded local vrm file info.  
path = local file path.  

Difference detection is recommended.  

Note: The user needs already agreed to the VRM license. 

**V2.7 spec is not implemented in Virtual motion capture**

### 【low frequency】Remote VRM information
```
V3.0
/VMC/Ext/Remote (string){service} (string){json} 
```

VMC loaded online service info.
  
service = service name(lower case).  
json = service info.
  
Note: The user needs already agreed to the VRM license. 
  
```
Service example 
/VMC/Ext/Remote vroidhub {"characterModelId":"123456789456"}
/VMC/Ext/Remote dmmvrconnect {"user_id":"123456789456", "avatar_id":"123456789456"}
```


### 【low frequency】Option string
```
V2.4
/VMC/Ext/Opt (string){option} 
```

General-purpose setting character string (recipient arbitrary definition)

### 【low frequency】background color
```
V2.4
/VMC/Ext/Setting/Color (float){r} (float){g} (float){b} (float){a} 
```

### 【low frequency】Window attribute
```
V2.4
/VMC/Ext/Setting/Win (int){IsTopMost} (int){IsTransparent} (int){WindowClickThrough} (int){HideBorder} 
```

1=true,0=false  

### 【low frequency】Loaded setting path
```
V2.5
/VMC/Ext/Config (string){path} 
```

VMC Setting(Profile) path.

### Thru info
```
V2.6
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (float){arg2} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (int){arg2} 
```

xxxxxxxxx is any name.  
vendor specific.  
performer must through out from assistant to marionette.  

# Performer(Motion sender application)
Marionette → Performer or  
Assistant → Performer specifications.

### Virtual Device Transform
```
V2.3
/VMC/Ext/Hmd/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Con/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
/VMC/Ext/Tra/Pos (string){serial} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w}  
```

Virtual device transform. 

Pos = avatar scale.  
Pos/Local = device raw scale.  
serial = OpenVR Serial No.  
p=Position  
q=Quaternion  

### Frame Period
```
V2.3
/VMC/Ext/Set/Period (int){Status} (int){Root} (int){Bone} (int){BlendShape} (int){Camera} (int){Devices} 
```

set send period.
value = 1/x Frame.

### Virtual MIDI CC Value Input
```
V2.3
/VMC/Ext/Midi/CC/Val (int){knob} (float){value}
``` 

### Virtual Camrea Transform&FOV
```
V2.3
/VMC/Ext/Cam (string){Camera} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){fov} 
```

### VRM BlendShapeProxyValue
```
V2.3
/VMC/Ext/Blend/Val (string){name} (float){value}  
/VMC/Ext/Blend/Apply
```

Note that due to changes in the UniVRM specification, it is Case Sensitive.  
In permissive applications (including those using EVMC4U), case sensitivity is ignored, mainly to maintain compatibility, but otherwise it does not work properly.  

[See here for PerfectSync used for advanced facial expression tracking](https://hinzka.hatenablog.com/entry/2020/08/15/145040)

https://docs.unity3d.com/ja/Packages/com.unity.xr.arkit@5.1/api/UnityEngine.XR.ARKit.ARKitBlendShapeLocation.html

**VRM0 vs VRM1 Incompatibility Warning**

Existing VMCProtcol compliant applications use VRM0.  
VRM0 and VRM1 have different preset Expressions (VRM0.x : BlendShape).

When using a VRM1-based VRM SDK, VRM0 models are also converted to VRM1 format during automatic migration.

Please refer to [the VRM official website for changes. ](https://vrm.dev/vrm1/changed.html)

To maintain compatibility with existing VMCProtocol applications that use the VRM0 format,

+ Senders using VRM1 system must implement transmission in VRM0 format.
   However, it is recommended to prepare transmission in VRM1 format as an option.
+ Recipients using VRM1 system should convert to VRM1 format and process when receiving in VRM0 format.

|VRM0 Preset BlendShape|VRM1 Preset Expressions|
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

Eye tracking target object position.  
V2.8~: Head relative position.  
V2.3~V2.7: absolute position. 
  

### 【Event send】Information send request
```
V2.4
/VMC/Ext/Set/Req 
```

Request immediate transmission.

### 【Event send】Response string
```
V2.4
/VMC/Ext/Set/Res (string){Response} 
```

General-purpose string (sender arbitrary definition)

### 【Event send】calibration/calibration ready request
```
V2.5
/VMC/Ext/Set/Calib/Ready  
/VMC/Ext/Set/Calib/Exec (int){mode}   
```
Ready: Request calibration ready
Exec: Request calibration execute (0=Normal,1=MR Normal,2=MR floor fix)  
  
Please allow enough time between Ready and Exec.

### 【Event send】Request load setting file
```
V2.5
/VMC/Ext/Set/Config (string){Path} 
```

### Thru info
```
V2.6
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (float){arg2} 
/VMC/Thru/xxxxxxxxx/xxxxxxxxx (string){arg1} (int){arg2} 
```

xxxxxxxxx is any name.  
vendor specific.  
performer must through out from assistant to marionette.    

### DirectionalLight transform & color
```
V2.9
/VMC/Ext/Light (string){name} (float){p.x} (float){p.y} (float){p.z} (float){q.x} (float){q.y} (float){q.z} (float){q.w} (float){color.red} (float){color.green} (float){color.blue} (float){color.alpha} 
```

VMC DirectionalLight position and color.  
p=Position  
q=Quaternion   
color=color  

### 【Event send】Call Shortcut
```
V3.1
/VMC/Ext/Set/Shortcut (string){shortcut}
```
Call VMC Shortcut

example

```
/VMC/Ext/Set/Shortcut Functions.FreeCamera
/VMC/Ext/Set/Shortcut Functions.ColorGreen
/VMC/Ext/Set/Shortcut Functions.ShowCalibrationWindow
```
