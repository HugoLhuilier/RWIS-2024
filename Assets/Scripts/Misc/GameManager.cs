using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public UnityEvent pauseTime;
    public UnityEvent resumeTime;

    public bool timePaused {  get; private set; }

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
        Debug.Log("Restart");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseTime()
    {
        Debug.Log("Time paused");

        timePaused = true;
        pauseTime.Invoke();
    }

    public void ResumeTime()
    {
        Debug.Log("Time paused");

        timePaused = false;
        resumeTime.Invoke();
    }
}
