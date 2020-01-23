using OneDevApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// The state game.
    /// </summary>
    [SerializeField]
    protected Enums.StateGame stateGame = Enums.StateGame.None;

    /// <summary>
    /// The game over gameobject.
    /// </summary>
    [SerializeField]
    private GameObject gameOverPopup;

    /// <summary>
    /// The game over gameobject.
    /// </summary>
    [SerializeField]
    private Text gameOverScoreText;
    #endregion


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Start this instance.
    /// </summary>
    protected void Start()
    {
        InitStart();
    }


    /// <summary>
    /// Updating Game State.
    /// </summary>
    public void UpdateState(Enums.StateGame state)
    {
        stateGame = state;
    }

    /// <summary>
    /// Inits the start.
    /// </summary>
    public void InitStart()
    {
        gameOverPopup.SetActive(false);

        stateGame = Enums.StateGame.Start;

        StartCoroutine(PrepareInitGame());
    }



    /// <summary>
    /// Prepares the init game.
    /// </summary>
    /// <returns>The init game.</returns>
    IEnumerator PrepareInitGame()
    {
        // Create environment.
        MapManager.Instance.createEnvironmentalGrid();

        yield return new WaitForSeconds(0.3f);

        PoolObjects _pooledObject = PoolManager.Instance.GetObjectFromPool("Player");
        //Enabling item from the pool
        PoolManager.Instance.EnableObjectFromPool(_pooledObject, MapManager.Instance.GetRandomTileObject(), Quaternion.identity);

        yield return new WaitForSeconds(0.3f);

        SpawnManager.Instance.SpawnCollectable();

        // Update the state of game.

        UpdateState(Enums.StateGame.Playing);

        yield return null;
    }

    /// <summary>
    /// Enables game over popup.
    /// </summary>
    public void EnableGameOverPopup()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < ScoreManager.Instance.Score)
        {
            PlayerPrefs.SetInt("HighScore", ScoreManager.Instance.Score);
        }

        gameOverPopup.SetActive(true);
        gameOverScoreText.text = "Score : " + ScoreManager.Instance.Score.ToString();
    }

    #endregion


    // ======================= Helper ======================== //

    #region Helper

    /// <summary>
    /// Gets the state game.
    /// </summary>
    /// <returns>The state game.</returns>
    public Enums.StateGame GetStateGame()
    {
        return stateGame;
    }

    /// <summary>
    /// Determines whether this instance is game ready.
    /// </summary>
    /// <returns><c>true</c> if this instance is game ready; otherwise, <c>false</c>.</returns>
    public bool IsGameReady()
    {
        return stateGame == Enums.StateGame.Playing;
    }

    /// <summary>
    /// Determines whether this instance is game end.
    /// </summary>
    /// <returns><c>true</c> if this instance is game end; otherwise, <c>false</c>.</returns>
    public bool IsGameEnd()
    {
        return stateGame == Enums.StateGame.GameOver;
    }

    #endregion
}
