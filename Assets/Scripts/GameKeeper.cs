using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeeper : MonoBehaviour {

    public int killCount;
    public GameObject menu;
    public OVRInput.Controller controller;
    private float score;
    private bool activeMenu;

	// Use this for initialization
	void Start () {
        killCount = 0;
        activeMenu = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetUp(OVRInput.Button.Two, controller))
        {
            activeMenu = !activeMenu;
            menu.SetActive(activeMenu);
        }
        menu.GetComponent<TextMesh>().text = "MENU"
            + "\nKills: " + killCount + " | Score: " + score
            + "\nControls:"
            + "\nHand Trigger Drops/Summons Sword"
            + "\nIndex Trigger Casts Spells"
            + "\n\nB to Open/Exit Menu";
    }

    public void registerDeath(float health)
    {
        killCount++;
        score += health;
    }
}
