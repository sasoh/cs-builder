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

        var behaviours = entityConfiguration.behaviours;
        if (behaviours != null) {
            if (behaviours.Contains(CSBehaviour.AddPoint) == true) {
                Debug.Log($"Add point from {gameObject}");
            }

            if (behaviours.Contains(CSBehaviour.Explode) == true) {
                Debug.Log($"Destroying {gameObject}");
                Destroy(gameObject);
            }
        }
    }
}
