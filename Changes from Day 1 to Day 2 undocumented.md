# UniteTrainingDayPhase1 --> Phase 2 Undocumented Changes
If you want to follow the Adventure game tutorial as a full front to back, starting as UniteTrainingDayPhase1 Project and finishing with it, follow the undocumented changes below.

**Note: Prefabs to save it!** If you want to save it all around the project, don't forget to save these changes to the **Prefab**.

Unsolved undocumented changes:

* When transitioning from SecurityRoom to Market, the players position is not reset near the interactable object (DoorToMarket/DoorToSecurityRoom)

## Add:
### Scenes/Persistent
Hierarchy: EventSystem

Hierarchy/EventSystem: Drag Threshold: 5 (instead of 10)
### Scripts/MonoBehavious/Player/PlayerMovement
```csharp
public const string startingPositionKey = "starting position";
public SaveData playerSaveData;
```
Start Function: 
```csharp
string startingPositionName = "";
playerSaveData.Load(startingPositionKey, ref startingPositionName);
Transform startingPosition = StartingPosition.FindStartingPosition(startingPositionName);

transform.position = startingPosition.position;
transform.rotation = startingPosition.rotation;
```
### Hierarchy/Player (on each Scene, so Scenes/Market AND Scenes/SecurityRoom)
Player Movement (Script): Add PlayerSaveData to Player Save Data field


## Remove:
Scenes/SecurityRoom: EventSystem (This is moved to Persistent scene)

Scripts/MonoBehavious/Inventory/Inventory

Editor/Interaction/Inventory/InventoryEditor

## Change:
### Scenes/Market
Add Player Prefab to Market Scene

Hierarchy: Market: Camera Rig: Add Player (Transform) to Player Position
### Add Event Triggers (OnInteractableClick/OnGroundClick):
* Hierarchy: BirdInteractable
* Hierarchy: GlassesInteractable
* Hierarchy: CoffeeBotInteractable
* Hierarchy: CoinInteractable
* Hierarchy: DoorToSecurityRoomInteractable
* Hierarchy: FitVendorInterable
* Hierarchy: Market

