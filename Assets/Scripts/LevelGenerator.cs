using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private GameObject chunkDad;
    [SerializeField] private int numberOfChunks = 10;
    [SerializeField] private float chunkLength = 10f;
    [SerializeField] private float chunkMoveSpeed = 5f;

    List<GameObject> chunks = new List<GameObject>();
    //GameObject[] chunks = new GameObject[10];

    private void Start()
    {
        chunkSpawner();
    }
    private void Update()
    {
        chunkMove();
    }

    private void chunkSpawner()
    {
        for (int i = 0; i < numberOfChunks; i++)
        {
            float chunkZ;
            chunkZ = (i * chunkLength) + transform.position.z;

            Vector3 chunkPos = new Vector3(transform.position.x, transform.position.y, chunkZ);
            GameObject newChunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkDad.transform);
            chunks.Add(newChunk);
        }
    }

    private void SpawnNewChunk()
    {
        float chunkZ;
        chunkZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        Vector3 chunkPos = new Vector3(transform.position.x, transform.position.y, chunkZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkDad.transform);
        chunks.Add(newChunk);
    }

    private void chunkMove()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(Vector3.back * chunkMoveSpeed * Time.deltaTime);
            if (chunk.transform.position.z < -chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnNewChunk();
            }
        }
    }
}
