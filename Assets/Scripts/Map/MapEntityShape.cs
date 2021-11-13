using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEntityShape: MonoBehaviour {
    public Action didClick;

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject() == false) {
            didClick?.Invoke();
        }
    }
}
