using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController: MonoBehaviour {
    public static MapController instance;
    public bool IsEditMode { private set; get; }
    public GameObject tileElementBase;
    public GameObject tileElementPrefab;
    public GameObject mapElementBase;
    public GameObject mapElementPrefab;
    public float mapElementSpacing;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public List<MapElementEntity> entities = new List<MapElementEntity>();
    public List<MapTileController> tiles = new List<MapTileController>();
    public MapTileController lastClickedTile;
    IMapEditor mapEditor;
    CSMap currentMap = new CSMap();
    public CSEntity currentlyPlacedEntity;

    void Awake() {
        Debug.Assert(tileElementBase != null);
        Debug.Assert(tileElementPrefab != null);
        Debug.Assert(mapElementBase != null);
        Debug.Assert(mapElementPrefab != null);
        Debug.Assert(mapElementSpacing > 0);
        Debug.Assert(mapWidth > 0);
        Debug.Assert(mapHeight > 0);

        if (instance == null) {
            instance = this;

            currentMap = Utils.GetSavedMap();
            RebuildMap();
        }
        else {
            Destroy(gameObject);
        }
    }

    public void RebuildMap() {
        tiles.Clear();
        ConstructTiles(tileElementPrefab, tileElementBase, mapElementSpacing, mapWidth, mapHeight, tiles);
        entities.Clear();
        ConstructMap(currentMap, mapElementPrefab, mapElementBase, mapElementSpacing, mapWidth, mapHeight, entities);
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

    public void SetEditMode(bool status, IMapEditor editor) {
        IsEditMode = status;
        mapEditor = editor;
        if (mapEditor != null) {
            mapEditor.SetShowPlacementInfoPanel(IsEditMode == true);
        }

        if (status == false) {
            currentMap = Utils.GetSavedMap();
            RebuildMap();
        }

        foreach (var e in entities) {
            e.gameObject.SetActive(IsEditMode == false);
            tiles[e.EntityConfiguration.x * mapWidth + e.EntityConfiguration.y].SetTakenHighlight(IsEditMode == true);
        }
    }

    public void ClearMap() {
        currentMap = new CSMap();
        RebuildMap();
    }

    public void SaveMap() {
        Utils.SaveMap(currentMap);
    }

    void DidClickOnTile(MapTileController tile) {
        if (lastClickedTile != null) {
            lastClickedTile.SetConfirmPlacement(false);
        }

        if (tile == lastClickedTile) {
            mapEditor.SetShowPlacementInfoPanel(true);
            mapEditor.SetShowConfirmationInfoPanel(false);

            currentMap.elements.Add(new CSMapElement {
                entity = currentlyPlacedEntity,
                x = tile.X,
                y = tile.Y
            });
            tiles[tile.X * mapWidth + tile.Y].SetTakenHighlight(true);
        }
        else {
            lastClickedTile = tile;
            lastClickedTile.SetConfirmPlacement(true);
            mapEditor.SetShowPlacementInfoPanel(false);
            mapEditor.SetShowConfirmationInfoPanel(true);
        }
    }

    static void ConstructTiles(GameObject tilePrefab, GameObject tileBase, float elementSpacing, int width, int height, List<MapTileController> collection) {
        Utils.RemoveChildren(tileBase.transform);
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var instance = Instantiate(tilePrefab, tileBase.transform);
                if (instance.TryGetComponent(out MapTileController tileController) == true) {
                    collection.Add(tileController);
                    tileController.Configure(MapController.instance.DidClickOnTile, i, j);
                }
                instance.name += $" [{i}: {j}]";

                var position = new Vector3((i - (height - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (j - (width - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }

    static void ConstructMap(CSMap mapData, GameObject elementPrefab, GameObject elementBase, float elementSpacing, int width, int height, List<MapElementEntity> collection) {
        Utils.RemoveChildren(elementBase.transform);
        if (mapData.elements != null) {
            foreach (var e in mapData.elements) {
                var instance = Instantiate(elementPrefab, elementBase.transform);
                if (instance.TryGetComponent(out MapElementEntity mapEntity) == true) {
                    mapEntity.Configure(e);
                    collection.Add(mapEntity);
                }

                var position = new Vector3((e.x - (height - 1) / 2) * elementSpacing - elementSpacing / 2, 0.0f, (e.y - (width - 1) / 2) * elementSpacing - elementSpacing / 2);
                instance.transform.localPosition = position;
            }
        }
    }
}
