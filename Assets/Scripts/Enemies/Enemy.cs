using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public EnemyAttributes attributes;

    protected GameObject _player;
    protected Weapon _weapon;
    protected bool _player_in_range;
    protected NavMeshAgent _nav;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected EnemyHealthbar _healthbar;
    protected string _walkAnimation = "walk";
    protected string _attackAnimation = "attack";
    protected bool _is_walking = true;

    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weapon = _player.GetComponentInChildren<Weapon>();
        _healthbar = GetComponentInChildren<EnemyHealthbar>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _nav.updateRotation = false;
    }

    public void Start()
    {
        _healthbar.Reset(this, attributes.health);
    }

    public void Update()
    {
        RotateTowards(_player.transform);
        // When attack animation is finished, keep moving
        if (!_player_in_range)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName(_walkAnimation) && _is_walking)
            {
                _nav.SetDestination(_player.transform.position);
            }
            else
            {
                _nav.SetDestination(transform.position);
            }
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * attributes.angularSpeed);
    }

    // called by Enemy class
    protected virtual void OnWeaponHit(Vector3 dir, Vector3 pos)
    {
        _healthbar.OnHit(_weapon.damage);
        _weapon.OnHitEnemy(pos);
    }

    // Called by Spell class
    public virtual void OnSpellHit(float damage, float impact, Vector3 dir, Vector3 pos)
    {
        _rb.AddForceAtPosition(dir * impact, pos);
        _healthbar.OnHit(damage);
    }

    public void StartWalking()
    {
        _is_walking = true;
    }

    public void TryToDie()
    {
        if (!_healthbar.IsEmpty()) return;
        Destroy(gameObject);
    }
}