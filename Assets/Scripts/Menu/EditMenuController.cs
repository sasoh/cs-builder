using System.Collections.Generic;
using UnityEngine;

public class EditMenuController: ReloadableMenuController {
    public GameObject menuEditEntityPrefab;
    public EditMenuEntitiesListController entitiesListController;
    int currentlySelectedIndex = -1;
    public GameObject editMapPrefab;

    void Awake() {
        Debug.Assert(menuEditEntityPrefab != null);
        Debug.Assert(entitiesListController != null);
        Debug.Assert(editMapPrefab != null);
    }

    void Start() {
        Debug.Assert(menuEditEntityPrefab != null);
        entitiesListController.didPressNewButton = () => { MenuController.OpenMenu(menuEditEntityPrefab); };
        entitiesListController.didPressSavedButton = (selectedIndex) => {
            currentlySelectedIndex = selectedIndex;

            var popupConfig = new PopupConfiguration {
                title = $"Select action",
                buttons = new List<PopupButtonConfiguration> {
                    new PopupButtonConfiguration {
                        buttonText = "Place",
                        didClickButton = DidPressPlaceSaved
                    },
                    new PopupButtonConfiguration {
                        buttonText = "Delete",
                        didClickButton = DidPressDeleteSaved
                    },
                    new PopupButtonConfiguration {
                        buttonText = "Cancel",
                    },
                }
            };
            MenuController.ShowPopup(popupConfig);
        };
    }

    public void DidPressDeleteAllSaved() {
        var popupConfig = new PopupConfiguration {
            title = $"Delete all saved entities?",
            buttons = new List<PopupButtonConfiguration> {
                new PopupButtonConfiguration {
                    buttonText = "Delete",
                    didClickButton = () => {
                        Utils.SaveEntities(new CSEntityList());
                        entitiesListController.Reload();
                    }
                },
                new PopupButtonConfiguration {
                    buttonText = "Cancel",
                },
            }
        };
        MenuController.ShowPopup(popupConfig);
    }

    public void DidPressDeleteSaved() {
        if (currentlySelectedIndex != -1) {
            var saved = Utils.GetSavedEntities();
            saved.entities.RemoveAt(currentlySelectedIndex);
            Utils.SaveEntities(saved);
            ShouldReload();
        }
    }

    public void DidPressPlaceSaved() {
        var saved = Utils.GetSavedEntities();
        MapController.instance.currentlyPlacedEntity = saved.entities[currentlySelectedIndex];
        MenuController.OpenMenu(editMapPrefab);
    }

    public override void ShouldReload() {
        entitiesListController.Reload();
    }
}
