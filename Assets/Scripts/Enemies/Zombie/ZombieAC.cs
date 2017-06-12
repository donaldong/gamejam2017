using UnityEngine;

public class ZombieAC : Enemy {
    public string walkAnimation = "walk";
    public string attackAnimation = "attack";
    public string rightFallAnimation = "right_fall";
    public string backFallAnimation = "back_fall";
    public string leftFallAnimation = "left_fall";
    public float backFallThreshold = 0.8f;

    protected override void OnWeaponHit(Vector3 dir, Vector3 pos)
    {
        base.OnWeaponHit(dir, pos);
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
