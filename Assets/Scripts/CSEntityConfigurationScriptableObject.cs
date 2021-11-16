using System;
using UnityEngine;

[Serializable]
public struct CSEntityData {
    public Sprite buttonSprite;
    public GameObject instancePrefab;
}

[CreateAssetMenu(fileName = "CSEntityConfiguration", menuName = "ScriptableObjects/CSEntityConfigurationScriptableObject", order = 1)]
public class CSEntityConfigurationScriptableObject: ScriptableObject {
    public CSEntityData[] configuration;
    public Sprite newEntityImage;
}
