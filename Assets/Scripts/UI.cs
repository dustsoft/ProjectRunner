using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Menu Items
    [Header("Menu & UI Info")]
    [SerializeField] GameObject muteButtonUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject endGameUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject settingsUI;
    [SerializeField] GameObject[] menuItems;
    #endregion

    #region Coin & Score Info
    [Header("Coin & Score Info")]
    [SerializeField] Text _score;
    [SerializeField] Text coins;
    [SerializeField] Text _endScreenCoins;
    [SerializeField] Text _endScreenScore;
    [SerializeField] Text _endScreenFinalScore;

    [SerializeField] Text _lastScore;
    [SerializeField] Text _highScore;
    #endregion

    public bool isMuted;
    public bool gameIsPaused;

    void Start()
    {
        SwitchUI(mainMenuUI);
    }

    void Update()
    {
        CoinInfo();
        ScoreInfo();
    }

    #region Buttons
    public void TapToStart()
    {
        GameManager.instance.GameStart();

        SwitchUI(inGameUI);

    }

    public void PlayAgainButton()
    {
        GameManager.instance.GameRestart();
    }

    public void MuteButton()
    {
        isMuted = !isMuted; // works like a switcher
        AudioListener.pause = isMuted;

        #region Mute Icon Controller
        Image muteIcon = GameObject.Find("muteIcon").GetComponent<Image>();

        if (isMuted)
        {
            muteIcon.color = new Color(muteIcon.color.r, muteIcon.color.g, muteIcon.color.b, 0.15f);
        }
        else
        {
            muteIcon.color = new Color(muteIcon.color.r, muteIcon.color.g, muteIcon.color.b, 1f);
        }
        #endregion
    }

    public void ShopButton()
    {
        SwitchUI(shopUI);
        muteButtonUI.SetActive(false);
    }

    public void SettingsButton()
    {
        SwitchUI(settingsUI);
        muteButtonUI.SetActive(false);
    }

    public void CloseButton()
    {
        SwitchUI(mainMenuUI);
        muteButtonUI.SetActive(true);
    }

    public void PauseButton()
    {
        gameIsPaused = !gameIsPaused; // works like a switcher
        Image pauseIcon = GameObject.Find("pauseIcon").GetComponent<Image>();

        if (gameIsPaused)
        {
            pauseIcon.color = new Color(pauseIcon.color.r, pauseIcon.color.g, pauseIcon.color.b, 1f);
            Time.timeScale = 0;
        }
        else
        {
            pauseIcon.color = new Color(pauseIcon.color.r, pauseIcon.color.g, pauseIcon.color.b, 0.15f);
            Time.timeScale = 1;
        }
    }
    #endregion

    public void SwitchUI(GameObject uiToActivate)
    {
        for (int i=0; i < menuItems.Length; i++)
        {
            menuItems[i].SetActive(false);
        }

        AudioManager.instance.PlaySFX(8);

        uiToActivate.SetActive(true);

    }

    public void EndGameCalculations()
    {
        SwitchUI(endGameUI);

        _endScreenCoins.text = ": " + GameManager.instance.coins.ToString("#,#");
        _endScreenScore.text = ": " + Mathf.Round(GameManager.instance.score).ToString("#,#") + " meters";
        _endScreenFinalScore.text = ": " + GameManager.instance.lastScore.ToString("#,#");
    }

    void CoinInfo()
    {
        coins.text = GameManager.instance.coins + " ";
    }

    void ScoreInfo()
    {
        _score.text = Mathf.Round(GameManager.instance.score) + " ";

        _lastScore.text = GameManager.instance.lastScore.ToString("#,#");
        _highScore.text = GameManager.instance.highSchore.ToString("#,#"); 
    }
}

