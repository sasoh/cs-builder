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
            MapController.instance.AttachScoreCounter(() => didUpdateScore());
            MapController.instance.AttachExplodeCounter(() => didExplode());
        }
    }

    void didUpdateScore() {
        score++;
        scoreText.text = $"Points: {score}";
    }

    void didExplode() {
        exploded++;
        explodedText.text = $"Exploded: {exploded}";
    }

    public void RestartGame() {
        score = -1;
        didUpdateScore();
        exploded = -1;
        didExplode();
        MapController.instance?.RebuildMap();
    }
}
