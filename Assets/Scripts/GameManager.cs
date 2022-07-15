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
    #endregion

    #region Score Info
    bool canIncreaseScore;
    public float score;
    public float finalScore;
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

        finalScore = Mathf.RoundToInt(score * coins); // calculation of final score

        UI.EndGameCalculations(); // final score passing to UI

        //Time.timeScale = 0;  // stops the time | However it wasn't allowing my mainMenuButton animation to play
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

}
