using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour
{
    private const string MAIN_MENU = "MainMenu";

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI curRoomText;
    private RoomManager firstRoom;

    [SerializeField] private int numberOfRooms = 10;
    private int curRoom = 1;

    [SerializeField] private float difficultyPower = 2;

    [SerializeField] RoomManager[] allRooms;

    private Timer timer;
    private UIController ui;
    private PlayerController player;

    public GameObject torchPrefab;
    public GameObject lampPrefab;
    public GameObject skeletonPrefab;
    public GameObject switcherPrefab;
    public GameObject finishPrefab;

    public UnityEvent pauseTime;
    public UnityEvent resumeTime;

    private List<RoomManager> usedRooms;

    public bool timePaused {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();

        player = FindAnyObjectByType<PlayerController>();
        ui = FindAnyObjectByType<UIController>();

        SetupScene();
        StartGame();
    }

    private void Update()
    {
        timerText.text = timer.getTime().ToString("#.00") + "s";
    }

    public void Win()
    {
        timer.stopTimer();
        Time.timeScale = 0;

        finalTimeText.text = timer.getTime().ToString("#.00") + "s";

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
        foreach (RoomManager room in usedRooms)
        {
            if (room == firstRoom)
                firstRoom.activateRoom();
            else
                room.deactivateRoom();
        }

        player.transform.position = firstRoom.getSpawnPoint().position;

        Debug.Log("Rooms: ");
        foreach(RoomManager room in usedRooms)
            Debug.Log(room.name);

        curRoomText.text = "Current room: 1/" + numberOfRooms.ToString();
        ui.displayInGameUI();

        Time.timeScale = 1.0f;
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

    public void SetupScene()
    {
        List<int> indexes = new List<int>();
        usedRooms = new List<RoomManager>();

        // Creates a list of indexes from 0 to allRooms.Length - 1
        for (int i = 0; i < allRooms.Length; i++)
        {
            indexes.Add(i);
        }

        int curIndex;

        // Set usedRooms to a list of numberOfRooms random rooms from all the rooms, and initializes them
        for (int i = 0; i < numberOfRooms; i++)
        {
            curIndex = indexes[Random.Range(0, allRooms.Length - i)];
            indexes.Remove(curIndex);

            usedRooms.Add(allRooms[curIndex]);

            allRooms[curIndex].GenerateRoom(DifficultyFunction(i));

            if (i > 0)
            {
                GameObject newSwitcher = Instantiate(switcherPrefab, usedRooms[i - 1].transform);

                Switcher sw1 = newSwitcher.GetComponentInChildren<Switcher>();
                Switcher sw2 = sw1.getOtherSwitcher();

                sw1.transform.position = usedRooms[i - 1].getExitPoint().position;
                sw1.setTpTo(usedRooms[i].getSpawnPoint());
                sw1.setCurrentRoom(usedRooms[i - 1]);

                sw2.transform.position = new Vector3(9999, 9999, 9999);
                sw2.setTpTo(null);
                sw2.setCurrentRoom(usedRooms[i]);
            }
        }

        firstRoom = usedRooms[0];

        // Spawns finish point
        Instantiate(finishPrefab, usedRooms[numberOfRooms - 1].getExitPoint().transform);
    }

    public void NextRoom()
    {
        curRoom++;
        curRoomText.text = "Current room: " + curRoom.ToString() + "/" + numberOfRooms.ToString();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    private float DifficultyFunction(int roomNb)
    {
        return Mathf.Pow(((float)roomNb + 1) / ((float)numberOfRooms + 1), difficultyPower);
    }
}
