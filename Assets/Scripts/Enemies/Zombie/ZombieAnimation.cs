    using UnityEngine;

public class ZombieAnimation : Enemy
{
    public string walkAnimation = "walk";
    public string attackAnimation = "attack";
    public string rightFallAnimation = "right_fall";
    public string backFallAnimation = "back_fall";
    public string leftFallAnimation = "left_fall";
    public float backFallThreshold = 0.8f;

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
        Vector3 localDir = transform.InverseTransformDirection(dir);
        if (localDir.z < -backFallThreshold)
        {
            _anim.Play(backFallAnimation);
        }
        else if (localDir.x >= 0.0f)
        {
            _anim.Play(rightFallAnimation);
        }
        else
        {
            _anim.Play(leftFallAnimation);
        }
    }
}
