using System.Collections.Generic;
using UnityEngine;

public class EditMenuController: ReloadableMenuController {
    public GameObject menuEditEntityPrefab;
    public EditMenuEntitiesListController entitiesListController;
    public GameObject savedEntityPanel;
    int currentlySelectedIndex = -1;

    void Start() {
        Debug.Assert(menuEditEntityPrefab != null);
        entitiesListController.didPressNewButton = () => { MenuController.OpenMenu(menuEditEntityPrefab); };
        entitiesListController.didPressSavedButton = (selectedIndex) => {
            savedEntityPanel.SetActive(true);
            currentlySelectedIndex = selectedIndex;
        };
        savedEntityPanel.SetActive(false);
    }

    public void DidPressDeleteAllSaved() {
        Utils.SaveEntities(new List<CSEntity>());
        entitiesListController.Reload();
    }

    public void DidPressDeleteSaved() {
        savedEntityPanel.SetActive(false);

        if (currentlySelectedIndex != -1) {
            var saved = Utils.GetSavedEntities();
            saved.RemoveAt(currentlySelectedIndex);
            Utils.SaveEntities(saved);
            ShouldReload();
        }
    }

    public void DidPressPlaceSaved() {
        savedEntityPanel.SetActive(false);
        Debug.Log($"Go to placing {currentlySelectedIndex}");
    }

    public void DidPressCancelSaved() {
        savedEntityPanel.SetActive(false);
    }

    public override void ShouldReload() {
        entitiesListController.Reload();
    }
}
