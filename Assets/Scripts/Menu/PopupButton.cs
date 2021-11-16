using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct PopupButtonConfiguration {
    public string buttonText;
    public Action didClickButton;
}

public class PopupButton: MonoBehaviour {
    public TMPro.TMP_Text buttonText;
    public Button attachedButton;

    void Awake() {
        Debug.Assert(buttonText != null);
        Debug.Assert(attachedButton != null);
    }

    public void Configure(PopupButtonConfiguration configuration) {
        buttonText.text = configuration.buttonText;
        attachedButton.onClick.AddListener(() => {
            configuration.didClickButton?.Invoke();
            MenuController.ClosePopup();
        });
    }
}
