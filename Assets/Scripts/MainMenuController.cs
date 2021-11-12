using UnityEngine;

public class MainMenuController: MonoBehaviour {
    public GameObject menuEditPrefab;
    public GameObject menuPlayPrefab;

    void Start() {
        Debug.Assert(menuEditPrefab != null);
        Debug.Assert(menuPlayPrefab != null);
    }

    public void DidPressPlay() {
        MenuController.OpenMenu(menuPlayPrefab);
    }

    public void DidPressEdit() {
        MenuController.OpenMenu(menuEditPrefab);
    }
}
