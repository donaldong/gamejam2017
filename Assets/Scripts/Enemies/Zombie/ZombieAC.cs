    using UnityEngine;

public class ZombieAC : Enemy {
    public string walkAnimation = "walk";
    public string attackAnimation = "attack";
    public string rightFallAnimation = "right_fall";
    public string backFallAnimation = "back_fall";
    public string leftFallAnimation = "left_fall";
    public float backFallThreshold = 0.8f;
    public float hitStunSecond = 1.0f ;

    private float _stun_elapsed = 0.0f;

    private void FixedUpdate()
    {
        if (!_is_walking)
        {
            _nav.SetDestination(transform.position);
            _stun_elapsed += Time.deltaTime;
            if (_stun_elapsed >= hitStunSecond)
            {
                _stun_elapsed = 0.0f;
                _is_walking = true;
                Debug.Log("Move!");
            }
        }
    }

    protected override void OnWeaponHit(Vector3 dir, Vector3 pos)
    {
        base.OnWeaponHit(dir, pos);
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
