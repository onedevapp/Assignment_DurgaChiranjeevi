using OneDevApp;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Configurator for Collectable.
    /// </summary>
    [SerializeField]
    private TextAsset collectableConfig;

    /// <summary>
    /// List of Collectables.
    /// </summary>
    private Collectable[] collectables;
    #endregion


    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    /// Configurator for Collectable.
    /// </summary>
    void Start()
    {
        Collectables collectableInJson = JsonUtility.FromJson<Collectables>(collectableConfig.text);
        collectables = collectableInJson.collectables;
    }


    /// <summary>
    /// Spwans a new collectable object.
    /// </summary>
    public void SpawnCollectable()
    {
        if (GameManager.Instance.IsGameEnd()) return;

        Vector3 spawnPosition = MapManager.Instance.GetRandomTileObject();
        while (CollideWithObstacleOrSnake(spawnPosition))
        {
            spawnPosition = MapManager.Instance.GetRandomTileObject();
        }

        int RandomIndex = (int)System.Math.Min(collectables.Length - 1, Random.Range(0, collectables.Length));
        Collectable collectableItem = collectables[RandomIndex];

        PoolObjects _pooledObject = PoolManager.Instance.GetObjectFromPool("Food");
        //Enabling item from the pool
        GameObject pickUp = PoolManager.Instance.EnableObjectFromPool(_pooledObject, spawnPosition + Vector3.up * .5f, Quaternion.identity);

        pickUp.GetComponent<PickUpObject>().CollectableItem = collectableItem;
    }

    #endregion


    // ======================== Helper ====================== //

    #region Helper 

    /// <summary>
    /// Checks whether new spawn position is available or not.
    /// </summary>
    private bool CollideWithObstacleOrSnake(Vector3 spawnPosition)
    {
        TailingObject[] tailingObjects = FindObjectsOfType<TailingObject>();

        for (int i = 0; i < tailingObjects.Length; i++)
        {
            if (tailingObjects[i].gameObject.transform.position == spawnPosition)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}
