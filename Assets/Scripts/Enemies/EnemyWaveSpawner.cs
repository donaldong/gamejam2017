using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference
//  https://www.youtube.com/watch?v=Vrld13ypX_I

public class EnemyWaveSpawner : MonoBehaviour {

    public enum State { SPAWNING, WAITING };

    public EnemyWave[] waves;
    public float countDown = 10.0f;
    public bool loopWaves = true;

    protected float _countDown;
    protected State _state = State.WAITING;
    protected int _waveCount;

    private void Awake()
    {
        _waveCount = 0;
        _countDown = countDown;
    }

    IEnumerator Spwan(EnemyWave wave)
    {
        _state = State.SPAWNING;

        foreach (EnemyAttributes attributes in wave.enemiesInWave)
        {
            GameObject newEnemy = Instantiate(wave.enemy, gameObject.transform);
            newEnemy.GetComponent<Enemy>().attributes = attributes;
            newEnemy.transform.localPosition = attributes.positionOffset;
            yield return new WaitForSeconds(1f/wave.rate);
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
            if (_state != State.SPAWNING)
            {
                _countDown -= Time.deltaTime;
            }
        }
    }
}
