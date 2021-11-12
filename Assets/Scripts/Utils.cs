using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Utils: MonoBehaviour {
    const string savedFilePath = "/savedEntities.txt";

    public static void RemoveChildren(Transform t) {
        if (t != null) {
            foreach (Transform c in t) {
                Destroy(c.gameObject);
            }
        }
    }

    public static List<CSEntity> GetSavedEntities() {
        var result = new List<CSEntity>();

        var stream = OpenedSavedEntitiesFileStream(true);
        if (stream.Length > 0) {
            var formatter = new BinaryFormatter();
            var list = (CSEntityList)formatter.Deserialize(stream);
            result.AddRange(list.entities);
        }
        stream.Close();

        return result;
    }

    public static void SaveEntities(List<CSEntity> entities) {
        var entityList = new CSEntityList {
            entities = entities.ToArray()
        };

        var stream = OpenedSavedEntitiesFileStream(false);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, entityList);
        stream.Close();
    }

    static FileStream OpenedSavedEntitiesFileStream(bool shouldRead) {
        FileStream result = null;

        var fullPath = Application.persistentDataPath + savedFilePath;
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
}
