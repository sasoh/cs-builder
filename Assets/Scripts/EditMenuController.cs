using System.Collections.Generic;
using UnityEngine;

public class EditMenuController: ReloadableMenuController {
    public GameObject menuEditEntityPrefab;
    public EditMenuEntitiesListController entitiesListController;

    void Start() {
        Debug.Assert(menuEditEntityPrefab != null);
        entitiesListController.didPressNewButton = () => { MenuController.OpenMenu(menuEditEntityPrefab); };
    }

    public void DidPressDeleteSaved() {
        Utils.SaveEntities(new List<CSEntity>());
        entitiesListController.Reload();
    }

    public override void ShouldReload() {
        entitiesListController.Reload();
    }
}
