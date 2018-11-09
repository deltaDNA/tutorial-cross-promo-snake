using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DeltaDNA;


public class GameManager : MonoBehaviour {


    public PlayerManager player;
    public HudManager hud;
    public GameObject snake;
    public GameObject food;
    private Transform rBorder;
    private Transform lBorder;
    private Transform tBorder;
    private Transform bBorder;

    private void Start()
    {
        rBorder = GameObject.Find("border-right").transform;
        lBorder = GameObject.Find("border-left").transform;
        tBorder = GameObject.Find("border-top").transform;
        bBorder = GameObject.Find("border-bottom").transform;

        Vector3 pos = new Vector3(0, 0, -1);
        Instantiate(player, pos, Quaternion.identity);
    }

    public void StartLevel(int levelNo)
    {
        // Player starts level

        // Record Mission Started 

        // Spawn new Snake 
        Vector3 pos = new Vector3(0, 0, -1);
        Instantiate(snake, pos, Quaternion.identity);

        SpawnFood(6);
    }

    public void LevelUp()
    {
        player.playerLevel++;

        Debug.Log("Level Up - playerLevel " + player.playerLevel);

        player.UpdatePlayerStatistics();
    }
    public void SpawnFood(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int x = (int)Random.Range(lBorder.position.x, rBorder.position.x);
            int y = (int)Random.Range(bBorder.position.y, tBorder.position.y);
            Instantiate(food, new Vector3(x, y,-1), Quaternion.identity);
            Debug.Log("Burp!");
        }

    }
    void OnTriggerEnter(Collider c)
    {

        if (c.name.StartsWith("food"))
        {
            //eat = true;
            Destroy(c.gameObject);
            Debug.Log("Munch");
        }
    }
}
