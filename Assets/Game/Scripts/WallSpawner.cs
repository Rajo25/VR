using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallSpawner : MonoBehaviour
{
    [Header("Grid")]
    public int columns = 5;
    public int rows = 5;

    public float cellWidth = 1.5f;
    public float cellHeight = 1.5f;

    public float spawnDistance = 30f;

    [Header("Prefabs")]
    public GameObject[] prefabs;
    public int maxAlive = 15;

    [Header("Spawn")]
    public float startSpawnDelay = 1f;
    public float minimumSpawnDelay = 0.3f;
    public float speedIncreaseEvery = 15f;
    public float spawnDelayMultiplier = 0.9f;

    public Transform parent;

    private int aliveCount;
    private float currentSpawnDelay;

    private void Start()
    {
        currentSpawnDelay = startSpawnDelay;

        StartCoroutine(SpawnLoop());
        StartCoroutine(SpeedLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (aliveCount < maxAlive)
                SpawnRandom();

            yield return new WaitForSeconds(currentSpawnDelay);
        }
    }

    IEnumerator SpeedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseEvery);

            currentSpawnDelay *= spawnDelayMultiplier;
            currentSpawnDelay = Mathf.Max(currentSpawnDelay, minimumSpawnDelay);
        }
    }

    void SpawnRandom()
    {
        if (prefabs.Length == 0)
            return;

        int col = Random.Range(0, columns);
        int row = Random.Range(0, rows);

        Spawn(col, row);
    }

    public void Spawn(int column, int row)
    {
        float width = (columns - 1) * cellWidth;
        float height = (rows - 1) * cellHeight;

        float x = column * cellWidth - width / 2f;
        float y = row * cellHeight;

        Vector3 position =
            transform.position +
            transform.forward * spawnDistance +
            transform.right * x +
            transform.up * y;

        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        Quaternion rotation = Quaternion.LookRotation(-transform.forward);

        GameObject obj = Instantiate(prefab, position, rotation, parent);

        aliveCount++;

        GrowAndDieOnHit grow = obj.GetComponent<GrowAndDieOnHit>();

        if (grow != null)
            grow.Initialize(this);
    }

    public void NotifyPrefabDied()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        float width = (columns - 1) * cellWidth;
        float height = (rows - 1) * cellHeight;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                float px = x * cellWidth - width / 2f;
                float py = y * cellHeight;

                Vector3 pos =
                    transform.position +
                    transform.forward * spawnDistance +
                    transform.right * px +
                    transform.up * py;

                Gizmos.DrawWireCube(
                    pos,
                    new Vector3(cellWidth * 0.9f, cellHeight * 0.9f, 0.1f));
            }
        }
    }
}