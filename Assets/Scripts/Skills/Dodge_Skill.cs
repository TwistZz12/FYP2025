using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] private int evasionAmount;
    public bool dodgeUnlocked;

    [Header("Mirage dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockMirageDodge;
    public bool dodgeMirageUnlocked;

    protected override void Start()
    {
        base.Start();

        if (unlockDodgeButton != null)
            unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        
        if (unlockMirageDodge != null)
            unlockMirageDodge.GetComponent<Button>().onClick.AddListener(UnlockMirageDodge);
    }

    protected override void CheckUnlock()
    {
        UnlockDodge();
        UnlockMirageDodge();
    }

    private void UnlockDodge()
    {
        if (unlockDodgeButton == null)
            return;
        
        if (unlockDodgeButton.unlocked && !dodgeUnlocked)
        {
            if (player != null && player.stats != null && player.stats.evasion != null)
            {
                player.stats.evasion.AddModifier(evasionAmount);
                
                if (Inventory.instance != null)
                    Inventory.instance.UpdateStatsUI();
                    
                dodgeUnlocked = true;
            }
        }
    }
    private void UnlockMirageDodge()
    {
        if (unlockMirageDodge == null)
            return;
        
        if(unlockMirageDodge.unlocked)
            dodgeMirageUnlocked = true;
    }

    public void CreateMirageOnDodge()
    {
        if (dodgeMirageUnlocked && player != null && SkillManager.instance != null && SkillManager.instance.clone != null)
            SkillManager.instance.clone.CreateClone(player.transform, new Vector3(2 * player.facingDir, 0));
    }

}
