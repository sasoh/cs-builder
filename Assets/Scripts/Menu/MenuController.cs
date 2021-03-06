using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadableMenuController: MonoBehaviour {
    public virtual void ShouldReload() {
    }
}

public class MenuController: MonoBehaviour {
    public static MenuController instance;
    public GameObject mapMaskPrefab;
    public GameObject mainCanvas;
    public GameObject menuMainPrefab;
    public GameObject popupPrefab;
    public GameObject ShownPopup { private set; get; }
    List<GameObject> menuStack = new List<GameObject>();
    GameObject mapMask;

    void Awake() {
        Debug.Assert(mainCanvas != null);
        Debug.Assert(menuMainPrefab != null);
        Debug.Assert(mapMaskPrefab != null);
        Debug.Assert(popupPrefab != null);

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        Utils.RemoveChildren(mainCanvas.transform);
        mapMask = Instantiate(mapMaskPrefab, mainCanvas.transform);
        var menuInstance = Instantiate(menuMainPrefab, mainCanvas.transform);
        menuStack.Add(menuInstance);
    }

    public static void ShowPopup(PopupConfiguration configuration) {
        if (instance != null) {
            ClosePopup();
            instance.ShownPopup = Instantiate(instance.popupPrefab, instance.mainCanvas.transform);
            if (instance.ShownPopup.TryGetComponent(out PopupController p) == true) {
                p.Configure(configuration);
            }
        }
    }

    public static void ClosePopup() {
        if (instance != null) {
            if (instance.ShownPopup != null) {
                Destroy(instance.ShownPopup);
            }
        }
    }

    public static void OpenMenu(GameObject menuPrefab, bool shouldHideMask = false) {
        if (instance != null) {
            if (menuPrefab != null) {
                foreach (var m in instance.menuStack) {
                    if (m != instance.mapMask) {
                        m.SetActive(false);
                    }
                }

                var menuInstance = Instantiate(menuPrefab, instance.mainCanvas.transform);
                instance.menuStack.Add(menuInstance);
                instance.mapMask.SetActive(shouldHideMask == false);
            }
        }
    }

    public static void DidPressBack() {
        if (instance != null) {
            if (instance.menuStack.Count > 0) {
                var last = instance.menuStack.Last();
                Destroy(last.gameObject);
                instance.menuStack.RemoveAt(instance.menuStack.Count - 1);
                instance.mapMask.SetActive(true);

                if (instance.menuStack.Count > 0) {
                    last = instance.menuStack.Last();
                    last.SetActive(true);

                    if (last.TryGetComponent(out ReloadableMenuController r) == true) {
                        r.ShouldReload();
                    }
                }
            }
        }
    }
}
