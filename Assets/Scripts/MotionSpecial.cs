using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSpecial : MonoBehaviour
{

    // Katelynn's AOE Special V3

    public float damage;
    public float rayLength;
    public float initialCooldown;
    public float cooldownTime;
    public float rangeOfEffect;
    public GameObject centerEyeAnchor;

    private float timeStamp;
    private RaycastHit vision;

    private void Start()
    {
        timeStamp = initialCooldown;
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out vision, rayLength))
        {
            if (vision.collider.tag == "Weapon")
            {
                if (this.transform.rotation.x > -0.25 && this.transform.rotation.x < 0.25) //check if holding sword vertically
                {
                    if (this.transform.rotation.z > -0.25 && this.transform.rotation.z < 0.25)
                    {
                        if (this.transform.position.y > centerEyeAnchor.transform.position.y)
                        {
                            if (timeStamp <= Time.time) //cooldown over
                            {
                                timeStamp = Time.time + cooldownTime;
                                Debug.Log("AOE Special Activated");
                                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                                foreach (GameObject enemy in enemies)
                                {
                                    if (enemy.transform.parent.parent == null) //only root objects
                                    {
                                        Debug.Log("Affected enemy: " + enemy.gameObject.name);
                                        float dist = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, enemy.transform.position);
                                        if (dist < rangeOfEffect) //kill all enemies within range
                                        {
                                            try
                                            {
                                                enemy.GetComponentInChildren<EnemyHealthbar>().OnWeaponHit(damage);
                                                enemy.GetComponent<Enemy>().OnSpecial();
                                                GetComponent<ParticleSystem>().Play();  
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
