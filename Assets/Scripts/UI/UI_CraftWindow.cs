using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;


    [SerializeField] private Image[] materialImage;



    public void SetupCraftWindow(ItemData_Equipment _data)
    {
        craftButton.onClick.RemoveAllListeners();


        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear; 
        }

        for (int i = 0; i < _data.craftingMaterias.Count; i++)
        {
            if (_data.craftingMaterias.Count > materialImage.Length)
                Debug.Log("you have more materials......");

            materialImage[i].sprite = _data.craftingMaterias[i].data.icon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

            materialSlotText.text = _data.craftingMaterias[i].stackSize.ToString(); 
            materialSlotText.color = Color.white; 
        }

        itemIcon.sprite = _data.icon;
        itemName.text = _data.itemName;
        itemDescription.text = _data.GetDescription();

        craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(_data,_data.craftingMaterias));
    }
}
