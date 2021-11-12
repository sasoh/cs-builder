using System;
using UnityEngine;

public class EditMenuEntityButtonController: MonoBehaviour {
    public Action didPressButton;
    public TMPro.TMP_Text text;

    public void DidPressButton() {
        didPressButton?.Invoke();
    }
}
