using UnityEngine;

public class PlayMenuController: MonoBehaviour {
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text explodedText;
    int score;
    int exploded;

    void Awake() {
        Debug.Assert(scoreText != null);
        Debug.Assert(explodedText != null);
    }

    void Start() {
        if (MapController.instance != null) {
            MapController.instance.AttachScoreCounter(() => {
                score++;
                scoreText.text = $"Points: {score}";
            });
            MapController.instance.AttachExplodeCounter(() => {
                exploded++;
                explodedText.text = $"Exploded: {exploded}";
            });
        }
    }
}
