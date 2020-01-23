using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Collectable current item
    /// </summary>
    private Collectable collectableItem;

    /// <summary>
    /// Set current collectable item with color
    /// </summary>
    public Collectable CollectableItem
    {
        get
        {
            return collectableItem;
        }
        set
        {
            this.collectableItem = value;

            Color newColor = Color.white;
            ColorUtility.TryParseHtmlString(collectableItem.color, out newColor);

            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material.color = newColor;
        }
    }

    #endregion
}
