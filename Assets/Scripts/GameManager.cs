using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Coin Info
    public int coins;
    #endregion
    [SerializeField] private string mainScene;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {

    }

    public void GameRestart()
    {
        EditorSceneManager.LoadScene(mainScene);
    }
}
