using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    public enum MenuState
    {
        Ready,
        InGame
    }
    public MenuState Menu;

    [Header("READY")]
    [SerializeField] private GameObject readyCanvas;
    [SerializeField] private Button playBtn;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("IN GAME")]
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    [Header("SET UP GAME")]
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 locationFirstTime;
    [SerializeField] private EnemyManager enemyManager;

    [Header("Music")]
    [SerializeField] private AudioSource musicTheme;

    private int score;

    public static Action OnPlusScore;
    public static Action OnGameOver;

    private void Awake()
    {
        playBtn.onClick.AddListener(() => { SetUpStartBtn(); });
        OnPlusScore = () => { UpdateScore(score + 1); };
        OnGameOver = () => { SwitchState(MenuState.Ready); };
    }

    private void Start()
    {
        SwitchState(MenuState.Ready);
    }

    void SetUpStartBtn()
    {
        SwitchState(MenuState.InGame);
        Restart();
    }

    void SwitchState(MenuState state)
    {
        readyCanvas.SetActive(false);
        inGameCanvas.SetActive(false);

        // Run Last Time
        switch (Menu)
        {
            case MenuState.Ready:
                break;
            case MenuState.InGame:
                break;
        }

        Menu = state;

        // Run First Time
        switch (Menu)
        {
            case MenuState.Ready:
                readyCanvas.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                musicTheme.Stop();
                break;
            case MenuState.InGame:
                inGameCanvas.SetActive(true);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                musicTheme.Play();
                break;
        }
    }

    void UpdateScore(int score)
    {
        this.score = score;
        currentScoreText.text = score.ToString();
        finalScoreText.text = score.ToString();
    }

    void Restart()
    {
        UpdateScore(0);

        player.transform.position = locationFirstTime;
        enemyManager.Restart();
    }
}
