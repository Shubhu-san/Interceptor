using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
   [SerializeField] Transform pathPrefab;
   [SerializeField] float moveSpeed = 4f;
   [SerializeField] List<GameObject> enemyPrefab;
   [SerializeField] float timeBetweenEnemySpawn = 1f;
   [SerializeField] float spawnTimeVariance;
   [SerializeField] float minSpawnTime = 0.2f;

    public Transform GetStartingWaypoint(){
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints(){
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab)
        {
            waypoints.Add(waypoint);
        }
        return waypoints;
    }

   public float GetMoveSpeed(){
        return moveSpeed;
   }

   public int GetEnemyCount(){
        return enemyPrefab.Count;
   }

   public GameObject GetEnemyPrefab(int index){
        return enemyPrefab[index];
   }

   public float GetRandomSpawnTime(){
     float spawnTime = Random.Range(timeBetweenEnemySpawn - spawnTimeVariance, timeBetweenEnemySpawn + spawnTimeVariance);
     return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
   }

}
