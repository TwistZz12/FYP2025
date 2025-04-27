using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;
    


    public virtual void GenerateDrop()
    {
        dropList.Clear(); // 确保列表是空的，避免重复添加
        
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if(Random.Range(0,100) <= possibleDrop[i].dropChance)
                    dropList.Add(possibleDrop[i]);
        }
        
        // 如果没有物品符合掉落条件，直接返回
        if (dropList.Count == 0)
            return;
        
        int itemsToDrop = Mathf.Min(possibleItemDrop, dropList.Count);
        
        for (int i = 0; i < itemsToDrop; i++)
        {
            // 确保随机索引正确计算，避免越界
            int randomIndex = Random.Range(0, dropList.Count);
            ItemData randomItem = dropList[randomIndex];
            
            dropList.Remove(randomItem);
            DropItem(randomItem);
            
            // 如果列表已空，退出循环
            if (dropList.Count == 0)
                break;
        }
    }


    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab,transform.position,Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5,5),Random.Range(15,20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
