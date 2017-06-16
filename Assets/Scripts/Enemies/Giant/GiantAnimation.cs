using UnityEngine;

public class GiantAnimation : Enemy
{

    public string walkAnimation;
    public string attackAnimation;
    public string fallAnimation;
    public string stunAnimation;
    public string deathAnimation;
    protected bool _isDead;

    protected new void Awake()
    {
        base.Awake();
        _attackAnimation = attackAnimation;
        _isDead = false;
    }

    protected override void OnWeaponHit(Vector3 dir, Vector3 pos)
    {
        base.OnWeaponHit(dir, pos);
        _play(dir);
    }

    public override void OnSpellHit(float damage, float impact, Vector3 dir, Vector3 pos)
    {
        base.OnSpellHit(damage, impact, dir, pos);
        _play(dir);
    }

    protected void _play(Vector3 dir)
    {
        _is_walking = false;
        _anim.Play(fallAnimation);
    }

    protected new void Update()
    {
        base.Update();

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName(_walkAnimation) && _isDead)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _anim.Play(stunAnimation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _anim.Play(attackAnimation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _anim.Play(fallAnimation);
        }
    }

    // Called by Animation Event
    public new void TryToDie()
    {
        if (!_healthbar.IsEmpty()) return;
        pc.countEnemyKill++;
        _needRotate = false;
        _anim.Play(deathAnimation);
        _isDead = true;
    }
}