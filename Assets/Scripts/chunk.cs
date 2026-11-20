using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chunk : MonoBehaviour
{
    [SerializeField] float[] lanes = { -3, 0, 3 };
    [SerializeField] GameObject FencePrefab;
    [SerializeField] float FenceHeight = 0.37f;
    List<int> availableLanes = new List<int> { 0, 1, 2 };
    [SerializeField] GameObject CoinPrefab;

    private void Start()
    {
        SpawnFences();
        SpawnCoins();
    }

    void SpawnFences()
    {
        int FencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < FencesToSpawn; i++) 
        {
            if (availableLanes.Count <= 0) break;

            int randomLanesIndex = Random.Range(0, availableLanes.Count);
            int SelectedlaneIndex = availableLanes[randomLanesIndex];
            availableLanes.RemoveAt(randomLanesIndex);

            Vector3 spawnPosition = new Vector3(lanes[SelectedlaneIndex], FenceHeight, transform.position.z);
          Instantiate(FencePrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    private void SpawnCoins()
    {
        int firstAvailableLane = availableLanes[0];
        Vector3 spawnPosition = new Vector3(lanes[firstAvailableLane]-0.8f, 1f, transform.position.z);
        Instantiate(CoinPrefab, spawnPosition, Quaternion.identity, transform);
    }
}
