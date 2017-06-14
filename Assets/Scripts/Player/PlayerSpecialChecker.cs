using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialChecker : MonoBehaviour
{
    /* Katelynn's AOE Special
     * 
     * SETUP: 
     * create an invisible 3d object
     * attach to main hand anchor
     * set rotation to 90,0,0
     * add this script as component
     * 
     * Skill works by holding your blade aloft and pressing the trigger.
     * It's bound to an extra 3d object because my sword wasn't at a starting rotation of 90,0,0 but if yours is, 
     * this can be attached to the weapon directly.
     */
     
    //public float initialCooldown;
    //public float cooldownTime;
    //public float rangeOfEffect;

    //private float timeStamp;

    //private void Start()
    //{
    //    timeStamp = initialCooldown;
    //}

    void Update()
    {
        //if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) //&& !cooldown
        //{
        //    if (this.transform.rotation.x > -0.25 && this.transform.rotation.x < 0.25) //check if holding sword vertically
        //    {
        //        if (this.transform.rotation.z > -0.25 && this.transform.rotation.z < 0.25)
        //        {
        //            if (timeStamp <= Time.time)
        //            {
        //                timeStamp = Time.time + cooldownTime; 
        //                Debug.Log("AOE Special Activated");
        //                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //                foreach (GameObject enemy in enemies)
        //                {
        //                    if (enemy.transform.parent == null) //only root objects
        //                    {
        //                        float dist = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, enemy.transform.position);
        //                        if (dist < rangeOfEffect) //kill all enemies within range
        //                        {
        //                            GameManager.score += 100;

        //                            try
        //                            {
        //                                enemy.gameObject.GetComponentInParent<NPCLife>().kill();
        //                            }
        //                            catch { }
        //                        }
        //                    }
        //                }
        //            }else Debug.Log((int)(timeStamp - Time.time) + "s until reuse.");
        //        }
        //        else Debug.Log("Wrong Position");
        //    }
        //    else Debug.Log("Wrong Position");
        //}
    }
}
