using UnityEngine;

public class EditMenuController: MonoBehaviour {
    public GameObject menuEditEntityPrefab;

    void Start() {
        Debug.Assert(menuEditEntityPrefab != null);
    }

    public void DidPressEditEntity() {
        MenuController.OpenMenu(menuEditEntityPrefab);
    }
}
