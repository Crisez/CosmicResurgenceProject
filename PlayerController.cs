using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
       
    //character controller and gamemanager
    private CharacterController playCB;
    private GameManager manage;

    //is the player in the air
    private bool inTheAir;

    //camera and horizontal+vertical movement
    private Vector3 camOffset;
    private Vector3 move;
    private Vector3 fall;

    //gravity and speed
    private float grav = -8;
    private float spd = 10;
    private float turnSpd = 40;

    //attack, armor, health, and biomass value
    private int playDmg = 0;
    private int playArmor = 0;
    private int bioM = 0;
    private int hp = 100;

    //invincibility time
    private float inv = 0;

    //biomass and health text
    public Text bioText;
    public Text hpText;

    //the players transform
    public Transform pb;

    //Gets all attacks in a list 
    public List<Attack> attackStyle = new List<Attack>();

	// Use this for initialization
	void Start () {

        //getcomponents of gamemanager and character controller
        manage = GetComponent<GameManager>();
        playCB = GetComponent<CharacterController>();

        //set the distance of the camera from the player
        camOffset = Camera.main.transform.position - transform.position;

       
    }
	
    //allows other classes to acquire the player's upgraded damage
    public int PlayerDamage
    {
        get { return playDmg; }
    }

    //allows other classes to get the collected biomass and alter the amount of biomass collected
    public int PlayerBiomass
    {
        get { return bioM; }
        set
        {
            bioM -= value;
        }
    }

	void Update () {

        //------------------------//
                //Movement//
        //------------------------//

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //URL Citation for Movement code alterations  https://www.youtube.com/watch?v=_QajrabyTJc

        //face right
        if (Input.GetKey(KeyCode.Q))
        {
            //rotates player
            pb.Rotate(Vector3.up * (-1 * Time.deltaTime * turnSpd));

            //camera follows rotation
            //URL citation for camera following the rotation  https://answers.unity.com/questions/600577/camera-rotation-around-player-while-following.html
            camOffset = Quaternion.AngleAxis((-1 * Time.deltaTime * turnSpd), Vector3.up) * camOffset;

            //camera rotates
            Camera.main.transform.Rotate(Vector3.up * (-1 * Time.deltaTime * turnSpd));
        }

       //face left
        if (Input.GetKey(KeyCode.E))
        {
            //rotates player
            pb.Rotate(Vector3.up * (1 * Time.deltaTime * turnSpd));

            //camera follows rotation
            //URL citation for camera following the rotation  https://answers.unity.com/questions/600577/camera-rotation-around-player-while-following.html
            camOffset = Quaternion.AngleAxis((1 * Time.deltaTime * turnSpd), Vector3.up) * camOffset;
            
            //camera rotates
            Camera.main.transform.Rotate(Vector3.up * (1 * Time.deltaTime * turnSpd));
        }

        //set move relative to the direction the player is facing
        move = (transform.right * xInput) + (transform.forward * zInput);

        //apply movement
        playCB.Move(move * Time.deltaTime * spd);

        //fall at the rate of the set gravity
        fall.y += grav * Time.deltaTime;

        //apply vertical movement
        playCB.Move(fall * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !inTheAir)
        {
            //jump up by this value
            fall.y = 5f;
            //be in the air
            inTheAir = true;
        }

        //if the player is on the ground, then they are no longer in the air
        if (playCB.isGrounded)
        {
            inTheAir = false;
        }

        //------------------------//
                //Attacks//
        //------------------------//

        //when right clicking, uses first attack
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            attackStyle[0].atk();
        }

        //decreases invulnerability time
        inv -= (1 * Time.deltaTime);

        //pressing T restarts the game
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.gameInstance.ResetGame();
        }
    }

    //remove health from the player
    public void damagePlayer(int amount)
    {
        //if the player has armor, divide the incoming damage by the armor
        if (playArmor > 0)
        {
            hp -= (amount / playArmor);
        }
        else //do it as normal to avoid dividing by zero
        {
            hp -= amount;
        }
        //if all health is gone restart the game.
        if(hp <= 0)
        {
            GameManager.gameInstance.ResetGame();
        }
        //set the health text
        hpText.text = "Health: " + hp + "/100";
    }

    public void OnTriggerEnter(Collider other)
    {
        //if the player hits a bullet and is no longer invincible, the player will take damage and gain invulnerability time
        if (other.gameObject.CompareTag("bullet") && inv <= 0)
        {
            damagePlayer(20);
            inv = 2;
        }
        else if (other.CompareTag("biomass")) //if the player found biomass, then increase biomass collected, update the text, and destroy the biomass.
        {
            bioM++;
            UpdateBiomassText();
            Destroy(other.gameObject);
        } 
    }

    //upgrades damage by an amount
    public void UpAttack(int inc)
    {
        playDmg += inc;
    }

    //upgrades speed by an amount
    public void UpSpeed(int inc)
    {
        spd += inc;
    }

    //upgrades armor by an amount
    public void UpDefense(int inc)
    {
        playArmor += inc;
    }

    //updates the text for biomass
    public void UpdateBiomassText()
    {
        bioText.text = "BioMass: " + bioM;
    }

    void LateUpdate()
    {
        //camera follows the player.
        Camera.main.transform.position = transform.position + camOffset;
    }
    

}
