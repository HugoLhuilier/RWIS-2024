using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credits;

    private const string MAIN_SCENE = "FinalScene";

    public void ToCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void ToMain()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(MAIN_SCENE);
    }
}
