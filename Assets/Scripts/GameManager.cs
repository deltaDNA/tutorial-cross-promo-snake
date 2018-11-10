using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DeltaDNA;


public class GameManager : MonoBehaviour {

    private string sharedUserID;
    public PlayerManager player;
    //public HudManager hud;
    public GameObject snake;

    public Text txtStart;
    public Text txtGameOver;
    public Button bttnStart;


    // Start Button Size and Color
    private Color sourceColor;
    private Color targetColor;
    private Vector3 InitialScale;
    private Vector3 FinalScale;
    bool readyToStart = false; 

    private void Start()
    {
        // These are for pulsing the start button size and alpha 
        InitialScale = transform.localScale;
        FinalScale = new Vector3(InitialScale.x + 0.04f,
                                 InitialScale.y + 0.04f,
                                 InitialScale.z);
        sourceColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        targetColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //Vector3 pos = new Vector3(0, 0, -1);
        //Instantiate(player, pos, Quaternion.identity);

        DdnaPlayerConfig();

        txtStart.gameObject.SetActive(true);
        bttnStart.gameObject.SetActive(true);
        readyToStart = true;
    }

    private void Update()
    {
        if (readyToStart)
        {
            // Pulse the start button size and alpha
            bttnStart.image.color = Color.Lerp(sourceColor, targetColor, Mathf.PingPong(Time.time, 1.2f));
            bttnStart.transform.localScale = Vector3.Lerp(InitialScale, FinalScale, Mathf.PingPong(Time.time, 1.2f));

        }
    }
    private void DdnaPlayerConfig()
    {
        // Use deviceID in this simple cross promo example. 
        sharedUserID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log("Shared userID (deviceID) = " + sharedUserID);



    }
    public void StartLevel(int levelNo)
    {
        // Player starts level
        txtGameOver.gameObject.SetActive(false);
        txtStart.gameObject.SetActive(false);
        bttnStart.gameObject.SetActive(false);
        readyToStart = false; 

        // Record Mission Started 

        // Spawn new Snake 
        Vector3 pos = new Vector3(0, 0, -1);
        Instantiate(snake, pos, Quaternion.identity);

        
    }
    public void PlayerDied()
    {
        txtGameOver.gameObject.SetActive(true);
        txtStart.gameObject.SetActive(true);
        bttnStart.gameObject.SetActive(true);
        readyToStart = true; 

    }
    public void LevelUp()
    {
        player.playerLevel++;

        Debug.Log("Level Up - playerLevel " + player.playerLevel);

        player.UpdatePlayerStatistics();
    }


}
