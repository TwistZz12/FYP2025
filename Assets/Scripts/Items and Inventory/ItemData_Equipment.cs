using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Ited Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
   public EquipmentType equipmentType;



    public float itemCooldown;
    public ItemEffect[] itemEffects;

    [Header("Major stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;


    [Header("Magicstats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterias;

    private int descriptionLength;

    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }

    public override string GetDescroption()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescrption(strength, "Strength");
        AddItemDescrption(agility, "Agility");
        AddItemDescrption(intelligence, "Intelligence");
        AddItemDescrption(vitality, "Vitality");

        AddItemDescrption(damage, "Damage");
        AddItemDescrption(critChance, "Crit.Chance");
        AddItemDescrption(critPower, "Crit.Power");

        AddItemDescrption(health, "Health");
        AddItemDescrption(evasion, "Evasion");
        AddItemDescrption(armor, "Armor");
        AddItemDescrption(magicResistance, "magic Resist.");

        AddItemDescrption(fireDamage, "FireDamage");
        AddItemDescrption(iceDamage, "IceDamage");
        AddItemDescrption(lightingDamage, "LightingDamage");


        if(descriptionLength < 5)
        {
            for (int i = 0; i < 5 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }


        return sb.ToString();
    }

    private void AddItemDescrption(int _value,string _name)
    {
        if(_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if(_value > 0)
                sb.Append("+ " + _value + " " + _name);

            descriptionLength++;
        }
    } 
}
