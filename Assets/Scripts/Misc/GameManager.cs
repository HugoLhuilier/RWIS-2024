using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RoomManager firstRoom;

    [SerializeField] private AudioClip backgroundMusic;

    private Timer timer;
    private UIController ui;

    public UnityEvent pauseTime;
    public UnityEvent resumeTime;

    public bool timePaused {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        ui = FindAnyObjectByType<UIController>();

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

        ui.showWinScreen();
    }

    public void Lose()
    {
        timer.stopTimer();
        Time.timeScale = 0;

        ui.showLoseScreen();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;

        RoomManager[] rooms = FindObjectsOfType<RoomManager>();

        foreach (RoomManager room in rooms)
        {
            if (room == firstRoom)
                firstRoom.activateRoom();
            else
                room.deactivateRoom();
        }

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
