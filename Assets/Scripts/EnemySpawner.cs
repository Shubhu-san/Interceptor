using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs; 
    [SerializeField] float timeBetweenWaves = 2f; 
    WaveConfigSO currentWave;
    // int waveNO = 0;
    [SerializeField] bool isPlaying = true; 
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves(){
        do
        {
            RandomWaves.Shuffle(waveConfigs);
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                // waveNO++;
                // Debug.Log("Wave " + waveNO);
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(j), 
                    currentWave.GetStartingWaypoint().position, 
                    Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }            
            }
        } while (isPlaying);
    }

    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }

    public class RandomWaves
    {
        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n+1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
