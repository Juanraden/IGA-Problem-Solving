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

    public int minSquares = 5;
    public int maxSquares = 10;

    private Bounds spawnArea;
    private Vector2 spawnPosition;

    private List<GameObject> squaresPool = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        spawnArea = GetComponent<SpriteRenderer>().bounds;

        int spawnCount = Random.Range(minSquares, maxSquares);

        for (int i = 0; i < spawnCount; i++)
        {
            do
            {
                float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
                float y = Random.Range(spawnArea.min.y, spawnArea.max.y);

                spawnPosition = new Vector2(x, y);
            }
            while (CheckOverlap(spawnPosition, squarePrefab));

            SpawnSquare(spawnPosition);
        }
    }

    private GameObject getFromPool()
    {
        if (squaresPool.Count == 0)
        {
            return Instantiate(squarePrefab);
        }
        else
        {
            GameObject square = squaresPool[0];
            squaresPool.RemoveAt(0);
            return square;
        }
    }

    private void SpawnSquare(Vector2 position)
    {
        GameObject square = getFromPool();
        square.transform.position = position;
        square.SetActive(true);
    }

    private bool CheckOverlap(Vector2 pos, GameObject go)
    {
        Vector2 size = go.GetComponent<BoxCollider2D>().size;
        Collider2D hits = Physics2D.OverlapBox(pos, size, 0);
        if (hits != null)
            return true;
        return false;
    }

    public void DestroySquare(GameObject square)
    {
        square.SetActive(false);
        squaresPool.Add(square);
    }
}
