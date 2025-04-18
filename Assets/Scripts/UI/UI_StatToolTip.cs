using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class UI_StatToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    
    public void ShowStatToolTip( string _text)
    {
        description.text = _text;

        gameObject.SetActive(true);
    }


    public void HideStatToolTip()
    {
        description.text = "";
        gameObject.SetActive(false);
    }
}
