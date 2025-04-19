using System.Text;
using UnityEngine;
using UnityEngine.Rendering;


public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Ited Data",menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;

    [Range(0,100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();


    public virtual string GetDescription()
    {
        return "";
    }
}
