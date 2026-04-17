# FriendRFoe-GDM-Final-Project
Multiplayer platformer meyhem
## Setup

### Prerequisites
- Unity 2022.3 LTS or later
- Git

### Installation

1. Clone this repository:
```bash
git clone https://github.com/tbagnarqa/Friend-R-Foe-GDM-Final-Project.git
```
3. Open the project in Unity Hub
4. Open the Players scene in Assets/Scenes/
5. Press Play to test in the editor

## How to Play

### Controls
- WASD: Move
- Spacebar: Jump

### Objective
Reach the goal of each level and collect as many of the collectibles as possible or do so in as little time as possible.

## Testing Multiplayer

### Option 1: Build and Run Two Instances
1. Build the project (File → Build Settings → Build)
2. Run the built executable
3. Press Play in the Unity Editor
4. In one instance, click "Host Game"
5. In the other instance, click "Join Game" and enter 127.0.0.1

### Option 2: Using ParrelSync (if installed)
1. Open ParrelSync → Clones Manager
2. Create a clone of the project
3. Open the clone in a separate Unity Editor
4. Run both editors simultaneously

## Project Structure

FriendRFoe-GDM-Final-Project/  
├── README.md    
├── Assets/    
│   ├── Scenes/  
│   │   │── Levels/  
│   │   │   ├── 1 - 1.unity  
│   │   │   ├── 1 - 2.unity  
│   │   ├── GameEnd.unity  
│   │   ├── GameOver.unity  
│   │   ├── Pause.unity  
│   │   ├── Players.unity  
│   │   └── World.unity  
│   ├── Scripts/  
│   │   ├── ManagersSingletons/  
│   │   ├── PlayerJoining/  
│   │   ├── SceneManagers/  
│   │   ├── InGame/  
│   │   └── InMap/  
│   ├── Prefabs/  
│   └── Audio/  
├── Packages/  
└── ProjectSettings/  

## Technical Implementation

**Singleton Pattern**
- Location: Assets/Scripts/ManagersSingletons/GameManager.cs
- Description: Manages game state across scenes

**Delegate**
- Location: Assets/Scripts/ManagersSingletons/GameManager.cs
- Description: Manages game state across scenes

**Object Pool Pattern**
- Location: Assets/Scripts/ManagersSingletons/CoinPoolManager.cs
- Description: Pools coin objects for quicker loadtimes.

**Database Integration**
- Location: Assets/Scripts/ManagersSingletons/DatabaseManager.cs
- Description: Used to create and manage a database for player data storage.

**Audio Implmentation**
- Location: Assets/Scripts/ManagersSingletons/AudioManager.cs
- Description: Listens for events that play audio and then plays the proper audio.

**Save/load System**
- Location: Assets/Scripts/PlayerJoining/SaveLoadManager.cs
- Description: Saves player data to disk.

## Known Issues

- None as of now.

## Future Enhancements

- Many more levels
- Power-ups
- More enemies
- More settings in the pause menu

## Technologies Used

- **Unity 2022.3 LTS**: Game engine
- **Netcode for GameObjects**: Multiplayer networking
- **SQLite**: Database for persistent storage
- **TextMeshPro**: UI text rendering
