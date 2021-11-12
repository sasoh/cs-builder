using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuController: MonoBehaviour {
    public static MenuController instance;
    public GameObject mainCanvas;
    public GameObject menuMainPrefab;
    List<GameObject> menuStack = new List<GameObject>();

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        Debug.Assert(mainCanvas != null);
        Debug.Assert(menuMainPrefab != null);

        Utils.RemoveChildren(mainCanvas.transform);
        var menuInstance = Instantiate(menuMainPrefab, mainCanvas.transform);
        menuStack.Add(menuInstance);
    }

    public static void OpenMenu(GameObject menuPrefab) {
        if (instance != null) {
            if (menuPrefab != null) {
                foreach (var m in instance.menuStack) {
                    m.SetActive(false);
                }

                var menuInstance = Instantiate(menuPrefab, instance.mainCanvas.transform);
                instance.menuStack.Add(menuInstance);
            }
        }
    }

    public static void DidPressBack() {
        if (instance != null) {
            if (instance.menuStack.Count > 0) {
                var last = instance.menuStack.Last();
                Destroy(last.gameObject);
                instance.menuStack.RemoveAt(instance.menuStack.Count - 1);

                if (instance.menuStack.Count > 0) {
                    last = instance.menuStack.Last();
                    last.SetActive(true);
                }
            }
        }
    }
}
