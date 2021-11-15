using UnityEngine;

public interface IMapEditor {
    public void SetShowConfirmationInfoPanel(bool status);
    public void SetShowPlacementInfoPanel(bool status);
}

public class EditMapMenuController: MonoBehaviour, IMapEditor {
    public GameObject placementInfoObject;
    public GameObject confirmationInfoObject;

    void Awake() {
        Debug.Assert(placementInfoObject != null);
        Debug.Assert(confirmationInfoObject != null);
        placementInfoObject.SetActive(false);
        confirmationInfoObject.SetActive(false);
    }

    void Start() {
        MapController.instance?.SetEditMode(true, this);
    }

    void OnDisable() {
        MapController.instance?.SetEditMode(false, null);
    }

    public void DidPressClearMap() {
        MapController.instance?.ClearMap();
    }

    public void DidPressSaveMap() {
        MapController.instance?.SaveMap();
    }

    public void SetShowPlacementInfoPanel(bool status) {
        placementInfoObject.SetActive(status);
    }

    public void SetShowConfirmationInfoPanel(bool status) {
        confirmationInfoObject.SetActive(status);
    }
}
