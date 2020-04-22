using UnityEngine;
using UnityEngine.EventSystems;

// https://answers.unity.com/questions/943854/ui-46-disable-mouse-from-stealing-focus.html
public class IgnoreMouseFocus : MonoBehaviour {
    GameObject lastselect;
    void Start() {
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
