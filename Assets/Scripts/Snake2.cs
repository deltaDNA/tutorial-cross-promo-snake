using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake2 : MonoBehaviour {

    public GameObject tail;
    private Transform rBorder;
    private Transform lBorder;
    private Transform tBorder;
    private Transform bBorder;
    private List<GameObject> tailSections = new List<GameObject>();
    bool vertical = false;
    bool horizontal = true;
    bool eat = false;

    private float speed = 0.020f;
	Vector2 vector = Vector2.up;
	Vector2 moveVector;

    // Use this for initialization
    void Start () {

        rBorder = GameObject.Find("border-right").transform;
        lBorder = GameObject.Find("border-left").transform;
        tBorder = GameObject.Find("border-top").transform;
        bBorder = GameObject.Find("border-bottom").transform;

        InvokeRepeating("Movement", 0.1f, speed);
        
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

        Vector3 ta = transform.position;
        if (eat)
        {
            if (speed > 0.002){
                speed = speed - 0.002f;
            }


           GameObject g = (GameObject)Instantiate(tail, ta, Quaternion.identity);
            tailSections.Insert(0, g);
            Debug.Log(speed);
            eat = false;
        }
        else if (tailSections.Count > 0) {
            tailSections.Last().transform.position = ta;
            tailSections.Insert(0, tailSections.Last());
            tailSections.RemoveAt(tailSections.Count - 1);
        }

        transform.Translate(moveVector);//* Time.deltaTime);
    }
    void OnTriggerEnter(Collider c)
    {

        if (c.name.StartsWith("food"))
        {
            eat = true;
            Destroy(c.gameObject);
            Debug.Log("Munch");
        }
        else if(c.name.StartsWith("border"))
        {
            Debug.Log("Boom");
            Destroy(gameObject);
        }
    }

}
