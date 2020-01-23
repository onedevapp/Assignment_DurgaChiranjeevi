using OneDevApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailingObject : MonoBehaviour
{
    // ======================== Variables ======================== //

    #region Variables

    [SerializeField] private TailingObject _nextTail;
    private PlayerBehaviour playerBehaviour;

    /// <summary>
    ///     The next tail assigned to this tail. null if none.
    /// </summary>
    public TailingObject NextTail
    {
        get { return _nextTail; }
    }
    #endregion

    // ======================== Functional ====================== //

    #region Functional 

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    /// <summary>
    ///     Recursively adds a tail to this or one of the next tails,
    ///     dependat on which tail needs a next tail.
    /// </summary>
    public TailingObject AddTail()
    {
        if (NextTail)
        {
            return NextTail.AddTail();
        }

        PoolObjects _pooledObject = PoolManager.Instance.GetObjectFromPool("Tail");
        //Enabling item from the pool
        GameObject tail = PoolManager.Instance.EnableObjectFromPool(_pooledObject, transform.position, Quaternion.identity, playerBehaviour.gameObject);

        _nextTail = tail.GetComponent<TailingObject>();
        _nextTail.GetComponent<BoxCollider>().enabled = false;
        return _nextTail;
    }

    /// <summary>
    ///     Moves this tail to the given position. Recursively moves the next
    ///     tails with it.
    /// </summary>
    public void MoveTo(Vector3 position)
    {
        if (NextTail)
        {
            NextTail.MoveTo(transform.position);
        }

        if (!GetComponent<BoxCollider>().enabled)
            GetComponent<BoxCollider>().enabled = true;

        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food" && gameObject.tag == "Snake_Head")
        {
            if (!playerBehaviour)
                playerBehaviour = FindObjectOfType<PlayerBehaviour>();

            playerBehaviour.AddTail();

            ScoreManager.Instance.UpdateValues(other.gameObject.GetComponent<PickUpObject>().CollectableItem);
            other.gameObject.GetComponent<PoolObjects>().DisablePoolObject();
            SpawnManager.Instance.SpawnCollectable();
        }
        else
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Snake_Tail")
        {
            GameManager.Instance.UpdateState(Enums.StateGame.GameOver);
            GameManager.Instance.EnableGameOverPopup();
        }
    }

    #endregion
}
