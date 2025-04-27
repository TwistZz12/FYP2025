using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BgmTriggerZone : MonoBehaviour
{
    [SerializeField] private int newBgmIndex;
    [SerializeField] private float fadeOutDuration = 3f;
    [SerializeField] private float fadeInDuration = 2f;
    private bool triggered = false;

    private void Awake()
    {
        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            AudioManager.instance.CrossFadeBGM(newBgmIndex, fadeOutDuration, fadeInDuration);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
