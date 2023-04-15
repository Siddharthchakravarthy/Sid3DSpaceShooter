using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.ParticleSystemJobs;
public class Enemy : MonoBehaviour
{

    [Header("PowerUpSpawnTime")]
    public float PowerUpSpawnTime = 0.0f;
    public GameObject Powerup;
    
    public float PowerUpTimer = 0.0f;
    
    Vector3 SpawnLocation;
    public List<EnemyMovementScript> EnemyPrefabs;
    
    public float SpawnTime = 2.0f;
    public float Timer = 0.0f;
    public static ObjectPool<EnemyMovementScript> PoolOfEnemy;
    void Awake()
    {
        // EnemyPrefabs = GetComponent<EnemyMovementScript>();
        PoolOfEnemy = new ObjectPool<EnemyMovementScript>(CreateEnemy, OnTakeFromPool, OnReleaseFromPool,null, false, 100);
    }

    private EnemyMovementScript CreateEnemy() {
        int randEnemy = Random.Range(0, EnemyPrefabs.Count - 1);
        EnemyMovementScript instance = Instantiate( EnemyPrefabs[randEnemy].GetComponent<EnemyMovementScript>() , Vector3.zero, Quaternion.identity);
        EnemyPrefabs.Remove(EnemyPrefabs[randEnemy]);

        instance.transform.SetParent(transform, true);
        instance.gameObject.SetActive(false);
        return instance;
    }
    private void OnTakeFromPool(EnemyMovementScript instance) {
        instance.gameObject.SetActive(true);
        SpawnEnemy(instance);
    }

    private void OnReleaseFromPool(EnemyMovementScript Enemy) {
        Enemy.gameObject.SetActive(false);
    }

    private void SpawnEnemy(EnemyMovementScript Enemy) {
        SpawnLocation = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(2.40f, 4.4f), 45.0f);
        Enemy.transform.position = SpawnLocation;
    } 
    // Update is called once per frame
    void Update()
    {
        PowerUpSpawnTime = PowerUpHelper.GetSpawnTime();
    
        if(Time.time > Timer) {
            Timer = SpawnTime + Time.time;
            PoolOfEnemy.Get();
        }
        
        if(Time.time > PowerUpTimer && PowerUpBehaviour.GoingOn == false) {
            PowerUpTimer = PowerUpSpawnTime + Time.time;
            GameObject instance = instance = Instantiate(Powerup, SpawnLocation, Quaternion.identity);
            if(instance != null) {
                PowerUpBehaviour powerup = instance.GetComponent<PowerUpBehaviour>();
                PowerUpBehaviour.GoingOn = true;
                powerup.transform.SetParent(this.transform);
            }
        
        }
    }

    public static void ReturnToPool(EnemyMovementScript DoneItsJobObject) {
        PoolOfEnemy.Release(DoneItsJobObject);
    }
}
