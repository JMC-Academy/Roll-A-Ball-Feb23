using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    int pickupCount;
    int totalPickups;
    Timer timer;
    bool wonGame = false;

    [Header("UI")]
    public GameObject winPanel;
    public TMP_Text winTime;
    public GameObject inGamePanel;
    public TMP_Text timerText;
    public TMP_Text pickupText;

    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Set the pickup count to the total
        pickupCount = totalPickups;
        //Display the pickup count
        CheckPickups();
        //Get the Timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //Turn off our win panel
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
    }

    void Update()
    {
        if (wonGame == true)
            return;

        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from our vertical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our movement vector
        rb.AddForce(movement * speed);

        //Update the timer text to that of the timer
        timerText.text = "Time: " + timer.GetTime().ToString("F3");
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
        //Debug.Log("Pickups left: " + pickupCount);
        pickupText.text = "Pickups left: " + pickupCount + "/" + totalPickups;
        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        wonGame = true;
        timer.StopTimer();
        //Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
        //Turn off our in game panel
        inGamePanel.SetActive(false);
        //Set the timer on the text
        winTime.text = "Your time was: " + timer.GetTime().ToString("F3");
        //Turn on our win panel
        winPanel.SetActive(true);

        //Set the velocity of the rigidbody to zero
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Temporary - Remove when doing modules in A2
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
