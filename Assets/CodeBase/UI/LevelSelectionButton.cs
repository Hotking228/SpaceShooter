using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] private LevelProperties m_levelProperties;

    [SerializeField] private TextMeshProUGUI m_LevelTitle;
    [SerializeField] private Image m_PreviewImage;


    private void Start()
    {
        if (m_levelProperties == null) return;
        m_PreviewImage.sprite = m_levelProperties.PreviewImage;
        m_LevelTitle.text = m_levelProperties.Title;

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_levelProperties.SceneName);
    }
}
