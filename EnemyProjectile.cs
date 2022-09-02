using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	void Start () {
        //despawns after 5 seconds
        Destroy(this.gameObject, 5);
	}
	
    //if the bullet collides with anything that is not an enemy, it is destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
    }
   
}
