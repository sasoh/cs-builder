using System;
using UnityEngine;

public class EditMenuEntitiesListController: MonoBehaviour {
    public GameObject savedEntityPrefab;
    public GameObject grid;
    public Action didPressNewButton;
    public Action<int> didPressSavedButton;

    void Start() {
        Debug.Assert(savedEntityPrefab != null);
        Debug.Assert(grid != null);

        Reload();
    }

    public void Reload() {
        Utils.RemoveChildren(grid.transform);
        var newEntityInstance = Instantiate(savedEntityPrefab, grid.transform);
        if (newEntityInstance.TryGetComponent(out EditMenuEntityButtonController newEntityButton) == true) {
            newEntityButton.text.text = "New";
            newEntityButton.didPressButton += didPressNewButton;
        }

        var saved = Utils.GetSavedEntities();
        for (var i = 0; i < saved.Count; i++) {
            var entityButton = Instantiate(savedEntityPrefab, grid.transform);
            if (entityButton.TryGetComponent(out EditMenuEntityButtonController savedEntityButton) == true) {
                savedEntityButton.Configure(saved[i]);
                savedEntityButton.serializationIndex = i;
                savedEntityButton.didPressButton += () => { didPressSavedButton?.Invoke(savedEntityButton.serializationIndex); };
            }
        }
    }
}
