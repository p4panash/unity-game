using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING , COMPLETE};

    [System.Serializable]
	public class Wave {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountDown = timeBetweenWaves;
    }

    void Update() {
        if (state == SpawnState.WAITING) {
            if(!EnemyIsAlive()) {
                //Begin a new wave
                WaveCompleted();
            }
        }

        if (waveCountDown <= 0) {
            Debug.Log(state);
            if (state == SpawnState.COUNTING) {
                //start spawning
                Debug.Log("SPAWNING");
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountDown -= Time.deltaTime;
        }
        
    }

    void WaveCompleted() {
        Debug.Log("Wave completed !");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1) {
            Debug.Log("LEVEL CLEARED !");
            state = SpawnState.COMPLETE;
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f) {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)
                return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawn wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy, i);
            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy, int i) {
        //Spawn enemy
        Instantiate(_enemy, _enemy.transform.position, _enemy.transform.rotation);
        Debug.Log("Spawining enemy: " + (i + 1));
    }

}
