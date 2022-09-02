using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //set up all attacks and get the player's transform
	public enum AttackMethod { Claw };
    public Transform playerTransform;

    //player controller;
    private PlayerController playControl;

    //allows the attacks to inherit the variables for what type, base damage, and attack range
    protected AttackMethod atkM;
    protected int baseDmg;
    protected int atkRange;

    private void Awake()
    {
        //Gets playercontroller
        playControl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //creates a method that will be overridden in the attack's class with the hitOrMiss method being called
    public virtual void atk()
    {

    }

    //this method allows the attack to see if it has actually hit and what damage it will deal.
    public void hitOrMiss(int weaponRange, int dmg)
    {
        //The raycast sees if the attack is within range and if it will hit
        RaycastHit strike;
        //draw the attack range as a red line for now. Plan to replace with animation or attack sprite (Like fangs appear in front of player signifying attack). 
        Debug.DrawRay(playerTransform.position, playerTransform.TransformDirection(Vector3.forward) * weaponRange, Color.red, 2f);
        
        //if the ray cast hits check to see if what is hit is an enemy
        if (Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.forward), out strike, weaponRange))
        {
            
            GameObject colHit = strike.collider.gameObject;
            if (colHit.CompareTag("enemy"))
            {

                //dmgs the enemy by the base damage of the attack + upgrades to attack
                colHit.GetComponent<EnemyController>().takeDamage(dmg + playControl.PlayerDamage);
            }        
            
        }
    }

    
}
