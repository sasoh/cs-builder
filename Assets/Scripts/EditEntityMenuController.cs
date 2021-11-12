using System.Collections.Generic;
using UnityEngine;

public class EditEntityMenuController: MonoBehaviour {
    public EditEntityAppearanceController appearanceController;
    public EditEntityBehaviourListController behaviourController;
    CSEntity constructedEntity = new CSEntity();

    void Start() {
        appearanceController.didChooseAppearance += DidSelectAppearance;
        behaviourController.didChooseBehaviours += DidUpdateBehaviours;
    }

    void DidSelectAppearance(int selectedShapeIndex) {
        constructedEntity.shapeIndex = selectedShapeIndex;
    }

    void DidUpdateBehaviours(List<int> included) {
        constructedEntity.behaviours = new List<int>(included);
    }

    public void DidPressSave() {
        Debug.Log(constructedEntity.ToString());

        var saved = Utils.GetSavedEntities();
        saved.Add(constructedEntity);
        Utils.SaveEntities(saved);

        MenuController.DidPressBack();
    }

    public void DidPressSaveAndPlace() {
        Debug.Log(constructedEntity.ToString());

        var saved = Utils.GetSavedEntities();
        saved.Add(constructedEntity);
        Utils.SaveEntities(saved);

        Debug.Log($"and continue to placing");
    }
}
