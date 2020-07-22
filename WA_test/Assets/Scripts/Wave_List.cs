using UnityEngine;
using System.Collections.Generic;

public class Wave_List : MonoBehaviour
{
    //--- Public Variables ---//
    public List<Wave_Descriptor> m_waves;



    //--- Private Variables ---//
    private List<GameObject> m_enemiesLeftToSpawn;
    private List<GameObject> m_spawnedEnemies;
    private Wave_Descriptor m_currentWave;
    private int m_currentWaveIdx;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_enemiesLeftToSpawn = new List<GameObject>();
        m_spawnedEnemies = new List<GameObject>();
        m_currentWave = null;
        m_currentWaveIdx = 0;

        // Start the first wave
        StartWave();
    }



    //--- Methods ---//
    public bool CheckIfWaveComplete()
    {
        // If there are no more enemies, the wave is complete
        return (m_spawnedEnemies.Count == 0);
    }

    public void NextWave()
    {
        // Increase the current wave
        m_currentWaveIdx++;

        // If we reached the end of the waves, end the game
        // Otherwise, start the next wave
        if (m_currentWaveIdx >= m_waves.Count)
            Debug.Log("GAME OVER: All waves complete");
        else
            StartWave();
    }

    public void StartWave()
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
