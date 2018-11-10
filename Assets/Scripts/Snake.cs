﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour {

    public GameObject tail;
    public GameObject food;
    private Transform rBorder;
    private Transform lBorder;
    private Transform tBorder;
    private Transform bBorder;
    public GameObject tailburst;
    private GameManager gameManager; 
    
    

    private List<GameObject> tailSections = new List<GameObject>();
    private List<GameObject> tailBurstParticles = new List<GameObject>();
    private List<GameObject> foodList = new List<GameObject>();
    private bool vertical = false;
    private bool horizontal = true;
    private bool eat = false;
    private bool dead = false; 
    private float speed = 0.020f;
	Vector2 vector = Vector2.up;
	Vector2 moveVector;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rBorder = GameObject.Find("border-right").transform;
        lBorder = GameObject.Find("border-left").transform;
        tBorder = GameObject.Find("border-top").transform;
        bBorder = GameObject.Find("border-bottom").transform;
        
        InvokeRepeating("Movement", 0.1f, speed);
        SpawnFood(6);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) && horizontal) {
            horizontal = false;
            vertical = true;
            vector = Vector2.right;
        } else if (Input.GetKey(KeyCode.UpArrow) &&  vertical) {
            horizontal = true;
            vertical = false;
            vector = Vector2.up;
        } else if (Input.GetKey(KeyCode.DownArrow) && vertical) {
            horizontal = true;
            vertical = false;
            vector = -Vector2.up;
        } else if (Input.GetKey(KeyCode.LeftArrow) && horizontal) {
            horizontal = false;
            vertical = true;
            vector = -Vector2.right;
        }
        moveVector = vector / 20f;

    }

    void Movement()
    {
        if (!dead)
        {
            Vector3 ta = transform.position;
            if (eat)
            {
                if (speed > 0.002)
                {
                    speed = speed - 0.002f;
                }


                GameObject g = (GameObject)Instantiate(tail, ta, Quaternion.identity);
                tailSections.Insert(0, g);
                Debug.Log(speed);
                eat = false;
            }
            else if (tailSections.Count > 0)
            {
                tailSections.Last().transform.position = ta;
                tailSections.Insert(0, tailSections.Last());
                tailSections.RemoveAt(tailSections.Count - 1);
            }

            transform.Translate(moveVector);//* Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider c)
    {

        if (c.name.StartsWith("food"))
        {
            eat = true;
            EatFood(c.gameObject);
        }
        else if(c.name.StartsWith("border"))
        {
            dead = true;
            Debug.Log("Boom");
            foreach(GameObject t in tailSections)
            {
                GameObject p = (GameObject)Instantiate(tailburst, t.transform.position, Quaternion.identity);
                tailBurstParticles.Add(p);
                Destroy(t); // Destroy tail sections
            }

            StartCoroutine(DeathOnTimer());
            
        }
    }
    private IEnumerator DeathOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            
            CleanUpFood();
            gameManager.PlayerDied();
            Destroy(gameObject);
        }
    }

    private void EatFood(GameObject f)
    {
        foreach (GameObject food in foodList)
        {
            if (food == f)
            {
                foodList.Remove(food);
                Destroy(f);
                Debug.Log("Munch");
                break;
            }
        }

        if (foodList.Count == 0)
        {
            gameManager.LevelUp();
            SpawnFood(6);

        }
        
       
    }
    public void SpawnFood(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int x = (int)Random.Range(lBorder.position.x, rBorder.position.x);
            int y = (int)Random.Range(bBorder.position.y, tBorder.position.y);
            GameObject f = Instantiate(food, new Vector3(x, y, -1), Quaternion.identity);
            foodList.Add(f);
        }
    }

    public void CleanUpFood()
    {
        foreach(GameObject food in foodList)
        {
            Destroy(food);
        }
    }

}
