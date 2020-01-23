using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{

    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// The high score gameobject.
    /// </summary>
    [SerializeField]
    private Text highScoreText;

    #endregion


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0);
    }
    #endregion
}
