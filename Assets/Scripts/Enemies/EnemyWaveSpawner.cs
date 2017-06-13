using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference
//  https://www.youtube.com/watch?v=Vrld13ypX_I

public class EnemyWaveSpawner : MonoBehaviour {

    public enum State { SPAWNING, WAITING, COUNTING };

    public EnemyWave[] waves;
    public float countDown = 10.0f;
    public bool loopWaves = true;

    protected float _countDown;
    protected State _state = State.COUNTING;
    protected int _waveCount;

    private void Awake()
    {
        _waveCount = 0;
        _countDown = countDown;
    }

    IEnumerator Spwan(EnemyWave wave)
    {
        _state = State.SPAWNING;

        foreach (EnemyAttributes attributes in wave.waveAttributes)
        {
            GameObject newEnemy = Instantiate(wave.enemy);
            newEnemy.GetComponent<Enemy>().attributes = attributes;
            yield return new WaitForSeconds(1/wave.rate);
        }
        _state = State.WAITING;
        yield break;
    }

    private void Update()
    {
        if (_countDown <= 0 )
        {
            if (_waveCount >= waves.Length)
            {
                if (loopWaves) _waveCount = 0;
                return;
            }
            if (_state != State.SPAWNING)
            {
                StartCoroutine(Spwan(waves[_waveCount]));
                _waveCount++;
                _countDown = countDown;
            }
        }
        else
        {
            _countDown -= Time.deltaTime;
        }
    }
}
