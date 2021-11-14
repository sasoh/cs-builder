using System.Collections.Generic;
using UnityEngine;

public class EditEntityMenuController: MonoBehaviour {
    public EditEntityAppearanceController appearanceController;
    public EditEntityBehaviourListController behaviourController;
    CSEntity constructedEntity = new CSEntity();
    public TMPro.TMP_InputField nameField;
    public GameObject editMapPrefab;

    void Awake() {
        Debug.Assert(appearanceController != null);
        Debug.Assert(behaviourController != null);
        Debug.Assert(nameField != null);
        Debug.Assert(editMapPrefab != null);
    }

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
        DidPressSave();
        MenuController.OpenMenu(editMapPrefab);
    }
}
