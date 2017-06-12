using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float impact = 1.0f;

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
}
