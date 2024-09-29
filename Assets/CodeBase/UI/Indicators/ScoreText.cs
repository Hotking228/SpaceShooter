using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    private int lastScore;

    private void Update()
    {
        int score = Player.Instance.Score;

        if (score != lastScore)
        {
            m_Text.text = "Score : " + score;
            lastScore = score;
        }
    }
}
