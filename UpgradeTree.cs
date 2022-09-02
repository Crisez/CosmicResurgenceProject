using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTree : MonoBehaviour {

    //The Tree UI buttons and their text
    public Button atkBtn;
    public Button spdBtn;
    public Button defBtn;
    public Text atkText;
    public Text spdText;
    public Text defText;

    //UI Text and values for costs
    public Text atkCostText;
    public Text spdCostText;
    public Text defCostText;
    private int atkCost = 1;
    private int spdCost = 1;
    private int defCost = 1;

    //player controller
    private PlayerController playControl;

    //Counts for checking amount of upgrades
    private int atkCount = 0;
    private int spdCount = 0;
    private int defCount = 0;

    private void Awake()
    {
        //Gets playercontroller
        playControl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start () {

        
        //Listens for when the button is clicked
        atkBtn.onClick.AddListener(UpgradeAttack);
        spdBtn.onClick.AddListener(UpgradeSpeed);
        defBtn.onClick.AddListener(UpgradeDefense);
    }

    //upgrades attack based on count to keep track of the current upgrade and its cost
    void UpgradeAttack ()
    {
        if(atkCount == 0 && playControl.PlayerBiomass >= atkCost)
        {
            //upgrades attack damage by number
            playControl.UpAttack(2);

            Debug.Log("Atk Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = atkCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            atkText.text = "Upgrade Damage +3";
           
            //up the cost and update the cost text then increase the count
            atkCost++;
            UpdateAtkCost();
            atkCount++;
        }
        else if (atkCount == 1 && playControl.PlayerBiomass >= atkCost)
        {
            //upgrades attack damage by number
            playControl.UpAttack(3);

            Debug.Log("Atk Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = atkCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            atkText.text = "Upgrade Damage +5";

            //up the cost and update the cost text then increase the count
            atkCost++;
            UpdateAtkCost();
            atkCount++;
        }
        else if (atkCount == 2 && playControl.PlayerBiomass >= atkCost)
        {
            //upgrades attack damage by number
            playControl.UpAttack(5);

            Debug.Log("Atk Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = atkCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            atkText.text = "All Upgrades Obtained";

            //up the cost and update the cost text then increase the count
            atkCost++;
            UpdateAtkCost();
            atkCount++;
        }
              
    }

    //upgrades speed based on count to keep track of the current upgrade and its cost
    void UpgradeSpeed()
    {
        //first upgrade increases speed by 3
        if (spdCount == 0 && playControl.PlayerBiomass >= spdCost)
        {
            //upgrades speed by number
            playControl.UpSpeed(2);
            
            Debug.Log("Spd Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = spdCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            spdText.text = "Upgrade Speed +3";

            //up the cost and update the cost text then increase the count
            spdCost++;
            UpdateSpdCost();
            spdCount++;
        }
        else if (spdCount == 1 && playControl.PlayerBiomass >= spdCost)
        {
            //upgrades speed by number
            playControl.UpSpeed(3);

            Debug.Log("Spd Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = spdCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            spdText.text = "Upgrade Speed +5";

            //up the cost and update the cost text then increase the count
            spdCost++;
            UpdateSpdCost();
            spdCount++;
        }
        else if (spdCount == 2 && playControl.PlayerBiomass >= spdCost)
        {
            //upgrades speed by number
            playControl.UpSpeed(5);

            Debug.Log("Spd Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = spdCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            spdText.text = "All Upgrades Obtained";

            //up the cost and update the cost text then increase the count
            spdCost++;
            UpdateSpdCost();
            spdCount++;
        }
        
    }

    //upgrades defense based on count to keep track of the current upgrade and its cost
    void UpgradeDefense()
    {
        if (defCount == 0 && playControl.PlayerBiomass >= defCost)
        {
            //upgrades armor by number
            playControl.UpDefense(2);

            Debug.Log("Def Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = defCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            defText.text = "Upgrade Armor +3";

            //up the cost and update the cost text then increase the count
            defCost++;
            UpdateDefCost();
            defCount++;
        }
        else if (defCount == 1 && playControl.PlayerBiomass >= defCost)
        {
            //upgrades armor by number
            playControl.UpDefense(3);

            Debug.Log("Def Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = defCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            defText.text = "Upgrade Armor +5";

            //up the cost and update the cost text then increase the count
            defCost++;
            UpdateDefCost();
            defCount++;
        }
        else if (defCount == 2 && playControl.PlayerBiomass >= defCost)
        {
            //upgrades armor by number
            playControl.UpDefense(5);

            Debug.Log("Def Up");

            //take away biomass equivalent to cost set in if and update the text
            playControl.PlayerBiomass = defCost;
            playControl.UpdateBiomassText();

            //update button text to next upgrade
            defText.text = "All Upgrades Obtained";

            //up the cost and update the cost text then increase the count
            defCost++;
            UpdateDefCost();
            defCount++;
        }
               
    }

    //Edits button text to new cost
    private void UpdateAtkCost()
    {
        atkCostText.text = "Cost: " + atkCost;
    }

    //Edits button text to new cost
    private void UpdateSpdCost()
    {
        spdCostText.text = "Cost: " + spdCost;
    }

    //Edits button text to new cost
    private void UpdateDefCost()
    {
        defCostText.text = "Cost: " + defCost;
    }


}
