using UnityEngine;
using System.Collections.Generic;

public class Wave_List : MonoBehaviour
{
    //--- Public Variables ---//
    public List<Wave_Descriptor> m_waves;



    //--- Private Variables ---//
    private List<GameObject> m_enemiesLeftToSpawn;
    private Wave_Descriptor m_currentWave;
    private int m_currentWaveIdx;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_enemiesLeftToSpawn = new List<GameObject>();
        m_currentWave = null;
        m_currentWaveIdx = 0;
    }

    private void Start()
    {
        // Start the first wave
        StartWave();
    }



    //--- Methods ---//
    public void StartWave()
    {
        if (m_currentWaveIdx < m_waves.Count)
        {
            // Set the current wave reference
            m_currentWave = m_waves[m_currentWaveIdx];

            // Start by having the wave object generate the list of enemies to spawn
            m_enemiesLeftToSpawn = m_currentWave.GenerateEnemyList();

            string waveText = "";
            foreach (var obj in m_enemiesLeftToSpawn)
                waveText += obj.ToString() + "\n";
            Debug.Log(waveText);
        }
    }

    public void NextWave()
    {
        // Increase the current wave
        m_currentWaveIdx++;

        // If we reached the end of the waves, end the game
        if (m_currentWaveIdx >= m_waves.Count)
            Debug.Log("GAME OVER: All waves complete");
    }

    public GameObject SelectNextEnemy()
    {
        // If there are no enemies left to spawn, return null
        if (m_enemiesLeftToSpawn.Count == 0)
            return null;

        // Otherwise, we should randomly select one of the enemies
        int randIdx = Random.Range(0, m_enemiesLeftToSpawn.Count);
        var enemy = m_enemiesLeftToSpawn[randIdx];

        // We should remove the enemy from the list since it is about to be spawned
        m_enemiesLeftToSpawn.RemoveAt(randIdx);

        // Return the enemy object so it can be spawned
        return enemy;
    }
}
