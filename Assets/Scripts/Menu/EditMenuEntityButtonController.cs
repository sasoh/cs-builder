using System;
using UnityEngine;
using UnityEngine.UI;

public class EditMenuEntityButtonController: MonoBehaviour {
    public CSEntityConfigurationScriptableObject shapeData;
    public Action didPressButton;
    public TMPro.TMP_Text text;
    public Image entityImage;
    public int serializationIndex;

    void Awake() {
        Debug.Assert(shapeData != null && shapeData.configuration.Length > 0);
        Debug.Assert(text != null);
        Debug.Assert(entityImage != null);
    }

    public void Configure(CSEntity current, int index, Action didPressAction) {
        text.text = current.name;
        if (string.IsNullOrEmpty(current.name) == true) {
            text.text = "Unnamed";
        }
        serializationIndex = index;
        didPressButton += didPressAction;
        if (current.shapeIndex < shapeData.configuration.Length) {
            entityImage.sprite = shapeData.configuration[current.shapeIndex].buttonSprite;
        }
    }

    public void Configure(Action didPressAction) {
        text.text = "New";
        entityImage.sprite = shapeData.newEntityImage;
        didPressButton += didPressAction;
    }

    public void DidPressButton() {
        didPressButton?.Invoke();
    }
}
