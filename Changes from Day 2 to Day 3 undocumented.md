# Changes from Day 2 to Day 3 undocumented

If you want to follow the Adventure game tutorial as a full front to back, starting as UniteTrainingDayPhase1 Project and finishing with it, follow the undocumented changes below.

**Note: Prefabs to save it!** If you want to save it all around the project, don't forget to save these changes to the **Prefab**.

## Remove:

SecurityRoom/LaserGrindInteractable: The Laseroff Interaction Collection

## Change:
Scripts/Editor/Interaction/Conditions/ConditionCollectionEditor:
* Remove CustomEditor above the class
* OnEnable: Remove the _CheckAndCreateSubEditors_ call at the end of the method
* OnInspectorGui Method should be blank
* CreateConditionCollection: Only the newConditionCollection creation & return should stay

Scripts/ScriptableObjects/Interaction/Conditions/ConditionCollection: Only leave the method, which returns a default return true, so:

```csharp
using UnityEngine;

public class ConditionCollection : ScriptableObject
{
    public bool CheckAndReact()
    {
        return true;
    }
}
```

