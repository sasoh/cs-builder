using UnityEngine;

public class Utils: MonoBehaviour {
    public static void RemoveChildren(Transform t) {
        if (t != null) {
            foreach (Transform c in t) {
                Destroy(c.gameObject);
            }
        }
    }
}
