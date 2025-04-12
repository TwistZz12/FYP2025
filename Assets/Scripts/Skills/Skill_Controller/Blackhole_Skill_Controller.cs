using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> KeyCodeList;

    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    public bool canCreateHotKeys = true; 
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;

    [SerializeField] private List<Transform> targets = new List<Transform>();
    [SerializeField] private List<GameObject> createdHotKey = new List<GameObject>();

    public bool playerCanExitState {  get; private set; }

    public void SetupBlackhole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackholeDuration)
    {
        maxSize = _maxSize; 
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;

        blackholeTimer = _blackholeDuration;


        if (SkillManager.instance.clone.crystalInsteadOfClone)
            playerCanDisapear = false;

    }

    private void Update()
    {
        blackholeTimer = Mathf.Infinity;

        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if (blackholeTimer < 0)
        {
            if(targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackHoleAbility();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {

            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;


        cloneAttackReleased = true;
        canCreateHotKeys = false;
        DestroyHotKeys();


        if (playerCanDisapear)
        {
            playerCanDisapear = false;
        PlayerManager.instance.player.MakeTransparent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);



            float xoffset;

            if (Random.Range(0, 100) > 50)
                xoffset = 2;
            else
                xoffset = -2;


            if (SkillManager.instance.clone.crystalInsteadOfClone)
            {
                SkillManager.instance.crystal.CreateCrystal();
                SkillManager.instance.crystal.CurrentCrystalChooseRandomTarget();
            }
            else
            {
            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xoffset, 0));

            }
            amountOfAttacks--;


            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackHoleAbility", 1f);
            }
        }
    }

    private void FinishBlackHoleAbility()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
            AddEnemyToList(collision.transform);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }
    
        
    

    private void CreateHotKey(Collider2D collision)
    {
        if (KeyCodeList.Count == 0)
        {
            return;
        }

        if (!canCreateHotKeys)
        {
            return;
        }

      
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        
        createdHotKey.Add(newHotKey);


        KeyCode choosenKey = KeyCodeList[Random.Range(0, KeyCodeList.Count)];

        KeyCodeList.Remove(choosenKey);

        Blackhole_Hotkey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_Hotkey_Controller>();

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }
    public void AddEnemyToList(Transform _myEnemy)
    {
        targets.Add(_myEnemy);
    }

 
    private void DestroyHotKeys()
    {

        if (createdHotKey.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }

    }
}