using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    //public Tile wallTile;
    //public Tile boulderTile;

    public GameObject bombPrefab;
    public GameObject player;
    public GameObject explosionPrefab;
    public GameObject CaOH2Prefab;
    private Vector3 playerOriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerOriginalPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPos = player.transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 worldPos = player.transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

            Instantiate(CaOH2Prefab, cellCenterPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.gameObject.SetActive(true);
            player.transform.position = playerOriginalPosition;
        }
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        //Explode 1st row
        ExplodeCell(originCell + new Vector3Int(-1, 1, 0));
        ExplodeCell(originCell + new Vector3Int(0, 1, 0));
        ExplodeCell(originCell + new Vector3Int(1, 1, 0));
        //Explode 2nd row
        ExplodeCell(originCell + new Vector3Int(-1, 0, 0));
        ExplodeCell(originCell);
        ExplodeCell(originCell + new Vector3Int(1,0,0));
        //Explode 3rd row
        ExplodeCell(originCell + new Vector3Int(-1, -1, 0));
        ExplodeCell(originCell + new Vector3Int(0, -1, 0));
        ExplodeCell(originCell + new Vector3Int(1, -1, 0));
 
    }

    void ExplodeCell(Vector3Int cell)
    {
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Collider2D objectCollider = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(0.5f, 0.5f), 0f);
        if (objectCollider)
        {
            if (objectCollider.tag == "Boulder")
            {
                Debug.Log("BoulderDestroyed");
                Destroy(objectCollider.gameObject);
            }
            if (objectCollider.tag == "Player")
            {
                Debug.Log("DestroyPlayer");
                //Run game over script then run below( or maybe we will just reset the level. we'll see);
                //Destroy(objectCollider.gameObject);
                objectCollider.gameObject.SetActive(false);
            }
            //Might want to make it possible to kill monster as well
            else
            {
                Debug.Log(cell.x);
                Debug.Log(cell.y);
            }
        }
        else
        {
            //Debug.Log(cell.x);
            //Debug.Log(cell.y);
        }
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

    public void Neutralize(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        //Neutralize 1st row
        NeutralizeCell(originCell + new Vector3Int(-1, 1, 0));
        NeutralizeCell(originCell + new Vector3Int(0, 1, 0));
        NeutralizeCell(originCell + new Vector3Int(1, 1, 0));
        //Neutralize 2nd row
        NeutralizeCell(originCell + new Vector3Int(-1, 0, 0));
        NeutralizeCell(originCell);
        NeutralizeCell(originCell + new Vector3Int(1, 0, 0));
        //Neutralize 3rd row
        NeutralizeCell(originCell + new Vector3Int(-1, -1, 0));
        NeutralizeCell(originCell + new Vector3Int(0, -1, 0));
        NeutralizeCell(originCell + new Vector3Int(1, -1, 0));

    }

    void NeutralizeCell(Vector3Int cell)
    {
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Collider2D objectCollider = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(0.5f, 0.5f), 0f);
        if (objectCollider)
        {
            if (objectCollider.tag == "AcidRiver")
            {
                Debug.Log("RiverNeutralized");
                Destroy(objectCollider.gameObject);
            }
            else
            {
                Debug.Log(objectCollider.tag);
            }
        }
        //Might want to do something to the neutralized cell
        //Vector3 pos = tilemap.GetCellCenterWorld(cell);
        
    }
}
