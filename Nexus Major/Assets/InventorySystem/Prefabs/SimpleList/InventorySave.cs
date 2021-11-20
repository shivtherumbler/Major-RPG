using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventorySave : MonoBehaviour
{
    public List<InventorySystem.InventoryItem> Inventory;

    void SaveInventory(string directory)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            InventorySystem.InventoryItem item = Inventory[i];

            File.AppendAllText
            (
                // Saves object index
                directory, "Object " + i.ToString() + "\n" +

                // Saves object properties
                item.Name.ToString() + "\n" +
                item.Sprite + "\n"
                
            );
        }
    }

    void LoadInventory(string directory)
    {
        StreamReader reader = new StreamReader(directory);
        string text = reader.ReadToEnd();

        // Remember to close the stream IMPORTANT!
        reader.Close();

        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            // Is a new object
            if (lines[i].Contains("Object"))
            {
                // Get properties
                string name = lines[i + 1];
                int id = int.Parse(lines[i + 2]);
                
                float price = float.Parse(lines[i + 3]);
                Vector3 position = StringToVector3(lines[i + 4]);
                //Sprite sprite = Sprite.lines[i + 5];

                // Create new scriptable object and add to inventory
                InventorySystem.InventoryItem item = ScriptableObject.CreateInstance<InventorySystem.InventoryItem>();

                item.Name = name;
                //item.Sprite = sprite;

                Inventory.Add(item);
            }
        }
    }

    Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
