using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : Attack {

	
	void Start () {
        //gives method of attack as claw
        //also sets claw base damage and range
        atkM = AttackMethod.Claw;
        baseDmg = 10;
        atkRange = 2;
	}
	
	public override void atk()
    {
        //Checks if attack hits and applies damage when applicable
        hitOrMiss(atkRange, baseDmg);
    }
}
