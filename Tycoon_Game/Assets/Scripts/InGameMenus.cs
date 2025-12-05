using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenus : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject UI;




    private void Start()
    {
        PauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             if (!PauseMenu.activeInHierarchy)
            {
                PauseMenu.SetActive(true);
                UI.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        PauseMenu?.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
    }
}
