using UnityEngine;

public class Weapon : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Vector3 handOffsetPosition;
    public Vector3 handOffsetRotation;
    public float impact = 1.0f;
    public float damage = 15.0f;
    public float pickupCoolDown = 1.5f;
    public float pickRadius = 0.15f;
    public float threshold_rot = 0.25f;
    public float threshold_pos_y = 0.5f;
    public float throwScaler = 50.0f;
    public float pullBackScaler = 50.0f;
    public float pullBackRoation = 1.0f;

    [HideInInspector]
    public static PlayerController pc;

    protected ParticleSystem _effectParticles;
    protected Transform _hand;
    protected Rigidbody _rb;
    protected bool _bHolding;
    protected float _countDown;
    protected bool _bOnRightHand;
    protected bool _bPullingBack;
    protected float _pcRadius;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _effectParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        _effectParticles.Pause();
        _hand = transform.parent;
        _bHolding = true;
        _bPullingBack = false;
        if (controller == OVRInput.Controller.RTouch ||
            controller == OVRInput.Controller.RTrackedRemote)
            _bOnRightHand = true;
        else
            _bOnRightHand = false;
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
        return transform.rotation.x > -threshold_rot &&
            transform.rotation.x < threshold_rot &&
            transform.rotation.z > -threshold_rot &&
            transform.rotation.z < threshold_rot &&
            transform.localPosition.y > threshold_pos_y;
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
        _bHolding = false;
        transform.parent = null;
        Unfreeze();
        _rb.AddForce(OVRInput.GetLocalControllerVelocity(controller) * throwScaler);
    }

    protected void PickUp()
    {
        Debug.Log("Pick up weapon");
        _bHolding = true;
        _countDown = pickupCoolDown;
        transform.parent = _hand;
        transform.localPosition = handOffsetPosition;
        transform.localEulerAngles = handOffsetRotation;
        Freeze();
    }

    protected float GetHandDistance()
    {
        if (_bOnRightHand)
            return Vector3.Distance(transform.position, pc.OVRCamera.rightHandAnchor.position);
        return Vector3.Distance(transform.position, pc.OVRCamera.leftHandAnchor.position);
    }

    protected Vector3 GetHandDirection()
    {
        if (_bOnRightHand)
            return (- transform.position + pc.OVRCamera.rightHandAnchor.position).normalized;
        return (- transform.position + pc.OVRCamera.leftHandAnchor.position).normalized;
    }

    public void Update()
    {
        // count down
        if (_countDown > 0 && !_bHolding)
        {
            _countDown -= Time.deltaTime;
        }

        // Debug key: 2
        if (Input.GetKeyDown(KeyCode.Alpha2) ||
            OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger, controller))
        {
            if (_bHolding)
            { Drop(); }
            else if (_countDown <= 0)
            {
                _bPullingBack = true;
            }
            if (!_bPullingBack && IsFocusedByEye())
            {
                if (GetHandDistance() <= pickRadius)
                { PickUp(); }
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
            }
        }
    }
}
