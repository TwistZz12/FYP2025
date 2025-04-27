using System.Collections;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField] private Transform targetDoor;     
    [SerializeField] private float fadeTime = 1.0f;   
    
    private bool isTeleporting = false;                
    private static bool isTeleportingGlobal = false; // 全局防止多门互相触发

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleportingGlobal && !isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    IEnumerator TeleportPlayer(Transform player)
    {
        isTeleporting = true;
        isTeleportingGlobal = true;

        UI.instance.GetFadeScreen().FadeOut();

        yield return new WaitForSeconds(fadeTime);

        player.position = targetDoor.position;

        UI.instance.GetFadeScreen().FadeIn();
        yield return new WaitForSeconds(fadeTime);

        isTeleporting = false;
        isTeleportingGlobal = false;
    }
}
