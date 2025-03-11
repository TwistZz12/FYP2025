using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{

    private Player player => GetComponentInParent<Player>(); 
   private void AnimationTirgger()
    {
        player.AnimationTrigger();
    }
}
