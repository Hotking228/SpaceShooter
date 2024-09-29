using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : SingletonBase<LevelController>
{
    public event UnityAction levelPassed;
    public event UnityAction levelLost;

    [SerializeField] private LevelProperties m_levelProperties;

    [SerializeField] private LevelCondition[] m_Conditions;
    private bool m_IsLevelCompleted;

    private float m_LevelTime;
    public float LevelTime => m_LevelTime;

    public bool HasNextLevel => m_levelProperties.NextLevel != null;

    private void Start()
    {
        Time.timeScale = 1.0f;
        m_LevelTime = 0;
    }

    private void Update()
    {
        if (!m_IsLevelCompleted)
        {
            m_LevelTime += Time.deltaTime;
            CheckLevelConditions();
        }

        

        if (Player.Instance.NumLives == 0)
        {
            Lose();
        }
    }

    private void CheckLevelConditions()
    {
        
        int numCompleted = 0;
        for (int i = 0; i < m_Conditions.Length; i++)
        {
            if (m_Conditions[i].isCompleted)
                numCompleted++;
        }

        if (numCompleted == m_Conditions.Length)
        {
            m_IsLevelCompleted = true;

            Pass();
        }

    }

    private void Pass()
    {
        levelPassed.Invoke();
        Time.timeScale = 0f;
    }

    private void Lose()
    {
        levelLost.Invoke();
        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        if (HasNextLevel)
        {
            SceneManager.LoadScene(m_levelProperties.NextLevel.SceneName);
        }
        else
        {
            SceneManager.LoadScene("main_menu");
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(m_levelProperties.SceneName);
    }
}
