using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float impact = 1.0f;
    public float damage = 15.0f;
    public GameObject weapon;
    public bool held = true;

    protected ParticleSystem _effectParticles;

    public void Awake()
    {
        _effectParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        _effectParticles.Pause();
    }

    public void OnHitEnemy(Vector3 pos)
    {
        _effectParticles.transform.position = pos;
        _effectParticles.Stop();
        _effectParticles.Play();
    }

    void Update()
    {
        if ((OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch)  || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch)) && held)
        {
            GameObject droppedWeapon = Instantiate(weapon, new Vector3(transform.position.x, transform.position.y-2.0f, transform.position.z), Quaternion.identity);
            droppedWeapon.gameObject.tag = "Weapon";
            droppedWeapon.gameObject.GetComponent<DroppedWeapon>().weapon = gameObject;
            gameObject.SetActive(false);
        }
    }
}
