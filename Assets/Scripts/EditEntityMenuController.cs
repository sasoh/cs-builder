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
        Debug.Log("and back");
    }

    public void DidPressSaveAndPlace() {
        Debug.Log(constructedEntity.ToString());
        Debug.Log($"and continue to placing");
    }
}
