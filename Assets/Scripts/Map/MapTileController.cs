using System;
using UnityEngine;

public class MapTileController: MonoBehaviour {
    public GameObject takenHighlightObject;
    public GameObject confirmPlacementObject;
    public Action<MapTileController> didClickOnTile;
    public int X { private set; get; }
    public int Y { private set; get; }

    void Awake() {
        Debug.Assert(takenHighlightObject != null);
        Debug.Assert(confirmPlacementObject != null);
        SetTakenHighlight(false);
        SetConfirmPlacement(false);
    }

    public void Configure(Action<MapTileController> didClickAction, int x, int y) {
        didClickOnTile = didClickAction;
        X = x;
        Y = y;
    }

    public void SetTakenHighlight(bool status) {
        takenHighlightObject.SetActive(status);
    }

    public void SetConfirmPlacement(bool status) {
        confirmPlacementObject.SetActive(status);
    }

    void OnMouseDown() {
        if (MapController.instance != null && MapController.instance.IsEditMode == true && takenHighlightObject.activeInHierarchy == false && MenuController.instance?.ShownPopup == null) {
            didClickOnTile?.Invoke(this);
        }
    }
}
