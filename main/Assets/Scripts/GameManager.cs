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
    public Tilemap tilemap_bg;
    public AnimatedTile acidTile;
    public Tile boulderTile;
    public Tile caesiumTile;
    public Tile CaOTile;
    public Tile waterTile;

    public GameObject bombPrefab;
    private GameObject player;
    public GameObject explosionPrefab;
    public GameObject neutralizaitonPrefab;
    public GameObject CaOH2Prefab;
    public GameObject redLightPrefab;
    public GameObject yellowLightPrefab;
    public GameObject purpleLightPrefab;
    public GameObject blueLightPrefab;
    public GameObject orangeLightPrefab;
    private Vector3 playerOriginalPosition;

    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;

    public AudioClip acidRiverSFX;
    public AudioClip boulderBreakSFX;
    public AudioClip collectItemSFX;
    public AudioClip neutralizeSFX;
    public AudioClip placeItemSFX;
    public AudioClip toggleInvSFX;
    public AudioClip successCraftSFX;
    public AudioClip failCraftSFX;

    public AudioClip playerScreamSFX;
    public AudioClip goblinLaughSFX;
    public AudioClip goblinDistractedSFX;
    public AudioClip goblinAlertedSFX;
    public AudioClip goblinChaseSFX;

    public CustomInputManager customInputManager;
    public Vector3 gridScale;
    private bool isWalking = false;

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
        bgmPlayer.Play();

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
        //    if (Input.GetKeyDown(KeyCode.T))
        //    {
        //        SetTorch("Blue");
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    RestartGame();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SetTorch("Red");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SetTorch("Yellow");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SetTorch("Purple");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SetTorch("Blue");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    SetTorch("Orange");
        //}
        if (customInputManager.GetKeyDown("Restart"))
        {
            RestartGame();
        }

    }
    public void SetBomb()
    {
        PlaySFX("placeitem");
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
                    PlaySFX("playerscream");
                    //Run game over script then run below( or maybe we will just reset the level. we'll see);
                    Destroy(objectCollider.gameObject);
                    //Might want to add dying sound
                    //objectCollider.gameObject.SetActive(false);
                }
                //Might want to make it possible to kill monster as well
                Tile tile = tilemap_boulder.GetTile<Tile>(cell);
                if (tile == boulderTile)
                {
                    tilemap_boulder.SetTile(cell, null);
                    PlaySFX("boulderbreak");
                }
            }

            Instantiate(explosionPrefab, pos, Quaternion.identity, grid.transform);
        }
        else if(action == "neutralize")
        {
            AnimatedTile tile = tilemap_acidriver.GetTile<AnimatedTile>(cell);
            Debug.Log("NEUTRALIZING");
            Debug.Log(tile);
            if (tile == acidTile)
            {
                tilemap_acidriver.SetTile(cell, null);
                PlaySFX("neutralize");
            }
            Instantiate(neutralizaitonPrefab, pos, Quaternion.identity, grid.transform);
        }
        else if(action == "playercheck")
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
        PlaySFX("placeitem");
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

    public void SetTorch(string color)
    {
        PlaySFX("placeitem");
        Vector3 worldPos = player.transform.position;
        Vector3Int cell = tilemap_boulder.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap_boulder.GetCellCenterWorld(cell);
        if (color == "Red")
        {
            Instantiate(redLightPrefab, cellCenterPos, Quaternion.identity);
        }
        else if(color == "Yellow")
        {
            Instantiate(yellowLightPrefab, cellCenterPos, Quaternion.identity);
        }
        else if (color == "Purple")
        {
            Instantiate(purpleLightPrefab, cellCenterPos, Quaternion.identity);
        }
        else if (color == "Blue")
        {
            Instantiate(blueLightPrefab, cellCenterPos, Quaternion.identity);
        }
        else if (color == "Orange")
        {
            Instantiate(orangeLightPrefab, cellCenterPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Error in color: " + color);
        }
        
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

    public void PlaySFX(string sfx)
    {
        if (sfx == "boulderbreak")
        {
            sfxPlayer.PlayOneShot(boulderBreakSFX);
        }else if(sfx == "neutralize")
        {
            sfxPlayer.PlayOneShot(neutralizeSFX);
        }else if(sfx == "acidriverkill")
        {
            sfxPlayer.PlayOneShot(acidRiverSFX);
        }else if(sfx == "collectitem")
        {
            sfxPlayer.PlayOneShot(collectItemSFX);
        }else if(sfx == "placeitem")
        {
            sfxPlayer.PlayOneShot(placeItemSFX);
        }else if(sfx == "playerscream")
        {
            sfxPlayer.PlayOneShot(playerScreamSFX);
        }else if(sfx == "goblinlaugh")
        {
            sfxPlayer.PlayOneShot(goblinLaughSFX);
        }else if(sfx == "goblinalerted")
        {
            sfxPlayer.PlayOneShot(goblinAlertedSFX);
        }else if(sfx == "goblindistracted")
        {
            sfxPlayer.PlayOneShot(goblinDistractedSFX);
        }else if(sfx == "goblinchase")
        {
            sfxPlayer.PlayOneShot(goblinChaseSFX);
        }else if(sfx == "toggleinv")
        {
            sfxPlayer.PlayOneShot(toggleInvSFX);
        }else if(sfx == "successcraft"){
            sfxPlayer.PlayOneShot(successCraftSFX);
        }else if(sfx == "failcraft")
        {
            sfxPlayer.PlayOneShot(failCraftSFX);
        }
        else
        {
            Debug.Log("SFX asked to played does not exist");
            Debug.Log(sfx);
        }
    }

    public bool LookForPlayer(Transform monster)
    {
        Debug.Log("Running LookForPlayer");
        RaycastHit2D[] hits;
        if (player == null)
        {
            return false;
        }
        else
        {
            hits = Physics2D.LinecastAll(monster.position, player.transform.position);
            bool playerFound = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Boulder"))
                {
                    playerFound = false;
                    break;
                }
                if (hit.collider.CompareTag("Player"))
                {
                    //monster.position = player.transform.position;
                    playerFound = true;
                    break;
                }

            }
            return playerFound;
        }
   

    }

    public Transform ReturnPlayerPosition()
    {
        return player.transform;
    }


    public bool LookForFlame(Transform monster)
    {
        Debug.Log("Running LookForPlayer");
        RaycastHit2D[] hits;
        hits = Physics2D.LinecastAll(monster.position, player.transform.position);
        bool flameFound = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Boulder"))
            {
                flameFound = false;
                break;
            }
            if (hit.collider.CompareTag("Player"))
            {
                //monster.position = player.transform.position;
                flameFound = true;
                break;
            }

        }
        return flameFound;

    }

}
