using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //Create the different states
    public enum State { Idle, Chase, Shoot };
    private State currentState;

    //distance variables
    private float playerDist = 0;
    private float chaseDist = 60f;
    private float shootDist = 20f;

    //player gameobject and enemy transform
    private GameObject player;
    private Transform enemyTransform;
    private CharacterController enemyCharControl;

    //game object for projectile and fire rate
    public GameObject projectile;
    private float shootingRate = 1f;

    //enemy movement for applying gravity or stopping
    private Vector3 drop;
    private float grav = -8;

    //health   
    private int health;


	// Use this for initialization
	void Start () {

        //Gets the player for player distance later
        player = GameObject.FindGameObjectWithTag("Player");

        //gets the character controller of the enemy that we will move
        enemyCharControl = GetComponent<CharacterController>();

        //sets health and current state
        health = 100;
        currentState = State.Idle;

        //gets the transform of the enemy so that distance can be given on the position
        enemyTransform = transform;

        //calls method to swap states for the AI
        SwitchStates(State.Idle);
	}
	
    //Method for taking damage
	public void takeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log("Health Remaining " + health);
        //upon loosing all health the enemy is destroyed.
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Shoots a projectile
    private void shoot()
    {
        //should create the bullet facing the direction the enemy is facing, not super important since bullet is just circle now but can be tested later with actual assets.
        GameObject bullet = Instantiate(projectile, enemyTransform.position, enemyTransform.rotation * projectile.transform.rotation);
        //sends bullet forward
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
    }

    private void Update()
    {
        //if the enemy is not on the ground for any reason, apply gravity
        if (!enemyCharControl.isGrounded)
        {
            //movement in this case should be limited to avoid AI moving in air to make it more fair for players
            drop = new Vector3(0f, grav, 0f);
            enemyCharControl.Move(drop * Time.deltaTime);
        }
    }

    private void SwitchStates(State changedState)
    {
        //stops all on going coroutines and sets the current state to the specified state
        StopAllCoroutines();
        currentState = changedState;

        //Switch statement to "switch" the state.
        switch (currentState)
        {
            case State.Idle:
                
                StartCoroutine(EnemyIdle());
                break;
            case State.Chase:
               
                StartCoroutine(EnemyChase());
                break;
            case State.Shoot:
                
                StartCoroutine(EnemyShoot());
                break;
        }
    }

    //Enemies should just guard areas so their default state is to just idle
    
    IEnumerator EnemyIdle()
    {
        while (true)
        {

            //Potentially expand to return to starting position in next iteration

            //checks to see if the enemy's distance from the player is shorter than the enemy's chase distance
            if (Vector3.Distance(enemyTransform.position, player.transform.position) <= chaseDist)
            {
                //goes to chase the player
                SwitchStates(State.Chase);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator EnemyChase()
    {
       
        while (true)
        {
            //Face the player and move toward them
            transform.LookAt(player.transform.position);
            enemyCharControl.Move(transform.forward * 5 * Time.deltaTime);

            //set player distance again
            playerDist = Vector3.Distance(enemyTransform.position, player.transform.position);

            //if the player is within the shooting distance prepare to shoot
            if (playerDist <= shootDist)
            {
                SwitchStates(State.Shoot);
                yield break;
            }
            else if (playerDist > chaseDist) //if the player has gotten out of range, will give up on pursuit
            {
                SwitchStates(State.Idle);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            //face the player
            transform.LookAt(player.transform.position);

            //set player distance again
            playerDist = Vector3.Distance(enemyTransform.position, player.transform.position);

            //if the enemy is within a certain distance, stop chasing in order to keep distance
            if(playerDist <= 10f)
            {
                enemyCharControl.Move(drop * Time.deltaTime);
                
            }
            else //chase the player
            {
                enemyCharControl.Move(transform.forward * 5 * Time.deltaTime);
            }

            if (playerDist > shootDist) //if the player is out of range of attack, go back to pursuing them
            {
                SwitchStates(State.Chase);
                yield break;
            }

            //shoot projectiles on an interval
            if (shootingRate >= 3)
            {
                shoot();
                shootingRate = 0;
            }

            shootingRate += (1 * Time.deltaTime);

            yield return null;
        }
    }
}
