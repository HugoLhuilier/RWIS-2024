using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
