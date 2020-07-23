using UnityEngine;
using System.Collections.Generic;

public class Spawn_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Spawn Controls")]
    public float m_timeBetweenSpawns;
    public List<Transform> m_spawnLocations;



    //--- Private Variables ---//
    private List<GameObject> m_spawnedEnemies;
    private Wave_List m_waveList;
    private float m_timeSinceLastSpawn;
    private bool m_isStillSpawning;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_spawnedEnemies = new List<GameObject>();
        m_waveList = FindObjectOfType<Wave_List>();
        m_timeSinceLastSpawn = 0.0f;
        m_isStillSpawning = false;
    }

    private void Start()
    {
        // Start the first wave
        StartWave();
    }

    private void Update()
    {
        // Try to spawn a new enemy
        if (m_isStillSpawning)
        {
            // Increase the timer
            m_timeSinceLastSpawn += Time.deltaTime;

            // If enough time has passed, spawn the next enemy
            if (m_timeSinceLastSpawn >= m_timeBetweenSpawns)
                SpawnNextEnemy();
        }
    }



    //--- Methods ---//
    public void StartWave()
    {
        // Tell the wave spawner to start the wave
        m_waveList.StartWave();

        // We should start spawning enemies
        m_timeSinceLastSpawn = 0.0f;
        m_isStillSpawning = true;
    }

    public void SpawnNextEnemy()
    {
        // Get a random enemy from the wave's internal list
        GameObject enemyToSpawn = m_waveList.SelectNextEnemy();

        // If there are no more enemies to spawn, we shouldn't bother
        if (enemyToSpawn == null)
        {
            m_isStillSpawning = false;
            return;
        }

        // Randomly select a spawn location
        int spawnIdx = Random.Range(0, m_spawnLocations.Count);
        Transform spawnLoc = m_spawnLocations[spawnIdx];

        // Spawn the enemy and keep track of it so we can tell when the wave is over
        GameObject enemyObj = Instantiate(enemyToSpawn, spawnLoc.position, Quaternion.identity, null);
        m_spawnedEnemies.Add(enemyObj);

        // Reset the timer
        m_timeSinceLastSpawn = 0.0f;
    }

    public void EnemyKilled(GameObject _enemy)
    {
        // Remove the enemy from the spawned list
        m_spawnedEnemies.Remove(_enemy);
        Destroy(_enemy);
        // Check if the wave is now complete
        CheckForWaveCompletion();
    }

    public void CheckForWaveCompletion()
    {
        // The wave is complete if there are no more enemies to spawn and if the enemies that have spawned are all dead
        if (m_spawnedEnemies.Count == 0 && !m_isStillSpawning)
        {
            Debug.Log("Wave complete!");

            // Tell the wave list to move to the next wave in its list
            m_waveList.NextWave();

            // Get ready to start spawning again
            StartWave();
        }
    }
}
