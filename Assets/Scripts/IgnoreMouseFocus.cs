using UnityEngine;
using UnityEngine.EventSystems;

// https://answers.unity.com/questions/943854/ui-46-disable-mouse-from-stealing-focus.html
public class IgnoreMouseFocus : MonoBehaviour {
    GameObject lastselect;
    void Start() {
        Debug.Log("Ignore mouse interations");
        // https://answers.unity.com/questions/1507986/disable-mouse-input-and-cursor-in-game.html
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lastselect = new GameObject();
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastselect);
        } else {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
