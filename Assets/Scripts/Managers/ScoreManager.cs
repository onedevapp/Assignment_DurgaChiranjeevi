using OneDevApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Score text
    /// </summary>
    public Text scoreText;
    /// <summary>
    /// Streak text
    /// </summary>
    public Text streakText;
    /// <summary>
    /// Score value
    /// </summary>
    private int score = 0;
    /// <summary>
    /// Streak value
    /// </summary>
    private int currentStreak = 0;

    /// <summary>
    /// Last collectable item details
    /// </summary>
    private Collectable lastCollectableItem;


    /// <summary>
    /// Score value
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            this.score = value;
            scoreText.text = "Score : "+this.score.ToString();
        }
    }

    /// <summary>
    /// Streak value
    /// </summary>
    public int CurrentStreak
    {
        get
        {
            return currentStreak;
        }

        set
        {
            this.currentStreak = value;
            streakText.text = "Streak : " + this.currentStreak.ToString();
        }
    }

    #endregion


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        this.Score = 0;
        this.CurrentStreak = 0;
    }


    /// <summary>
    /// Update values for both score and streak
    /// </summary>
    public void UpdateValues(Collectable hitItem)
    {
        if (hitItem.Equals(lastCollectableItem))
        {
            this.CurrentStreak++;
        }
        else
        {
            lastCollectableItem = hitItem;
            this.CurrentStreak = 1;
        }

        this.Score += (hitItem.point * currentStreak);
    }
    #endregion
}
