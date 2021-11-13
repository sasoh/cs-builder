using System;
using System.Collections.Generic;

[Serializable]
public struct CSEntityList {
    public List<CSEntity> entities;
}

[Serializable]
public enum CSBehaviour {
    Explode,
    AddPoint
}

[Serializable]
public struct CSEntity {
    public int shapeIndex;
    public string name;
    public List<CSBehaviour> behaviours;

    public override string ToString() {
        var result = $"Entity name: {name} shape: {shapeIndex} behaviors: [";

        if (behaviours != null) {
            for (var i = 0; i < behaviours.Count; i++) {
                result += $"{behaviours[i]}";

                if (i < behaviours.Count - 1) {
                    result += " ";
                }
            }
        }
        result += "]";

        return result;
    }
}