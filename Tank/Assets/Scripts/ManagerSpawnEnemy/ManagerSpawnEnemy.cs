using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawnEnemy : MonoBehaviour
{
    [SerializeField] private int _MaxEnemiesOnMap;

    [SerializeField] private int _HowMuchBeEnemyPerRound;

    [SerializeField] private Transform[] _SpawnPoints;

    [SerializeField] private GameObject[] _EnemiesBeSpawn;

    [SerializeField] private List<GameObject> _Enemies;

    [SerializeField]private int _tanksJustAppeared;

    private int _RandomSpawnEnemy;
    private int _RandomPoint;

    private bool CompRandom;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        CompRandom = true;
    }
    private void Update()
    {
        if(_Enemies.Count < _MaxEnemiesOnMap)
        {
            SpawnOnMap();
        }
        for(int i = 0; i < _Enemies.Count; i++) {
            RemoveList(_Enemies[i]);
        }
        
            
    }
    private void SpawnOnMap() {
        StartCoroutine("RandomSpawnCar", 3f);
        if(_tanksJustAppeared <= _HowMuchBeEnemyPerRound)
        {
            GameObject enemy = Instantiate(_EnemiesBeSpawn[_RandomSpawnEnemy], _SpawnPoints[_RandomPoint].position, Quaternion.identity);
            _Enemies.Add(enemy);
        }
        else
        {
            //RoundEnd
            return;
        }
        _tanksJustAppeared++;
        if(CompRandom)
        {
            StartCoroutine("ChooseNumber", 1.0f);
        }
    }

    private void RemoveList(GameObject tank) {
        if(tank == null)
        {
            _Enemies.Remove(tank);
            Destroy(tank);
        }
    }

    IEnumerator ChooseNumber(float t)
    {
        CompRandom = false;
        _RandomPoint = Random.Range(0, 3);
        yield return new WaitForSeconds(t);
        CompRandom = true;
    }

    IEnumerator RandomSpawnCar(float t)
    {
        int randomNumber = Random.Range(0, 101);
        if(20 > randomNumber)
        {
            _RandomSpawnEnemy = 1;
        }
        else
        {
            _RandomSpawnEnemy = 0;
        }
        yield return new WaitForSeconds(t);
        CompRandom = true;
    }
}
