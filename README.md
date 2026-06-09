# Rock Paper Scissors with Leap Motion

A Unity game that allows a player to play Rock-Paper-Scissors against the computer using hand gestures captured by a Leap Motion sensor.

The project demonstrates real-time hand tracking, gesture recognition, and natural user interaction without traditional input devices.

## Features

* Hand tracking using Leap Motion
* Gesture recognition for Rock, Paper, and Scissors
* Real-time interaction with a 3D user interface
* Animated computer opponent
* Optional "cheating mode" where the computer always wins
* Built in Unity

## How It Works

The Leap Motion sensor tracks the player's hand and provides joint rotations for each finger. A K-Nearest Neighbors (KNN) classifier is used to recognize the player's gesture.

Training data was collected by recording hand poses for the three possible gestures. During gameplay, the current hand pose is compared against the recorded samples and classified as Rock, Paper, or Scissors.

## Requirements

* Windows
* Unity
* Leap Motion Hand Tracking Software
* Leap Motion Controller
* Install Ultraleap's Hand Tracking Software: https://developer.leapmotion.com/tracking-software-download

## Controls

* Use your index finger to interact with the in-game buttons.
* Touch the **Play** button to start a round.
* Press **ESC** to exit the game.
* Enable **Cheating Mode** to let the computer always select the winning move.

## Technical Details

The gesture recognition system uses:

* Finger joint rotation angles as features
* K-Nearest Neighbors (KNN) classification
* Distance metrics based on angular differences between hand poses
* Outlier rejection for improved robustness

## Known Limitations

Recognition accuracy is very high for Rock and Paper. Scissors can occasionally be misclassified when one finger occludes another from the sensor's viewpoint.


