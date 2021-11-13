using UnityEngine;

public class MapController: MonoBehaviour {
    public GameObject tileElementBase;
    public GameObject tileElementPrefab;
    public GameObject mapElementBase;
    public GameObject mapElementPrefab;
    public float mapElementSpacing;
    public int mapWidth = 10;
    public int mapHeight = 10;

    void Awake() {
        Debug.Assert(mapElementBase != null);
        Debug.Assert(mapElementPrefab != null);
        Debug.Assert(mapElementSpacing > 0);
        Debug.Assert(mapWidth > 0);
        Debug.Assert(mapHeight > 0);
    }

    void Start() {
        var data = Utils.GetMockMap();
        ConstructTiles(tileElementPrefab, tileElementBase, mapElementSpacing, mapWidth, mapHeight);
        ConstructMap(data, mapElementPrefab, mapElementBase, mapElementSpacing, mapWidth, mapHeight);
    }

    static void ConstructTiles(GameObject tilePrefab, GameObject tileBase, float elementSpacing, int width, int height) {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var instance = Instantiate(tilePrefab, tileBase.transform);

                var position = new Vector3((j - (width - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (i - (height - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }

    static void ConstructMap(CSMap mapData, GameObject elementPrefab, GameObject elementBase, float elementSpacing, int width, int height) {
        if (mapData.elements != null) {
            foreach (var e in mapData.elements) {
                var instance = Instantiate(elementPrefab, elementBase.transform);
                if (instance.TryGetComponent(out MapElementEntity mapEntity) == true) {
                    mapEntity.Configure(e);
                }

                var position = new Vector3((e.x - (width - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (e.y - (height - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }
}
