using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSpecial : MonoBehaviour
{

    // Katelynn's AOE Specials V3

    //aoe special vars
    public float damage; 
    public float initialCooldown;
    public float cooldownTime;
    public float rangeOfEffect;
    public GameObject centerEyeAnchor; //to detect height of eye level
    private float timeStamp;

    //thrown vars
    public GameObject weapon;
    public GameObject weaponDropped;
    public OVRInput.Controller controller;
    private bool held;

    //shared vars
    private RaycastHit vision;

    private void Start()
    {
        timeStamp = initialCooldown;
        held = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //sky strike
        if (held && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out vision, 10))
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
                                Debug.Log("[Sky Strike] Special Activated, Cooldown: " + cooldownTime + "s");
                                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                                var enemyCount = 0;
                                foreach (GameObject enemy in enemies)
                                {
                                    if (enemy.transform.parent.parent == null) //only root objects
                                    {
                                        enemyCount++;
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
                                Debug.Log("[Sky Strike] Affected " + enemyCount + " enemies.");
                            }
                        }
                    }
                }
            }
        }

        //drop, pick up, and throw
        if ((OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch)))
        {
            if (held)
            {
                GameObject newWeapon = Instantiate(weapon, new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z), Quaternion.identity);
                newWeapon.transform.rotation = weapon.transform.rotation;
                newWeapon.AddComponent<Rigidbody>();
                newWeapon.GetComponent<Rigidbody>().AddForce(OVRInput.GetLocalControllerVelocity(controller)*500);
                Debug.Log(OVRInput.GetLocalControllerAngularVelocity(controller));

                newWeapon.tag = "Weapon";
                weaponDropped = newWeapon;
                weapon.SetActive(false);
                held = false;
            }
            else
            {
                //if (vision.collider.tag == "Weapon")
                //{
                    Destroy(weaponDropped);
                    weapon.SetActive(true);
                    held = true;
                /*}
                else
                {
                    Debug.Log("[Pick Up] Not Looking at Weapon");
                }*/
            }
        }
    }
}
