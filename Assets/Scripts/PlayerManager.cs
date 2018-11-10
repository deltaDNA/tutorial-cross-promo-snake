using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DeltaDNA;

public class PlayerManager : MonoBehaviour {

    // Health, Level & Coins are set in PlayFab
    public int playerLevel = 1 ;
    public int playerHealth = 100 ;
    public int playerCoins = 10 ;

    // Assigned to player from deltaDNA AB Test 
    // initiated from PlayFab with CLoud Script
    public int difficulty = 100; 

    public HudManager hud;


	// Use this for initialization
	void Start () {

        hud = GameObject.FindObjectOfType<HudManager>();
        GetPlayerInventory();   // Contains Virtual Currency Balance
        GetPlayerStatistics();  // Contains Numeric Stats (playerLevel, playerHealth..)
        UpdatePlayerStatistics();
    }




    public void GetPlayerInventory()
    {
    }



    public void GetPlayerStatistics()
    {
    }



    public void UpdatePlayerStatistics()
    { 
        hud.SetCoins(playerCoins);
        hud.SetHealth(playerHealth);
        hud.SetLevel(playerLevel);
        hud.SetDifficulty(difficulty);
    }


    

    
}