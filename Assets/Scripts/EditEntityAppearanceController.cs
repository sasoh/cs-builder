using System;
using UnityEngine;
using UnityEngine.UI;

public class EditEntityAppearanceController: MonoBehaviour {
    public Button[] shapeButtons;
    int selectedShapeIndex;
    public Action<int> didChooseAppearance;

    void Start() {
        Debug.Assert(shapeButtons != null && shapeButtons.Length > 0);
        DidPressShapeButton(0);
    }

    public void DidPressShapeButton(int index) {
        selectedShapeIndex = index;

        foreach (var s in shapeButtons) {
            s.transform.localScale = 0.9f * Vector2.one;
        }

        var selectedButton = shapeButtons[selectedShapeIndex];
        selectedButton.transform.localScale = Vector2.one;
        didChooseAppearance?.Invoke(selectedShapeIndex);
    }
}
