using System;
using System.Collections.Generic;
using UnityEngine;

public class EditEntityBehaviourListController: MonoBehaviour {
    public BehaviourButtonController[] behaviourButtons;
    List<int> includedBehaviours = new List<int>();
    public Action<List<int>> didChooseBehaviours;

    void Start() {
        foreach (var b in behaviourButtons) {
            b.SetIncluded(false);
        }
    }

    public void DidTapBehaviourButton(int index) {
        var included = includedBehaviours.Contains(index);

        if (included == false) {
            includedBehaviours.Add(index);
        }
        else {
            includedBehaviours.Remove(index);
        }

        included = !included;
        behaviourButtons[index].SetIncluded(included);
        didChooseBehaviours?.Invoke(includedBehaviours);
    }
}
