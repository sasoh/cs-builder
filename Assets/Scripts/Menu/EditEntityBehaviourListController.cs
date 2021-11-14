using System;
using System.Collections.Generic;
using UnityEngine;

public class EditEntityBehaviourListController: MonoBehaviour {
    public BehaviourButtonController[] behaviourButtons;
    List<CSBehaviour> includedBehaviours = new List<CSBehaviour>();
    public Action<List<CSBehaviour>> didChooseBehaviours;

    void Start() {
        foreach (var b in behaviourButtons) {
            b.SetIncluded(false);
        }
    }

    public void DidTapBehaviourButton(int behaviour) {
        var included = includedBehaviours.Contains((CSBehaviour)behaviour);

        if (included == false) {
            includedBehaviours.Add((CSBehaviour)behaviour);
        }
        else {
            includedBehaviours.Remove((CSBehaviour)behaviour);
        }

        included = !included;
        behaviourButtons[(int)behaviour].SetIncluded(included);
        didChooseBehaviours?.Invoke(includedBehaviours);
    }
}
