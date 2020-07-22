using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Wave_EnemyRequirements
{
    public GameObject m_enemyObj;
    public int m_requiredCount;
}

[System.Serializable]
public struct Wave_EnemyRatio
{
    public Wave_EnemyRatio(GameObject _enemyObj, float _ratio)
    {
        this.m_enemyObj = _enemyObj;
        this.m_ratio = _ratio;
    }

    public GameObject m_enemyObj;
    public float m_ratio;
}

[System.Serializable]
public class Wave_Descriptor
{
    //--- Public Variables ---//
    [Header("Required Wave Information")]
    public List<Wave_EnemyRequirements> m_requiredEnemies;

    [Header("Random Wave Information")]
    public int m_numRandomSpawns;
    public List<Wave_EnemyRatio> m_randomEnemies;



    //--- Methods ---//
    public List<GameObject> GenerateEnemyList()
    {
        // Create a list to hold all of the enemies that should be spawned in this wave
        // This list will have duplicates to make randomly spawning easy
        List<GameObject> enemyList = new List<GameObject>();

        // Start by adding the required enemies
        AddRequiredEnemies(ref enemyList);

        // Now generate the ratio values for each of the random enemies
        // These will always end with a value of 1
        // If the random value is lower then a ratio value, that enemy is selected
        // Ex: 0 - enemy 1 - 0.1 - enemy 2 - 0.6 - enemy 3 - 1.0
        // Enemy 1 will be selected <= 0.1, enemy <= 0.6, and enemy 3 <= 1.0
        List<Wave_EnemyRatio> ratioValues = GenerateEnemyRandRanges();

        // Using the generated ratios, randomly select the enemies to add to the wave
        AddRandomEnemies(ref enemyList, ratioValues);

        // Return the final list
        return enemyList;
    }

    

    //--- Utility Functions ---//
    private void AddRequiredEnemies(ref List<GameObject> _enemyList)
    {
        // Add the required enemies into the list as many times as requested
        foreach(var reqEnemy in m_requiredEnemies)
        {
            for (int i = 0; i < reqEnemy.m_requiredCount; i++)
                _enemyList.Add(reqEnemy.m_enemyObj);
        }
    }

    private List<Wave_EnemyRatio> GenerateEnemyRandRanges()
    {
        // If there are no random enemies selected for this wave, just back out
        if (m_randomEnemies.Count == 0)
            return null; 

        // Start by summing up all of the inputted values
        float ratioSum = 0.0f;
        foreach (var randomEnemyDesc in m_randomEnemies)
            ratioSum += randomEnemyDesc.m_ratio;

        // Now, normalize all of the values to ensure they are on a scale of 0 - 1
        // This results in a series of % chances for each enemy
        List<Wave_EnemyRatio> normalizedRatios = new List<Wave_EnemyRatio>();
        foreach (var randomEnemyDesc in m_randomEnemies)
            normalizedRatios.Add(new Wave_EnemyRatio(randomEnemyDesc.m_enemyObj, randomEnemyDesc.m_ratio / ratioSum));

        // Finally, the values should be added onto the previous in the list to create ranges of %s where each enemy will spawn
        // We have to manually add the first one since its upper bound is simply its current value (0 - currentValue is the range)
        List<Wave_EnemyRatio> rangedRatios = new List<Wave_EnemyRatio>();
        rangedRatios.Add(normalizedRatios[0]);
        for(int i = 1; i < normalizedRatios.Count; i++)
        {
            float rangeUpperBound = normalizedRatios[i].m_ratio + normalizedRatios[i - 1].m_ratio;
            rangedRatios.Add(new Wave_EnemyRatio(normalizedRatios[i].m_enemyObj, rangeUpperBound));
        }

        // Return the created ratio ranges
        return rangedRatios;
    }

    private void AddRandomEnemies(ref List<GameObject> _enemyList, List<Wave_EnemyRatio> _rangeUpperBounds)
    {
        // Randomly select enemies based on their ratios until the required number of enemies have been selected
        for (int i = 0; i < m_numRandomSpawns; i++)
        {
            // Generate a value between 0 - 1
            float randVal = Random.value;

            // Check each of the ratio upper bounds to see if this random value fits under it
            foreach(var upperBound in _rangeUpperBounds)
            {
                // If it does, this enemy can be added to the list and we can move on
                if (randVal <= upperBound.m_ratio)
                {
                    _enemyList.Add(upperBound.m_enemyObj);
                    break;
                }
            }
        }
    }
}
