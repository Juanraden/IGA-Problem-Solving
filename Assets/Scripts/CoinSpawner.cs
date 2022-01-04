using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    //Fungsi Singleton
    private static CoinSpawner _instance = null;
    public static CoinSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CoinSpawner>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: CoinSpawner not Found");
                }
            }

            return _instance;
        }
    }

    public GameObject coinPrefab;

    public int coinCount = 5;
    public float spawnDelay = 1f;

    private Bounds spawnArea;
    private Vector2 spawnPosition;

    private List<GameObject> coinPool = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        spawnArea = GetComponent<SpriteRenderer>().bounds;

        for (int i = 0; i < coinCount; i++)
        {
            SpawnCoin();
        }
    }

    private GameObject getFromPool()
    {
        if (coinPool.Count == 0)
        {
            return Instantiate(coinPrefab);
        }
        else
        {
            GameObject coin = coinPool[0];
            coinPool.RemoveAt(0);
            return coin;
        }
    }

    private void SpawnCoin()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        RandomPosition();

        GameObject coin = getFromPool();
        coin.transform.position = spawnPosition;
        coin.SetActive(true);
    }

    private bool CheckOverlap(Vector2 pos, GameObject go)
    {
        float radius = go.GetComponent<CircleCollider2D>().radius;
        Collider2D hits = Physics2D.OverlapCircle(pos, radius);
        if (hits != null)
            return true;
        return false;
    }

    public void DestroyCoin(GameObject coin)
    {
        coin.SetActive(false);
        ReturnToPool(coin);
    }

    public void ReturnToPool(GameObject coin)
    {
        coinPool.Add(coin);
        SpawnCoin();
    }

    public void RandomPosition()
    {
        do
        {
            float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float y = Random.Range(spawnArea.min.y, spawnArea.max.y);

            spawnPosition = new Vector2(x, y);
        }
        while (CheckOverlap(spawnPosition, coinPrefab));
    }
}
