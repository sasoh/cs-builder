using System.Collections.Generic;
using UnityEngine;

public interface IMapEditor {
    public void SetShowConfirmationInfoPanel(bool status);
    public void SetShowPlacementInfoPanel(bool status);
    public void DidModifyMap();
}

public class EditMapMenuController: MonoBehaviour, IMapEditor {
    public GameObject placementInfoObject;
    public GameObject confirmationInfoObject;
    bool mapModified;

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
        DidModifyMap();
    }

    //public void DidPressSaveMap() {
    //    MapController.instance?.SaveMap();
    //    mapModified = false;
    //}

    public void SetShowPlacementInfoPanel(bool status) {
        placementInfoObject.SetActive(status);
    }

    public void SetShowConfirmationInfoPanel(bool status) {
        confirmationInfoObject.SetActive(status);
    }

    public void DidModifyMap() {
        mapModified = true;
    }

    public void DidPressBack() {
        if (mapModified == true) {
            var popupConfig = new PopupConfiguration {
                title = $"The map was modified, do you want to save changes?",
                buttons = new List<PopupButtonConfiguration> {
                    new PopupButtonConfiguration {
                        buttonText = "Save",
                        didClickButton = () => {
                            MapController.instance?.SaveMap();
                            MenuController.DidPressBack();
                        }
                    },
                    new PopupButtonConfiguration {
                        buttonText = "Don't save",
                        didClickButton = () => {
                            MenuController.DidPressBack();
                        }
                    },
                    new PopupButtonConfiguration {
                        buttonText = "Cancel",
                    }
                }
            };
            MenuController.ShowPopup(popupConfig);
        }
        else {
            MenuController.DidPressBack();
        }
    }
}
