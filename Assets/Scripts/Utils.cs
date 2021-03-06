using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Utils: MonoBehaviour {
    const string savedFilePath = "/savedEntities.txt";
    const string savedMapFilePath = "/savedMap.txt";

    public static void RemoveChildren(Transform t) {
        if (t != null) {
            foreach (Transform c in t) {
                Destroy(c.gameObject);
            }
        }
    }

    static FileStream OpenedFileStream(string filePath, bool shouldRead) {
        FileStream result = null;

        var fullPath = Application.persistentDataPath + filePath;
        if (File.Exists(fullPath) == true) {
            if (shouldRead == true) {
                result = File.OpenRead(fullPath);
            }
            else {
                result = File.OpenWrite(fullPath);
            }
        }
        else {
            result = File.Create(fullPath);
        }

        return result;
    }

    public static CSEntityList GetSavedEntities() {
        var result = new CSEntityList {
            entities = new List<CSEntity>()
        };

        var stream = OpenedFileStream(savedFilePath, true);
        if (stream.Length > 0) {
            var formatter = new BinaryFormatter();
            result = (CSEntityList)formatter.Deserialize(stream);
        }
        stream.Close();

        return result;
    }

    public static void SaveEntities(CSEntityList entityList) {
        var stream = OpenedFileStream(savedFilePath, false);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, entityList);
        stream.Close();
    }

    public static CSMap GetSavedMap() {
        var result = new CSMap {
            elements = new List<CSMapElement>()
        };

        var stream = OpenedFileStream(savedMapFilePath, true);
        if (stream.Length > 0) {
            var formatter = new BinaryFormatter();
            result = (CSMap)formatter.Deserialize(stream);
        }
        stream.Close();

        return result;
    }

    public static void SaveMap(CSMap map) {
        var stream = OpenedFileStream(savedMapFilePath, false);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, map);
        stream.Close();
    }
}
