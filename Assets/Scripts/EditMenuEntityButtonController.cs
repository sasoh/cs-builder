using System;
using UnityEngine;

public class EditMenuEntityButtonController: MonoBehaviour {
    public Action didPressButton;
    public TMPro.TMP_Text text;
    public int serializationIndex;

    public void Configure(CSEntity current) {
        text.text = current.name;

        if (string.IsNullOrEmpty(current.name) == true) {
            text.text = "Unnamed";
        }
    }

    public void DidPressButton() {
        didPressButton?.Invoke();
    }
}
