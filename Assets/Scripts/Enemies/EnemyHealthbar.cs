using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour {

    protected float _maxHealth;
    protected float _currentHealth;
    protected Enemy _enemy;
    protected TextMesh _text;
    protected bool _empty = false;

    private void Awake()
    {
        _text = GetComponent<TextMesh>();
    }

    public void Reset(Enemy enemy, float health)
    {
        _enemy = enemy;
        _maxHealth = health;
        _currentHealth = health;
    }

    private void Update()
    {
        _text.text = "HP: " + _currentHealth + "/" + _maxHealth;
    }

    public void OnHit(float damage)
    {
        float newHealth = _currentHealth - damage;
        if (newHealth > 0)
        {
            _currentHealth = newHealth;
        }
        else
        {
            _empty = true;
        }   
    }

    public float GetHealth()
    {
        return _maxHealth;
    }

    public bool IsEmpty()
    {
        return _empty;
    }
}
