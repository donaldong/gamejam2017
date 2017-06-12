﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float angularSpeed = Mathf.Infinity;
    public int attacksPerSecond;
    public float attackDamage;

    protected GameObject _player;
    protected Weapon _weapon;
    protected bool _player_in_range;
    protected NavMeshAgent _nav;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected string _walkAnimation = "walk";
    protected string _attackAnimation = "attack";

    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weapon = _player.GetComponentInChildren<Weapon>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _nav.updateRotation = false;
    }

    public void Update()
    {
        _nav.SetDestination(_player.transform.position);
        RotateTowards(_player.transform);
        // When attack animation is finished, keep moving
        if (!_player_in_range && _anim.GetCurrentAnimatorStateInfo(0).IsName(_walkAnimation))
        {
            _nav.SetDestination(_player.transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
            SetPlayeInRange(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
            SetPlayeInRange(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject == _weapon.gameObject)
            {
                _rb.AddForceAtPosition(contact.normal * _weapon.impact, contact.point);
                OnWeaponHit(contact.normal, contact.point);
            }
        }
    }

    private void SetPlayeInRange(bool flag)
    {
        _player_in_range = flag;
        _anim.SetBool("playerInRange", _player_in_range);
        // Stop moving and play attack animation
        if (flag)
        {
            _nav.SetDestination(transform.position);
            _anim.Play(_attackAnimation);
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
    }

    protected virtual void OnWeaponHit(Vector3 dir, Vector3 pos)
    {
        _weapon.OnHitEnemy(pos);
    }
}