using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    //Fungsi Singleton
    private static SquareSpawner _instance = null;
    public static SquareSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SquareSpawner>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: SquareSpawner not Found");
                }
            }

            return _instance;
        }
    }

    public GameObject squarePrefab;

    public int spawnCount = 10;
    public float respawnDelay = 3f;

    private Bounds spawnArea;
    private Vector2 spawnPosition;

    private List<Square> squaresPool = new List<Square>();

    // Start is called before the first frame update
    private void Start()
    {
        spawnArea = GetComponent<SpriteRenderer>().bounds;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnSquare();
        }
    }

    private Square getFromPool()
    {
        Square square;

        if (squaresPool.Count == 0)
        {
            GameObject newSquare = Instantiate(squarePrefab);
            square = newSquare.GetComponent<Square>();
        }
        else
        {
            square = squaresPool[0];
            squaresPool.RemoveAt(0);
        }
        return square;
    }

    private void SpawnSquare()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        do
        {
            float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float y = Random.Range(spawnArea.min.y, spawnArea.max.y);

            spawnPosition = new Vector2(x, y);
        }
        while (CheckOverlap(spawnPosition, squarePrefab));

        Square square = getFromPool();
        square.Activate(spawnPosition);
    }

    private bool CheckOverlap(Vector2 pos, GameObject go)
    {
        Vector2 size = go.GetComponent<BoxCollider2D>().size;
        Collider2D hits = Physics2D.OverlapBox(pos, size, 0);
        if (hits != null)
            return true;
        return false;
    }

    public void ReturnToPool(Square square)
    {
        squaresPool.Add(square);
        Invoke("SpawnSquare", respawnDelay);
    }

    public void ReturnToPool2(Square square)
    {
        squaresPool.Add(square);
        SpawnSquare();
    }
}
