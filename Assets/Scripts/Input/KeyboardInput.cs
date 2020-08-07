using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Messenger.Broadcast(GameEvent.TARGET_UNSELECTED);
        }


    }
}
