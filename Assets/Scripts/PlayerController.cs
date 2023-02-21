using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    int pickupCount;
    Timer timer;

    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Display the pickup count
        CheckPickups();
        //Get the Timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
    }

    void Update()
    {
        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from our vertical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our movement vector
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
        //If the other object contains the Pickup tag, destroy it
        if(other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            CheckPickups();
        }
    }

    void CheckPickups()
    {
        Debug.Log("Pickups left: " + pickupCount);

        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        timer.StopTimer();
        Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
    }


}
