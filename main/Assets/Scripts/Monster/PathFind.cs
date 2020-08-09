using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    public static PathFind me;
    public CreateNodesFromTilemaps mappedNodes;
    // Start is called before the first frame update
    void Start()
    {
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector3> getPath(Vector3 startPos, Vector3 endPos)
    {
        List<WorldTile> listOfTiles = new List<WorldTile>();
        getPath(startPos, endPos, ref listOfTiles);
        List<Vector3> retVal = convertToVectorPath(listOfTiles);
        return retVal;
    }

    void getPath(Vector3 startPos, Vector3 endPos, ref List<WorldTile> store)
    {
        Vector2 sPos = new Vector2();
        Vector2 ePos = new Vector2();

        sPos.x = startPos.x + 7.5f;
        sPos.y = startPos.y + 4.5f;
        ePos.x = endPos.x + + 7.5f;
        ePos.y = endPos.y + 4.5f;
        
        //Vector2 sPos = new Vector2((int)startPos.x, (int)startPos.y);
        //Vector2 ePos = new Vector2((int)endPos.x, (int)endPos.y);//convert pos to int to get rough tile coords from them
        
        WorldTile startNode = mappedNodes.GetWorldTile((int)sPos.x,(int)sPos.y);
        WorldTile targetNode = mappedNodes.GetWorldTile((int)ePos.x, (int)ePos.y);
        //if dest not walkable, break
        if (targetNode.isTileWalkable() == false)
        {
            Debug.Log("targetNode is not walkable");
            Debug.Log(targetNode.gridX);
            Debug.Log(targetNode.gridY);
            return;
        }

        //tiles to check
        List<WorldTile> openSet = new List<WorldTile>();
        //tiles checked and not to be included
         List<WorldTile> closedSet = new List<WorldTile>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            WorldTile node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                //check if we can find a tile with lower or equal f cost(approx of distance to target from start including tile)
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    //if tile has lower distance to target tile than current one in node
                    if (openSet[i].GetH() < node.GetH())
                    {
                        //set node to be closer tile
                        node = openSet[i];
                    }
                }
            }
            //take node from open set and put to closed set
            openSet.Remove(node);
            closedSet.Add(node);

            //check to see if node is tile we want to go to. If so, return path after it has been retraced
            if (node == targetNode)
            {
                Debug.LogError("FINISHED PATH");
                RetracePath(startNode, targetNode, ref store);
                return;
            }

            foreach (WorldTile neighbour in node.myNeighbours)
            {
                if (!neighbour.isTileWalkable() || closedSet.Contains(neighbour) || neighbour == null || node == null)
                {
                    continue;
                }
                //Calc cost of going to neighbour from start to Path
                int newCostToNeighbour = node.GetG() + GetDistance(node, neighbour);
                //if cost is shorter and open set doesnt contain neighbour
                if (newCostToNeighbour < neighbour.GetG() || !openSet.Contains(neighbour))
                {
                    //Set G n H values of neighbours
                    neighbour.SetG(newCostToNeighbour);
                    neighbour.SetH(GetDistance(neighbour, targetNode));
                    //sets parent of neighbour to be node signifying that in path you'll go from node to neighbor
                    neighbour.setParent(node);


                    //add neighbour to openset if not already there
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    //convert path found to list of Vector3Int 
    List<Vector3> convertToVectorPath(List<WorldTile> tiles)
    {
        List<Vector3> retVal = new List<Vector3>();
        foreach(WorldTile tile in tiles)
        {
            retVal.Add(tile.GetVector3Pos());
        }
        return retVal;
    }
    //goes thru path via parent variable and puts it in a list
    void RetracePath(WorldTile startNode, WorldTile targetNode, ref List<WorldTile> store)
    {
        List<WorldTile> path = new List<WorldTile>();
        WorldTile currentNode = targetNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.getParent();
        }
        //reverse because it traces path from finish to start using parent variable
        path.Reverse();
        store = path;
    }

    //gets distance between two grid coords n returns them multiplied
    int GetDistance(WorldTile nodeA, WorldTile nodeB)
    {
        int dstX = Mathf.Abs(nodeA.getGridCoords().x - nodeB.getGridCoords().x);
        int dstY = Mathf.Abs(nodeA.getGridCoords().y - nodeB.getGridCoords().y);
        
        //make sure final number is positive
        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
} 

