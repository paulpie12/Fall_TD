using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject CreditCanvas;

    private void Start()
    {
        MenuCanvas.SetActive(true);
        CreditCanvas.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Towerdefence");
    }

    public void ChangeActive()
    {
        if (MenuCanvas.activeInHierarchy)
        {
            MenuCanvas.SetActive(false);
            CreditCanvas.SetActive(true);
        }
        else
        {
            MenuCanvas.SetActive(true);
            CreditCanvas.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
