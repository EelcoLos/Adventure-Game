# Changes from Day 5 to Day 6 undocumented

If you want to follow the Adventure game tutorial as a full front to back, starting as UniteTrainingDayPhase1 Project and finishing with it, follow the undocumented changes below.

**Note: Prefabs to save it!** If you want to save it all around the project, don't forget to save these changes to the **Prefab**.

## Add:
## Remove:
## Change:

Scripts/MonoBehaviours/SceneControl/SceneController:

Copy below code to overwrite the original SceneController

```csharp
using System;
using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;


    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1f;
    public string startingSceneName = "SecurityRoom";
    public string initialStartingPositionName = "DoorToMarket";
    public SaveData playerSaveData;


    private IEnumerator Start ()
    {
        faderCanvasGroup.alpha = 1f;

        playerSaveData.Save (PlayerMovement.startingPositionKey, initialStartingPositionName);

        yield break;
    }


    public void FadeAndLoadScene (SceneReaction sceneReaction)
    {
      
    }
}
```