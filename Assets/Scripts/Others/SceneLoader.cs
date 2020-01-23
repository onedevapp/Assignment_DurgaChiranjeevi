using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Load scene
    /// </summary>
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }
    #endregion
}
