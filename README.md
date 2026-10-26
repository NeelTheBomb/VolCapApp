1 Introduction

This project aims to create a headset-based viewer and interaction environment for volumetric captures, enabling users to review, explore, and discuss captured motion in an embodied, spatial way. Rather than focusing on capture or reconstruction, this project centers on human interaction with volumetric data: playback, navigation, annotation, and shared understanding. The aim is to apply it to sports analysis, where it can help uncover movement deficiencies during athletes’ training.

2 Main Features
2.1 Play and Pause

After becoming familiar with the XRI Toolkit in Unity, play and pause controls were created for the imported spatial video using UI buttons on a canvas. I made simple C# script that lets this function be controlled with a single button rather than two separate “play” and “pause” buttons, making it feel more like a real-life media feature.

2.2 Playback

Created the playback feature by adding a slider UI to the canvas. Then created a script that maps the slider’s position to the spatial video's playback time. To avoid glitches while scrubbing, I specified that the video should pause temporarily while the slider is being dragged and resume once the user stops dragging.

2.3 Spatial Calibration

When the right controller is placed on the ground at the desired origin point, the spatial video and UI are calibrated after the trigger button on the left controller is clicked. 

2.4 Annotation

The annotation feature was added by implementing through a custom script. Users can draw on the athlete’s body in 3D for training purposes. Annotations can be undone or cleared by pressing buttons A and B on the right controller. Saving annotations and changing annotation colors are planned for future development. 

2.5 Media Server

Created a media server that lets users select from videos and import them into the scene. 

2.6 UI design

Designed the UI for this project, including a theme color, font style, and icons. Then implemented the design in Unity, creating one main panel and two expandable panels for annotation and the media server. In the future, plan to refine the font style and add more functionality to the panels. 
