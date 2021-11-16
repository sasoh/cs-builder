using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PopupConfiguration {
    public string title;
    public List<PopupButtonConfiguration> buttons;
}

public class PopupController: MonoBehaviour {
    public TMPro.TMP_Text titleText;
    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    void Awake() {
        Debug.Assert(titleText != null);
        Debug.Assert(buttonPrefab != null);
        Debug.Assert(buttonContainer != null);
    }

    public void Configure(PopupConfiguration configuration) {
        titleText.text = configuration.title;
        Utils.RemoveChildren(buttonContainer.transform);

        if (configuration.buttons != null) {
            foreach (var b in configuration.buttons) {
                var instance = Instantiate(buttonPrefab, buttonContainer.transform);
                if (instance.TryGetComponent(out PopupButton p) == true) {
                    p.Configure(b);
                }
            }
        }
    }
}
