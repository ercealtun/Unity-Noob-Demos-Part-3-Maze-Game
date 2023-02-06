using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Button playAgainButton;
    public Text time, health, status;
    private Rigidbody rg;
    private float timeCounter = 16.5f;
    public float speed = 8f;
    private int healthCounter = 3;
    private bool gameContinue = true;
    private bool gameIsDone = false;
        
        
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        health.text = "Health: " + healthCounter.ToString();
        time.text = "Time: " + timeCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameContinue && !gameIsDone)
        {
            timeCounter -= Time.deltaTime;
            time.text = "Time: " + ((int)timeCounter).ToString();
        }
        else if(!gameIsDone)
        {
            status.text = "Game couldn't done";
            playAgainButton.gameObject.SetActive(true);
        }
        
        if (timeCounter < 0) gameContinue = false;
    }

    void FixedUpdate()
    {
        if (gameContinue && !gameIsDone)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 force = new Vector3(-vertical, 0, horizontal);
            rg.AddForce(force * speed);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
        

    }

    void OnCollisionEnter(Collision collision)
    {
        string objectName = collision.gameObject.name;
        if (objectName.Equals("End Point"))
        {
            //Debug.Log("Game is done!");
            gameIsDone = true;
            status.text = "Game is done! Congrats!!";
            playAgainButton.gameObject.SetActive(true);
        }
        else if( !objectName.Equals("Maze Plane") && !objectName.Equals("Plane"))
        {
            healthCounter--;
            health.text = "Health: " + healthCounter.ToString();

            if (healthCounter == 0) gameContinue = false;
        }
        
    }
    
}
