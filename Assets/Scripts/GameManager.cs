using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player player;
    private UI UI;

    #region Coin Info
    [Header("Coins")]
    public int coins;
    #endregion

    #region Score Info
    bool canIncreaseScore;
    public float score;
    public float finalScore;
    #endregion

    [SerializeField] private string mainScene;


    private void Awake()
    {
        instance = this;
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
        finalScore = Mathf.RoundToInt(score * coins); // calculation of final score

        UI.EndGameCalculations(); // final score passing to UI


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
