using UnityEngine;

public class GiantAnimation : Enemy
{

    public string walkAnimation;
    public string attackAnimation;
    public string fallAnimation;

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
}