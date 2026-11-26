using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaveSystem : MonoBehaviour
{
    [Header("Waves System")]
    public List<GameObject> enemies = new List<GameObject>();
    public float TimeToSpawn;
    public Transform[] spawnPoint;
    public float maxCountEnemy = 30;
    public int enemyCount;
    public int RandomPoint(){
        int spawnIndex = Random.Range(0, spawnPoint.Length);
        return spawnIndex;
    }
    
    //Spawnear Enemigos
    protected virtual void SpawnEnemy(){
        for (enemyCount = 0; enemyCount < maxCountEnemy; enemyCount++){
            RandomPoint();

        }
    }
}
