using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private const string MUSIC_VOLUME = "MusicVolume";

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Slider musicSlider;

    [SerializeField] private AudioMixer mixer;

    private float maxMusicVolume = Mathf.Pow(10, 0.75f);

    private GameManager gameManager;

    private GameObject[] allUIs;

    private void Start()
    {
        allUIs = new GameObject[] { winScreen,
                loseScreen,
                inGameUI,
                pauseMenu};

        
        musicSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void showWinScreen()
    {
        disableAllUIs();

        winScreen.SetActive(true);
    }

    public void showLoseScreen()
    {
        disableAllUIs();

        loseScreen.SetActive(true);
    }

    public void disableAllUIs()
    {
        foreach (var uIs in allUIs)
        {
            uIs.SetActive(false);
        }
    }

    public void displayInGameUI()
    {
        inGameUI.SetActive(true);
    }

    public void PauseGame()
    {
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();

        // Cannot pause when time is already paused
        if (gameManager.timePaused)
            return;

        gameManager.PauseTime();

        foreach (var uIs in allUIs)
            uIs.SetActive(false) ;

        pauseMenu.SetActive(true);

        float curVolume;
        mixer.GetFloat(MUSIC_VOLUME, out curVolume);
        musicSlider.SetValueWithoutNotify(Mathf.Pow(10, curVolume / 20) / maxMusicVolume);
    }

    public void ResumeGame()
    {
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();

        foreach (var uIs in allUIs)
            uIs.SetActive(false);

        inGameUI.SetActive(true);

        gameManager.ResumeTime();
    }

    public void ChangeVolume(float newVol)
    {
        mixer.SetFloat(MUSIC_VOLUME, 20 * Mathf.Log10(newVol * maxMusicVolume));
    }
}
