using UnityEngine;

public class MapElementEntity: MonoBehaviour {
    public GameObject[] shapePrefabs;
    CSEntity entityConfiguration = new CSEntity();

    public void Configure(CSMapElement configuration) {
        name = configuration.entity.name;
        entityConfiguration = configuration.entity;

        if (configuration.entity.shapeIndex < shapePrefabs.Length) {
            var instance = Instantiate(shapePrefabs[configuration.entity.shapeIndex], transform);
            if (instance.TryGetComponent(out MapEntityShape s) == true) {
                s.didClick += DidClickOnShape;
            }
        }
    }

    void DidClickOnShape() {
        var actionsList = "";
        foreach (var a in entityConfiguration.behaviours) {
            actionsList += $"{a} ";
        }
        Debug.Log($"clicked on {gameObject} with actions [{actionsList}]");
    }
}
