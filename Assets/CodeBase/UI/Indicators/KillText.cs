using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    private int lastNumKills;

    private void Update()
    {
        int numKills = Player.Instance.NumKills; 

        if (numKills != lastNumKills)
        {
            m_Text.text = "Kills : " + numKills;
            lastNumKills = numKills;
        }
    }
}
