using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI timerText;

    private Timer timer;

    public UnityEvent pauseTime;
    public UnityEvent resumeTime;

    public bool timePaused {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();

        Time.timeScale = 1.0f;

        StartGame();
    }

    private void Update()
    {
        timerText.text = timer.getTime().ToString();
    }

    public void Win()
    {
        timer.stopTimer();
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        timer.stopTimer();
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        timer.startTimer();
    }

    public void PauseTime()
    {
        timePaused = true;
        pauseTime.Invoke();
    }

    public void ResumeTime()
    {
        timePaused = false;
        resumeTime.Invoke();
    }
}
