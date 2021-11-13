using System.Collections.Generic;
using UnityEngine;

public class EditEntityMenuController: MonoBehaviour {
    public EditEntityAppearanceController appearanceController;
    public EditEntityBehaviourListController behaviourController;
    CSEntity constructedEntity = new CSEntity();
    public TMPro.TMP_InputField nameField;

    void Start() {
        appearanceController.didChooseAppearance += DidSelectAppearance;
        behaviourController.didChooseBehaviours += DidUpdateBehaviours;

        var saved = Utils.GetSavedEntities();
        nameField.text = $"Entity {saved.entities.Count}";
    }

    void DidSelectAppearance(int selectedShapeIndex) {
        constructedEntity.shapeIndex = selectedShapeIndex;
    }

    void DidUpdateBehaviours(List<CSBehaviour> included) {
        constructedEntity.behaviours = new List<CSBehaviour>(included);
    }

    public void DidPressSave() {
        constructedEntity.name = nameField.text;

        var saved = Utils.GetSavedEntities();
        saved.entities.Add(constructedEntity);
        Utils.SaveEntities(saved);

        MenuController.DidPressBack();
    }

    public void DidPressSaveAndPlace() {
        constructedEntity.name = nameField.text;

        var saved = Utils.GetSavedEntities();
        saved.entities.Add(constructedEntity);
        Utils.SaveEntities(saved);

        Debug.Log($"and continue to placing");
    }
}
