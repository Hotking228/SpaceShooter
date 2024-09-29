using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Kills;
    [SerializeField] private TextMeshProUGUI m_Score;
    [SerializeField] private TextMeshProUGUI m_Time;
    [SerializeField] private TextMeshProUGUI m_Result;
    [SerializeField] private TextMeshProUGUI m_ButtonNextText;

    private bool m_LevelPassed = false;

    private void Start()
    {

        LevelController.Instance.levelLost += OnLevelLost;
        LevelController.Instance.levelPassed += OnLevelPassed;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        LevelController.Instance.levelLost -= OnLevelLost;
        LevelController.Instance.levelPassed -= OnLevelPassed;
    }

    private void OnLevelLost()
    {
        gameObject.SetActive(true);

        FillLevelStatistics();

        m_Result.text = "Failed";

        m_ButtonNextText.text = "Restart";

        m_LevelPassed = false;
    }
    private void OnLevelPassed()
    {
        gameObject.SetActive(true);

        FillLevelStatistics();

        m_Result.text = "Passed";

        if (LevelController.Instance.HasNextLevel)
        {
            m_ButtonNextText.text = "Next";
        }
        else
        {
            m_ButtonNextText.text = "MainMenu";
        }

        m_LevelPassed = true;
    }

    private void FillLevelStatistics()
    {
        m_Kills.text = "Kills : " + Player.Instance.NumKills.ToString(); 
        m_Score.text = "Score : " + Player.Instance.Score.ToString();
        m_Time.text = "Time : " + ((int)LevelController.Instance.LevelTime).ToString();
    }

    public void OnButtonNextAction()
    {
        gameObject.SetActive(false);
        if (m_LevelPassed)
        {
            LevelController.Instance.LoadNextLevel();
        }
        else
        {
            LevelController.Instance.RestartLevel();
        }
    }
}
