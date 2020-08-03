using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject grid;
    public Tilemap tilemap_boulder;
    public Tilemap tilemap_acidriver;
    public Tile acidTile;
    public Tile boulderTile;
    public Tile caesiumTile;
    public Tile CaOTile;
    public Tile waterTile;

    public GameObject bombPrefab;
    private GameObject player;
    public GameObject explosionPrefab;
    public GameObject CaOH2Prefab;
    private Vector3 playerOriginalPosition;

    public Vector3 gridScale;
    // Start is called before the first frame update
    private void Awake()
    {
        //Needs to be run in Awake so that the Player script can obtain the scale to scale the movement
        gridScale = grid.transform.localScale;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerOriginalPosition = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if (player)
        //{
        //    if (Input.GetKeyDown(KeyCode.B))
        //    {
        //        SetBomb();
        //    }
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        SetCAOH2();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }
    public void SetBomb()
    {
        Vector3 worldPos = player.transform.position;
        Vector3Int cell = tilemap_boulder.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap_boulder.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
    }
    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap_boulder.WorldToCell(worldPos);

        InteractCell(originCell + new Vector3Int(-1, 1, 0), "explode");
        InteractCell(originCell + new Vector3Int(0, 1, 0), "explode");
        InteractCell(originCell + new Vector3Int(1, 1, 0), "explode");
        //Explode 2nd row
        InteractCell(originCell + new Vector3Int(-1, 0, 0), "explode");
        InteractCell(originCell, "explode");
        InteractCell(originCell + new Vector3Int(1, 0, 0), "explode");
        //Explode 3rd row
        InteractCell(originCell + new Vector3Int(-1, -1, 0), "explode");
        InteractCell(originCell + new Vector3Int(0, -1, 0), "explode");
        InteractCell(originCell + new Vector3Int(1, -1, 0), "explode");

    }

    bool InteractCell(Vector3Int cell, string action)//Originally Vector3Int
    {
        Vector3 pos = new Vector3();
        if (action == "explode" || action == "playercheck")
        {
            pos = tilemap_boulder.GetCellCenterWorld(cell);
        }
        else
        {
            pos = tilemap_acidriver.GetCellCenterWorld(cell);
        }

        Collider2D objectCollider = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(gridScale.x,gridScale.y), 0f);

        if (action == "explode")
        {
            if (objectCollider)
            {
                if (objectCollider.tag == "Item")
                {
                    Debug.Log("ItemDestroyed");
                    Destroy(objectCollider.gameObject);
                }
                if (objectCollider.tag == "Player")
                {
                    Debug.Log("DestroyPlayer");
                    //Run game over script then run below( or maybe we will just reset the level. we'll see);
                    Destroy(objectCollider.gameObject);
                    //objectCollider.gameObject.SetActive(false);
                }
                //Might want to make it possible to kill monster as well
                Tile tile = tilemap_boulder.GetTile<Tile>(cell);
                if (tile == boulderTile)
                {
                    tilemap_boulder.SetTile(cell, null);
                }
            }

            Instantiate(explosionPrefab, pos, Quaternion.identity, grid.transform);
        }
        else if(action == "neutralize")
        {
            Tile tile = tilemap_acidriver.GetTile<Tile>(cell);
            if (tile == acidTile)
            {
                tilemap_acidriver.SetTile(cell, null);
            }
        }else if(action == "playercheck")
        {
            //if detect boulder, return false
            //If detect player return true, else return false
        }else if (action == "bouldercheck")
        {
            //true if got boulder, false if got no boulder
        }
        return true;
    }


      

    public void SetCAOH2()
    {
        Vector3 worldPos = player.transform.position;
        Vector3Int cell = tilemap_boulder.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap_boulder.GetCellCenterWorld(cell);

        Instantiate(CaOH2Prefab, cellCenterPos, Quaternion.identity, grid.transform);
    }

    public void Neutralize(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap_acidriver.WorldToCell(worldPos);
        //Neutralize 1st row
        InteractCell(originCell + new Vector3Int(-1, 1, 0), "neutralize");
        InteractCell(originCell + new Vector3Int(0, 1, 0), "neutralize");
        InteractCell(originCell + new Vector3Int(1, 1, 0), "neutralize");
        //Neutralize 2nd row
        InteractCell(originCell + new Vector3Int(-1, 0, 0), "neutralize");
        InteractCell(originCell, "neutralize");
        InteractCell(originCell + new Vector3Int(1, 0, 0), "neutralize");
        //Neutralize 3rd row
        InteractCell(originCell + new Vector3Int(-1, -1, 0), "neutralize");
        InteractCell(originCell + new Vector3Int(0, -1, 0), "neutralize");
        InteractCell(originCell + new Vector3Int(1, -1, 0), "neutralize");

    }

    public bool CheckForPlayer(Vector2 worldPos)
    {
        //true for player there, false for player not there
        return false;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
