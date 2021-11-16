using System;
using UnityEngine;

public class MapElementEntity: MonoBehaviour {
    public CSEntityConfigurationScriptableObject shapeData;
    public CSMapElement EntityConfiguration { private set; get; }
    public GameObject addPointEffectPrefab;
    public GameObject explosionEffectPrefab;
    public Action didAddPoint;
    public Action didExplode;

    void Awake() {
        Debug.Assert(shapeData != null && shapeData.configuration.Length > 0);
        Debug.Assert(addPointEffectPrefab != null);
        Debug.Assert(explosionEffectPrefab != null);
        EntityConfiguration = new CSMapElement();
    }

    public void Configure(CSMapElement configuration) {
        name = configuration.entity.name;
        EntityConfiguration = configuration;

        if (configuration.entity.shapeIndex < shapeData.configuration.Length) {
            var instance = Instantiate(shapeData.configuration[configuration.entity.shapeIndex].instancePrefab, transform);
            if (instance.TryGetComponent(out MapEntityShape s) == true) {
                s.didClick += DidClickOnShape;
            }
        }
    }

    void DidClickOnShape() {
        var behaviours = EntityConfiguration.entity.behaviours;
        if (behaviours != null) {
            if (behaviours.Contains(CSBehaviour.AddPoint) == true) {
                didAddPoint?.Invoke();

                var point = Instantiate(addPointEffectPrefab, null);
                point.transform.position = transform.position;
            }

            if (behaviours.Contains(CSBehaviour.Explode) == true) {
                didExplode?.Invoke();

                var explosion = Instantiate(explosionEffectPrefab, null);
                explosion.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}
