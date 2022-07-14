using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Menu Items
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject endGameUI;
    [SerializeField] GameObject[] menuItems;
    #endregion

    [SerializeField] Text _score;
    [SerializeField] Text coins;
    [SerializeField] Text _endScreenCoins;
    [SerializeField] Text _endScreenScore;
    [SerializeField] Text _endScreenFinalScore;

    void Start()
    {
        SwitchUI(mainMenuUI);
    }

    public void SwitchUI(GameObject uiToActivate)
    {
        for (int i=0; i < menuItems.Length; i++)
        {
            menuItems[i].SetActive(false);
        }

        uiToActivate.SetActive(true);

    }

    void Update()
    {
        CoinInfo();
        ScoreInfo();
    }

    public void TapToStart()
    {
        GameManager.instance.GameStart();

        SwitchUI(inGameUI);

    }

    public void EndGameCalculations()
    {
        SwitchUI(endGameUI);

        _endScreenCoins.text = "Coins: " + GameManager.instance.coins;
        _endScreenScore.text = "Distance: " + GameManager.instance.score + " m";
        _endScreenFinalScore.text = "Final Score: " + GameManager.instance.finalScore;
    }

    public void PlayAgainButton()
    {
        GameManager.instance.GameRestart();
    }

    void CoinInfo()
    {
        coins.text = GameManager.instance.coins + " ";
    }

    void ScoreInfo()
    {
        _score.text = Mathf.Round(GameManager.instance.score) + " ";
    }
}

