using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject safetyMeter;
    [SerializeField] GameObject endText;
    void Start()
    {
        
    }

    void Update()
    {
        if (Time.timeScale < 1)
        {
            restartButton.SetActive(true);
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        restartButton.SetActive(false);
        endText.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void LevelEnd()
    {
        Time.timeScale = 0;
        endText.SetActive(true);
        restartButton.SetActive(true);
    }
}
