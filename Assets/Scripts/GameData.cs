using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameData", menuName = "gameData")]
public class GameData : ScriptableObject
{
    public Dictionary<Vector2, GridTile> gridTileDict = new Dictionary<Vector2, GridTile>();

    public GridTile GetTile(Vector2 location)
    {
        return (gridTileDict[location]);
    }

}
