using OneDevApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{

    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Number of columns for the grid.
    /// </summary>
    [SerializeField]
    private int numberOfColumns = 10;

    /// <summary>
    /// Number of rows for the grid.
    /// </summary>
    [SerializeField]
    private int numberOfRows = 10;

    /// <summary>
    /// Parent for all tiles and walls.
    /// </summary>
    [SerializeField]
    private Transform mapParentGo;

    /// <summary>
    /// List of all tiles.
    /// </summary>
    [SerializeField]
    private List<GameObject> Tiles = new List<GameObject>();

    /// <summary>
    /// Used to calculate the separation between each column
    /// </summary>
    private float tempSepX = 0;

    /// <summary>
    /// Used to calculate the separation between each row
    /// </summary>
    private float tempSepZ = 0;

    #endregion

    // ======================== Functional ====================== //

    #region Functional 


    /// <summary>
    /// Creates environment
    /// </summary>
    public void createEnvironmentalGrid()
    {
        Tiles.Clear();

        for (int i = 0; i < numberOfColumns; i++)
        {//loop 1 to loop through columns
            for (int j = 0; j < numberOfRows; j++)
            {
                //Instantiate(TheGridItem, new Vector3(i + tempSepX, 0, j + tempSepZ), Quaternion.identity);

                GameObject childObject;

                if (i == 0 || j == 0 || i == numberOfColumns - 1 || j == numberOfRows - 1)
                {
                    //loop 2 to loop through rows
                    childObject = GameObject.CreatePrimitive(PrimitiveType.Cube); //create a quad primitive as provided by unity
                    childObject.transform.position = new Vector3(i + tempSepX, 0.5f, j + tempSepZ); //position the newly created quad accordingly
                    childObject.name = "Wall";
                    childObject.tag = "Wall";
                    BoxCollider collider = childObject.GetComponent<BoxCollider>();
                    collider.isTrigger = true;
                    collider.size = new Vector3(0.9f, 0.9f, 0.9f);
                }
                else
                {
                    //loop 2 to loop through rows
                    childObject = GameObject.CreatePrimitive(PrimitiveType.Quad); //create a quad primitive as provided by unity
                    childObject.transform.position = new Vector3(i + tempSepX, 0, j + tempSepZ); //position the newly created quad accordingly
                    childObject.transform.eulerAngles = new Vector3(90f, 0, 0); //rotate the quads 90 degrees in X to face up
                    childObject.name = "Tile";
                    childObject.tag = "Tile";

                    Tiles.Add(childObject);
                }

                childObject?.transform.SetParent(mapParentGo);
            }
            tempSepZ = 0;//Reset the value of the seperation between the rows so it won‘t cumulate
        }
    }
    #endregion

    // ======================== Helper ====================== //

    #region Helper 

    /// <summary>
    /// Returns random tile
    /// </summary>
    public Vector3 GetRandomTileObject()
    {
        int RandomIndex = (int)System.Math.Min(Tiles.Count - 1, Random.Range(0, Tiles.Count));
        return Tiles[RandomIndex].transform.position;
    }

    #endregion

}
