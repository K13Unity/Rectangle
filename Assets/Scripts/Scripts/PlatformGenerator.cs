using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube;
using System;

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

    public event Action OnPlatformGenerationComplete;

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
        TriggerTile trigger = tile.GetComponent<TriggerTile>();
        trigger.OnPlayerEnter += ReportDestruction;
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
                int randomIndex = UnityEngine.Random.Range(0, remainingTiles.Count);
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
    private void ReportDestruction()
    {
        OnPlatformGenerationComplete?.Invoke();
    }
    
    public void DestroyPlatform()
    {
        RemoveTriggerTiles();
        StartCoroutine(DestroyTiles());
    }

    private void RemoveTriggerTiles()
    {
        foreach (var triggerTile in triggerTiles)
        {
            triggerTile.OnPlayerEnter -= ReportDestruction;
            Destroy(triggerTile.gameObject);
        }
        triggerTiles.Clear();
    }
    // Метод для знищення гравця
    private void DestroyPlayer()
    {
        Destroy(player.gameObject);
    }

   private IEnumerator DestroyTiles()
    {
        yield return new WaitForSeconds(0.5f); 
        yield return StartCoroutine(LowerTilesSequentially()); 
        yield return StartCoroutine(DestroyAllTiles()); 
        DestroyPlayer(); // 💥 
    }

    // 🔹 Вибирає 3 випадкові плитки для опускання
    private List<GameObject> SelectTilesToLower(List<GameObject> remainingTiles)
    {
        List<GameObject> selectedTiles = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            if (remainingTiles.Count == 0) break; 
            int randomIndex = UnityEngine.Random.Range(0, remainingTiles.Count); 
            selectedTiles.Add(remainingTiles[randomIndex]);
            remainingTiles.RemoveAt(randomIndex);
        }

        return selectedTiles;
    }

    // 🔹 Послідовно опускає всі плитки перед їх видаленням
    private IEnumerator LowerTilesSequentially()
    {
        List<GameObject> remainingTiles = new List<GameObject>(tiles); // 📋 Копіюємо список плиток

        while (remainingTiles.Count > 0) // 
        {
            List<GameObject> selectedTiles = SelectTilesToLower(remainingTiles); 

            foreach (var tile in selectedTiles)
            {
                if (tile != null)
                {
                    StartCoroutine(LowerTile(tile, new Vector3(tile.transform.position.x, riseHeight, tile.transform.position.z)));
                }
            }

            yield return new WaitForSeconds(riseInterval); 
        }
    }

    // 🔹 Видаляє всі плитки після завершення анімацій
    private IEnumerator DestroyAllTiles()
    {
        yield return new WaitForSeconds(riseInterval); 

        for (int i = tiles.Count - 1; i >= 0; i--) // 
        {
            if (tiles[i] != null)
            {
                Destroy(tiles[i]); 
            }
        }

        tiles.Clear(); 
    }
    
    private IEnumerator LowerTile(GameObject tile, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = tile.transform.position;

        while (tile != null && tile.transform.position != targetPosition)
        {
            float progress = (Time.time - startTime) * riseSpeed;
            tile.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }
    }
}
