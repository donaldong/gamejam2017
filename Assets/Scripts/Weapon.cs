using UnityEngine;

public class Weapon : MonoBehaviour
{
    // right hand only
    public bool onRightHand;
    public Vector3 handOffsetPosition;
    public Vector3 handOffsetRotation;
    public float impact = 1.0f;
    public float damage = 15.0f;
    public float pickupCoolDown = 1.5f;
    public float gcd = 0.3f;
    public float pickRadius = 0.15f;
    public float threshold_rot = 0.25f;
    public float threshold_pos_y = 0.5f;
    public float throwScaler = 50.0f;
    public GameObject AOE_Effects;
    public float pullBackScaler = 50.0f;
    public float pullBackRoation = 1.0f;

    [HideInInspector]
    public static PlayerController pc;

    protected ParticleSystem _effectParticles;
    protected Transform _hand;
    protected Rigidbody _rb;
    protected bool _bHolding;
    protected float _countDown;
    protected float _gcd;
    protected bool _bPullingBack;
    protected float _pcRadius;
    protected GameObject _aoeInstance;
    protected ParticleSystem _effectAoe;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _effectParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        _effectParticles.Pause();
        _hand = transform.parent;
        _bHolding = true;
        _bPullingBack = false;
        _aoeInstance = Instantiate(AOE_Effects, transform);
        _aoeInstance.transform.localPosition = new Vector3(0, 0.3f, 0);
        _effectAoe = _aoeInstance.GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        var c = pc.gameObject.GetComponent<CharacterController>();
        _pcRadius = c.height > c.radius ? c.height : c.radius;
        _pcRadius = _pcRadius > pickRadius ? _pcRadius : pickRadius;
    }

    public void OnHitEnemy(Vector3 pos)
    {
        _effectParticles.transform.position = pos;
        _effectParticles.Stop();
        _effectParticles.Play();
    }

    // Called by some spells
    public bool IsHoldingUp()
    {
        float r_x, p_y;
        if (onRightHand)
        {
            r_x = pc.OVRCamera.rightHandAnchor.localEulerAngles.x;
            p_y = pc.OVRCamera.rightHandAnchor.localPosition.y;
        }
        else
        {
            r_x = pc.OVRCamera.leftHandAnchor.localEulerAngles.x;
            p_y = pc.OVRCamera.leftHandAnchor.localPosition.y;
        }
        if (r_x < 0) r_x += 360.0f;
        return r_x < 330.0f + threshold_rot && r_x > 330.0f - threshold_rot && p_y > threshold_pos_y;
    }

    public void PlayAoeEffect()
    {
        _effectParticles.Stop();
        _effectAoe.Play();
    }

    public bool IsFocusedByEye()
    {
        return pc.bEyeHit && (pc.eyeHit.collider.gameObject == gameObject);
    }

    protected void Freeze()
    {
        _rb.isKinematic = true;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    protected void Unfreeze()
    {
        _rb.isKinematic = false;
        _rb.constraints = RigidbodyConstraints.None;
    }

    protected void Drop()
    {
        Debug.Log("Drop weapon");
        _gcd = gcd;
        _bHolding = false;
        transform.parent = null;
        Unfreeze();
        _rb.AddForce(GetControllerVelocity() * throwScaler);
        pc.countWeaponThrow++;
    }

    protected void PickUp()
    {
        Debug.Log("Pick up weapon");
        _gcd = gcd;
        _bHolding = true;
        _countDown = pickupCoolDown;
        transform.parent = _hand;
        transform.localPosition = handOffsetPosition;
        transform.localEulerAngles = handOffsetRotation;
        Freeze();
    }

    protected float GetHandDistance()
    {
        if (onRightHand)
            return Vector3.Distance(transform.position, pc.OVRCamera.rightHandAnchor.position);
        return Vector3.Distance(transform.position, pc.OVRCamera.leftHandAnchor.position);
    }

    protected Vector3 GetHandDirection()
    {
        if (onRightHand)
            return (- transform.position + pc.OVRCamera.rightHandAnchor.position).normalized;
        return (- transform.position + pc.OVRCamera.leftHandAnchor.position).normalized;
    }

    protected Vector3 GetControllerVelocity()
    {
        if (onRightHand)
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    }

    public void Update()
    {
        if (_gcd > 0)
        {
            _gcd -= Time.deltaTime;
        }

        if (_gcd > 0) return;

        // count down
        if (_countDown > 0 && !_bHolding)
        {
            _countDown -= Time.deltaTime;
        }

        // Debug key: 2
        if (Input.GetKeyDown(KeyCode.Alpha2) ||
            OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (_bHolding)
            {
                Drop();
                return;
            }
            else if (_countDown <= 0)
            {
                _bPullingBack = true;
            }
        }

        if(_bPullingBack)
        {
            if (GetHandDistance() > _pcRadius)
            {
                Vector3 dir = GetHandDirection();
                _rb.velocity = dir * pullBackScaler;
                _rb.AddTorque(new Vector3(0, 0, pullBackRoation));
            }
            else
            {
                _bPullingBack = false;
                PickUp();
            }
        }
    }
}
