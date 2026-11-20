
using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private GameObject obstacleDad;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnWidth = 3f;

    private GameManager gm;

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();

        if (gm == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (gm.CurrentState != GameManager.GameState.GameOver)
        {
            if (gm.CurrentState == GameManager.GameState.Paused)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(spawnInterval);

            if (gm.CurrentState == GameManager.GameState.GameOver)
                yield break;

            if (gm.CurrentState == GameManager.GameState.Paused)
                continue;

            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null || obstaclePrefab.Length == 0)
            return;

        GameObject obstacle = obstaclePrefab[
            Random.Range(0, obstaclePrefab.Length)
        ];

        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnWidth, spawnWidth),
            transform.position.y,
            transform.position.z
        );

        Instantiate(
            obstacle,
            spawnPosition,
            Random.rotation,
            obstacleDad.transform
        );
    }
}
