using System;
using System.Collections.Generic;

[Serializable]
public struct CSMapElement {
    public CSEntity entity;
    public int x;
    public int y;
}

[Serializable]
public struct CSMap {
    public List<CSMapElement> elements;
}