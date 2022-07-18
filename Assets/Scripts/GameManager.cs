using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    Player player;
    [SerializeField] UI UI;
    [SerializeField] string mainScene;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] GameObject _levelAreaGameObject;
    public static GameManager instance;
    #endregion

    #region Coin Info
    [Header("Coins")]
    public int coins;
    //public int ownedCoins;
    #endregion

    #region Score Info
    bool canIncreaseScore;
    public float score;
    public int lastScore;
    public int highSchore;
    #endregion

    void Awake()
    {
        instance = this;

        Time.timeScale = 1;
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UI = GameObject.Find("Canvas").GetComponent<UI>();

        LoadValues(); //loads values of player prefs
    }

    void Update()
    {
        CheckForScore();
    }

    public void GameStart()
    {
        player.canRun = true;
    }

    public void GameRestart()
    {
        EditorSceneManager.LoadScene(mainScene);
    }

    public void GameEnds()
    {
        playerGameObject.SetActive(false); // Setting the "Player" GameObject to unactive as a solution to stopping the game while allowing the mainMenuButton animation
        _levelAreaGameObject.SetActive(false); // this deactivates the Level Area GameObject, a solution to optimize ram while on the end screen

        lastScore = Mathf.RoundToInt(score * coins); // calculation of final score

        UI.EndGameCalculations(); // final score passing to UI

        //Time.timeScale = 0;  // stops the time | However it wasn't allowing my mainMenuButton animation to play

        SaveValues();
    }

    void CheckForScore()
    {
        if (canIncreaseScore)
        {
            score = player.rb.transform.position.x;
        }

        if (player.rb.velocity.x > 0)
        {
            canIncreaseScore = true;
        }
        else
        {
            canIncreaseScore = false;
        }
    }

    void SaveValues()
    {
        PlayerPrefs.SetInt("lastScore", lastScore);

        if (lastScore > highSchore)
        {
            PlayerPrefs.SetInt("highScore", lastScore);
        }
    }

    void LoadValues()
    {
        lastScore = PlayerPrefs.GetInt("lastScore");
        highSchore = PlayerPrefs.GetInt("highScore");
    }

}
