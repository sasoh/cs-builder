using System;
using UnityEngine;

public class MapElementEntity: MonoBehaviour {
    public GameObject[] shapePrefabs;
    CSEntity entityConfiguration = new CSEntity();
    public Action didAddPoint;
    public Action didExplode;

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
        var behaviours = entityConfiguration.behaviours;
        if (behaviours != null) {
            if (behaviours.Contains(CSBehaviour.AddPoint) == true) {
                didAddPoint?.Invoke();
            }

            if (behaviours.Contains(CSBehaviour.Explode) == true) {
                didExplode?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
