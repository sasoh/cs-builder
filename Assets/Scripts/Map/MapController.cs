using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController: MonoBehaviour {
    public static MapController instance;
    public GameObject tileElementBase;
    public GameObject tileElementPrefab;
    public GameObject mapElementBase;
    public GameObject mapElementPrefab;
    public float mapElementSpacing;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public List<MapElementEntity> entities = new List<MapElementEntity>();

    void Awake() {
        Debug.Assert(mapElementBase != null);
        Debug.Assert(mapElementPrefab != null);
        Debug.Assert(mapElementSpacing > 0);
        Debug.Assert(mapWidth > 0);
        Debug.Assert(mapHeight > 0);

        if (instance == null) {
            instance = this;

            var data = Utils.GetMockMap();
            ConstructTiles(tileElementPrefab, tileElementBase, mapElementSpacing, mapWidth, mapHeight);
            ConstructMap(data, mapElementPrefab, mapElementBase, mapElementSpacing, mapWidth, mapHeight, entities);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AttachScoreCounter(Action counter) {
        foreach (var m in entities) {
            m.didAddPoint += counter;
        }
    }
    public void AttachExplodeCounter(Action counter) {
        foreach (var m in entities) {
            m.didExplode += counter;
        }
    }

    static void ConstructTiles(GameObject tilePrefab, GameObject tileBase, float elementSpacing, int width, int height) {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var instance = Instantiate(tilePrefab, tileBase.transform);

                var position = new Vector3((j - (width - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (i - (height - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }

    static void ConstructMap(CSMap mapData, GameObject elementPrefab, GameObject elementBase, float elementSpacing, int width, int height, List<MapElementEntity> collection) {
        if (mapData.elements != null) {
            foreach (var e in mapData.elements) {
                var instance = Instantiate(elementPrefab, elementBase.transform);
                if (instance.TryGetComponent(out MapElementEntity mapEntity) == true) {
                    mapEntity.Configure(e);
                    collection.Add(mapEntity);
                }

                var position = new Vector3((e.x - (width - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (e.y - (height - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }
}
