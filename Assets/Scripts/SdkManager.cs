using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using DeltaDNA;


public class SdkManager : MonoBehaviour {



    private string sharedUserID;
    public GameObject panelQuit;
    public Button bttnStart;
    public Text txtLoading;
    public Text txtStart;
    private bool readyToStart = false;

    // Start Button Size and Color
    private Color sourceColor;
    private Color targetColor;
    private Vector3 InitialScale;
    private Vector3 FinalScale;

    // Use this for initialization
    void Start()
    {
        txtLoading.gameObject.SetActive(true);
        txtStart.gameObject.SetActive(false);

        // These are for pulsing the start button size and alpha 
        InitialScale = transform.localScale;
        FinalScale = new Vector3(InitialScale.x + 0.04f,
                                 InitialScale.y + 0.04f,
                                 InitialScale.z);
        sourceColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        targetColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        DdnaPlayerConfig();
        // Setup some DeltaDNA config stuff
        //DDNA.Instance.SetLoggingLevel(DeltaDNA.Logger.Level.DEBUG);


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

    
    private void ShowQuitPanel()
    {
        panelQuit.SetActive(true);
    }

    private void DdnaPlayerConfig()
    {
        // Use deviceID in this simple cross promo example. 
        sharedUserID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log("Shared userID (deviceID) = " + sharedUserID);

        txtLoading.gameObject.SetActive(false);
        txtStart.gameObject.SetActive(true);
        
        bttnStart.gameObject.SetActive(true);
        readyToStart = true;

    }


    public void ButtonStart_Click()
    {
        // Move to Main scene now that DDNA and PlayFab SDKs are running
        // and player config AB Tests have been checked.
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
