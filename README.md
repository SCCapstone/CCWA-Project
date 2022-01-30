# Bootymeat
## Project Name
Our project is a game 1-2 player game called Charlie and Charlinda’s Wacky Adventure (name in progress)  that we are all creating in Unity using C# scripts to handle user input, enemy ai, character interactions, etc.The layout of each floor and room will be procedurally generated so that the player has a new experience each time they play the game through from the beginning to end. The player will  have access to various options that will allow them to have a unique customized experience, such as changing the difficulty of their game and entering a speedrun mode. The game will also have three save file slots that will keep track of top runs and achievements for each file. 
## External Requirements

In order to build this project you need to install: * [Unity](https://unity.com/)

## Setup

This repository should be cloned into the directory where Unity projects are stored on the machine. 

After cloning the repository, open the folder in Unity as an existing project.

## Running

Clone the repository or download the zip file
Open Unity
Navigate to the project folder and open it

## Deployment

After each time the game code is changed, the executable is created by utilizing the build function in the unity editor.


# Testing

All tests are contained in the `Assets/Tests` directory, with Unit Tests in `Tests/Unit_Tests` and Behavioral Tests in `Tests/Behavioral_Tests`.

Tests can be run from the root directory of this repository with the following commands:
- To run the unit tests: `PATH/TO/UNITY_EXECUTABLE -runTests -batchmode -projectPath ./ -testResults ./TestResults/tests.xml`
- To run the behavioral tests:  `PATH/TO/UNITY_EXECUTABLE -runTests -testPlatform PlayMode -batchmode -projectPath ./ -testResults ./TestResults/tests.xml`

In Windows, the Unity executable should be located at `"C:\Program Files\Unity\Hub\Editor\2020.3.19f1\Editor\Unity.exe"`

The results of the tests will be saved in `TestResults/tests.xml`.

Please note that the tests will take a few seconds to run.

## Testing Technology

We have identified the testing framework we will be using as NUnit/XUnit through the Unity Test Framework.

## Running Tests

We will be implementing tests in CSCE 492

## Authors

- Ian McDowell (mcdoweli@email.sc.edu)
- Lance Kevin  (lkevin@email.sc.edu)
- Miles Littlejohn (mjl6@email.sc.edu)
- Jonathan Terry (jonathanterry935@gmail.com)
- Nicholas Del Gigante (nbd@email.sc.edu)
