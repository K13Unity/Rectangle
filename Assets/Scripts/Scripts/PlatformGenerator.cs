using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Префаб звичайної плитки
    [SerializeField] private TriggerTile triggerTilePrefab; // Префаб невидимої плитки-тригера
    [SerializeField] private CubeController playerPrefab; // Префаб гравця
    [SerializeField] private SoundManager soundManager; // Посилання на SoundManager
    [SerializeField] float riseSpeed = 1f;
    [SerializeField] float riseHeight = -10f;
    [SerializeField] float riseInterval = 0.3f;
    private CubeController player;
    List<GameObject> tiles = new List<GameObject>();
    List<TriggerTile> triggerTiles = new List<TriggerTile>();


    public void GeneratePlatform(int[,] layout)
    {
        int rows = layout.GetLength(0);
        int cols = layout.GetLength(1);

        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < cols; z++)
            {
                if(layout[x, z] == 1)
                {
                    CreateTile(x, z);
                }
                else if(layout[x, z] == 2)
                {
                    CreateTriggerTile(x, z);
                }
                else if(layout[x, z] == 3)
                {
                    Vector3 startPosition = new Vector3(x, -riseHeight, z);
                    CreateTile(x, z);
                    player = CreatePlayer(startPosition);
                    
                }
            }
        }
        StartCoroutine(RaiseTilesRandomly());
    }

    private GameObject CreateTile(int x, int z)
    {
        Vector3 startPosition = new Vector3(x, riseHeight, z);
        var tile = Instantiate(tilePrefab, startPosition, Quaternion.identity, transform);
        tiles.Add(tile);
        return tile;
    }
    
    private TriggerTile CreateTriggerTile(int x, int z)
    {
        var tile = Instantiate(triggerTilePrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
         //додати піписку на подію
        triggerTiles.Add(tile);
        return tile;
    }

    private CubeController CreatePlayer(Vector3 startPosition)
    {
        var player = Instantiate(playerPrefab, startPosition, Quaternion.identity);
        return player;
    }


    IEnumerator RiseTile(GameObject tile, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = tile.transform.position;

        while (tile.transform.position != targetPosition)
        {
            float progress = (Time.time - startTime) * riseSpeed;
            tile.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }
    }

     private IEnumerator RaiseTilesRandomly()
    {
        List<GameObject> remainingTiles = new List<GameObject>(tiles);

        while (remainingTiles.Count > 0)
        {
            List<GameObject> selectedTiles = new List<GameObject>();

            for (int i = 0; i < 3; i++)
            {
                if (remainingTiles.Count == 0) break;
                int randomIndex = Random.Range(0, remainingTiles.Count);
                selectedTiles.Add(remainingTiles[randomIndex]);
                remainingTiles.RemoveAt(randomIndex);
            }

            foreach (var tile in selectedTiles)
            {
                StartCoroutine(RiseTile(tile, new Vector3(tile.transform.position.x, 0f, tile.transform.position.z)));
            }

            yield return new WaitForSeconds(riseInterval);
        }
        yield return StartCoroutine(RiseTile(player.gameObject, new Vector3(player.transform.position.x, 1f, player.transform.position.z)));
        player.Init(soundManager);
        
    }
    //StartCoroutine(RiseTile(tile, new Vector3(pos.x, 0f, pos.y)));
    
}
