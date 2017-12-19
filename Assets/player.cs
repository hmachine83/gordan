using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {
    
    //pomagala
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;

    //promjenljive
    private Rigidbody rb;
    public Vector3 destination = new Vector3(0, 0, 0);
    public float speed = 1.0f;
    private Vector3 startPos;
    private Quaternion startRot;
    private Animator anim;
    private bool moving = false;
    public bool akcijaB = false;
    private int strana = 1 ; // 1:istok   2:jug   3:zapad  4:sjever
    private GameObject colid;
    private bool portaj = true;


    // Use this for initialization
    void Start () {
        b1.onClick.AddListener(TaskOnClick1);
        b2.onClick.AddListener(TaskOnClick2);
        b3.onClick.AddListener(TaskOnClick3);
        b4.onClick.AddListener(TaskOnClick4);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void TaskOnClick1()
    {
        moveForward();
    }
    void TaskOnClick2()
    {
        turnRight();
    }
    void TaskOnClick3()
    {
        turnLeft();
    }
    void TaskOnClick4()
    {

            akcija();
    }

    // Update is called once per frame
    void Update () {
       
    }

    void FixedUpdate()
    {
        if (moving)
        {
            Vector3 movePosition = transform.position;
            switch (strana)
            {
                case 1:
                    movePosition.x = Mathf.MoveTowards(transform.position.x, destination.x, speed * Time.deltaTime);
                    break;
                case 2:
                    movePosition.z = Mathf.MoveTowards(transform.position.z, destination.z, speed * Time.deltaTime);
                    break;
                case 3:
                    movePosition.x = Mathf.MoveTowards(transform.position.x, destination.x, speed * Time.deltaTime);
                    break;
                case 4:
                    movePosition.z = Mathf.MoveTowards(transform.position.z, destination.z, speed * Time.deltaTime);
                    break;
            }
            rb.MovePosition(movePosition);
            if ((System.Math.Round(rb.position.z, 2) == destination.z) && (System.Math.Round(rb.position.x, 2) == destination.x))
            {
                anim.Play("blank");
                moving = false;
            }
        }
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(startPos.x, startPos.y + 2, startPos.z);
            transform.rotation = startRot;
            destination = new Vector3(0, 0, 0);
            strana = 1;
        }
    }

    public void moveForward()
    {
        if (!moving)
        {
            switch (strana)
            {
                case 1: // istok
                    destination = new Vector3(destination.x + 1, destination.y, destination.z);
                    break;
                case 2: // jug
                    destination = new Vector3(destination.x, destination.y, destination.z - 1);
                    break;
                case 3: // zapad
                    destination = new Vector3(destination.x - 1, destination.y, destination.z);
                    break;
                case 4: //sjever
                    destination = new Vector3(destination.x, destination.y, destination.z + 1);
                    break;
            }
            anim.Play("anim1");
            moving = true;
        }
    }

    public void turnLeft()
    {
        if (!moving)
        {
            if (strana == 1) strana = 4;
            else strana--;
            transform.Rotate(new Vector3(0, -90, 0));
        }
    }
    public void turnRight()
    {
        if (!moving)
        {
            if (strana == 4) strana = 1;
            else strana++;
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void akcija()
    {
        if (akcijaB)
        {
            anim.Play("press");
            OpenTheDoor();
        }
    }

    public void Test()
    {
        // Debug.Log("zivko");
        // Application.ExternalEval("alert('Test por')");
    }
    void OnCollisionEnter(Collision colll)
    {
        if (colll.collider.gameObject.tag == "zid")
        {
            switch (strana)
            {
                case 1: // istok
                    destination = new Vector3(destination.x - 1, destination.y, destination.z);
                    break;
                case 2: // jug
                    destination = new Vector3(destination.x, destination.y, destination.z + 1);
                    break;
                case 3: // zapad
                    destination = new Vector3(destination.x + 1, destination.y, destination.z);
                    break;
                case 4: //sjever
                    destination = new Vector3(destination.x, destination.y, destination.z - 1);
                    break;
            }
        }

        if (colll.collider.gameObject.tag == "akcija")
        {
            akcijaB = true;         
        }

        if(colll.collider.gameObject.tag == "portal")
        {
            if (portaj)
            {
                colll.collider.gameObject.GetComponent<portal>().ajde();
                portaj = false;
            }
        }


    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.collider.gameObject.tag == "akcija")
        {
            akcijaB = false;
            Debug.Log(akcijaB);
        }
        if ((coll.collider.gameObject.tag == "portal")&&(moving))
        {
            portaj = true;
        }

    }

    void OpenTheDoor()
    {
        GameObject door = GameObject.Find("door");
        door.transform.position = new Vector3(3.484f, -1, 0);
    }
}
