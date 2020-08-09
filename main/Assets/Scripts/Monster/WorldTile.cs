using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    // Start is called before the first frame update
    public int gridX;
    public int gridY;
    public float convertX;
    public float convertY;
    public List<WorldTile> myNeighbours;
    public bool walkable;
    WorldTile parent;
    int gCost;
    int hCost;
    void Start()
    {
        convertX = 7.5f;
        convertY = 4.5f;
    }
    public void SetG(int val)
    {
        gCost = val;
    }

    public void SetH(int val)
    {
        hCost = val;
    }
    public int GetH()
    {
        return hCost;
    }
    public int GetG()
    {
        return gCost;
    }
    public Vector3 GetVector3Pos()
    {
        float xVal = gridX - convertX;
        float yVal = gridY - convertY;
        return new Vector3(xVal, yVal, 0);
    }
    public virtual void OnSelect()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }
    public virtual void OnDeselect()
    {
        if(walkable == true)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }
    public bool isTileWalkable()
    {
        return walkable;
    }
    public void setTileWalkable(bool canWalk)
    {
        walkable = canWalk;
    }
    public WorldTile getParent()
    {
        return parent;
    }
    public void setParent(WorldTile tile)
    {
        parent = tile;
    }
    public Vector2Int getGridCoords()
    {
        return new Vector2Int(gridX, gridY);
    }

    public void setGridCoords(Vector2Int coords)
    {
        gridX = coords.x;
        gridY = coords.y;
    }

    public int fCost//new for path finding
    {
        get
        {
            return gCost + hCost;//estimation of total route to destination if this tile used.
        }
    }
}
