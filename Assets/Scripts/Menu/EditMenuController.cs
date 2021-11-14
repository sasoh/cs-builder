using UnityEngine;

public class EditMenuController: ReloadableMenuController {
    public GameObject menuEditEntityPrefab;
    public EditMenuEntitiesListController entitiesListController;
    public GameObject savedEntityPanel;
    int currentlySelectedIndex = -1;
    public GameObject editMapPrefab;

    void Awake() {
        Debug.Assert(menuEditEntityPrefab != null);
        Debug.Assert(entitiesListController != null);
        Debug.Assert(savedEntityPanel != null);
        Debug.Assert(editMapPrefab != null);
    }

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
        Utils.SaveEntities(new CSEntityList());
        entitiesListController.Reload();
    }

    public void DidPressDeleteSaved() {
        savedEntityPanel.SetActive(false);

        if (currentlySelectedIndex != -1) {
            var saved = Utils.GetSavedEntities();
            saved.entities.RemoveAt(currentlySelectedIndex);
            Utils.SaveEntities(saved);
            ShouldReload();
        }
    }

    public void DidPressPlaceSaved() {
        savedEntityPanel.SetActive(false);
        MenuController.OpenMenu(editMapPrefab);
    }

    public void DidPressCancelSaved() {
        savedEntityPanel.SetActive(false);
    }

    public override void ShouldReload() {
        entitiesListController.Reload();
    }
}
