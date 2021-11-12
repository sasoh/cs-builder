using UnityEngine;
using UnityEngine.UI;

public class BehaviourButtonController: MonoBehaviour {
    public Image stateImage;
    public Sprite includedSprite;
    public Sprite excludedSprite;
    public Button attachedButton;
    public TMPro.TMP_Text nameText;
    bool isIncluded;

    void Awake() {
        Debug.Assert(stateImage != null);
        Debug.Assert(includedSprite != null);
        Debug.Assert(excludedSprite != null);
        Debug.Assert(attachedButton != null);
        Debug.Assert(nameText != null);
    }

    public void SetIncluded(bool state) {
        if (isIncluded != state) {
            isIncluded = state;

            var targetSprite = includedSprite;
            if (isIncluded == false) {
                targetSprite = excludedSprite;
            }
            stateImage.sprite = targetSprite;
        }
    }
}
