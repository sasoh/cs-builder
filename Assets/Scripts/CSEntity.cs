using System.Collections.Generic;

public struct CSEntity {
    public int shapeIndex;
    public List<int> behaviours;

    public override string ToString() {
        var result = $"Entity shape: {shapeIndex} behaviors: [";

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