using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    public static LevelController levelController { get; private set; }
    public PlayerController player;
    public Canvas floatingCanvas;
    [Header("UI")]
    [SerializeField]
    Text messageText;
    [SerializeField]
    [Multiline]
    string startMessage;
    static bool startMessageReaded = false;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject finishPanel;

    private void Awake()
    {
        levelController = this;
    }

    private void Start()
    {
        if(!startMessageReaded && startMessage != "")
        {
            messageText.text = startMessage;
            messageText.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            player.SetRun(true);
        }
    }

    public void CloseMessage()
    {
        startMessageReaded = true;
        messageText.transform.parent.gameObject.SetActive(false);
        player.SetRun(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Finish()
    {
        finishPanel.SetActive(true);
        player.Finish();
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(0);
    }
}
