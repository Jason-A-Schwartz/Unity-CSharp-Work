//These imports are needed to use certain functions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//This is the overall program
public class PlayerController : MonoBehaviour
{
    //This creates the objects to be used for later
    public float Speed = 0;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI TimerText;
    public AudioSource PickUpSound;
    public AudioSource LoseSound;
    private Rigidbody RB;
    private float MovementX;
    private float MovementY;
    public int Count;
    private float Timer = 30;
    float TimeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //These are defaults for the game to restart/run correctly
        Time.timeScale = 1;
        RB = GetComponent<Rigidbody>();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        Count = 0;
        CountIncrease();
        TimerIncrease();
    }

    //This is how movement is input
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        MovementX = movementVector.x;
        MovementY = movementVector.y;
    }

    //This updates the count text
    void CountIncrease()
    {
        CountText.text = "Gems: " + Count.ToString();
    }

    //This updates the timer text with a proper format
    void TimerIncrease()
    {
        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        TimerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    //This is called when the player gets the collectible and ends the game
    void WinTest()
    {
            WinTextObject.SetActive(true);
            Music.WinCheck = true;
            WinSound.WinCheck = true;
            Time.timeScale = 0;
    }

    //This is called when the player hits the lava to end the game
    void LoseTest()
    {
        LoseTextObject.SetActive(true);
        LoseSound.Play();
        Music.WinCheck = true;
        Time.timeScale = 0;
    }

    //This updates every frame
    void Update()
    {
        //This counts for every second passed
        TimeElapsed += Time.deltaTime;

        //This checks the count and removes one from the timer and updates the text
        if (TimeElapsed >= 1f && Timer !=0)
        {
            TimeElapsed = TimeElapsed % 1f;
            Timer -= 1;
            TimerIncrease();

            //This runs if the user didn't win in time
            if (Timer == 0)
            {

                Timer = 0;
                LoseTest();
            }
        }
    }

    //This updates the speed
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(MovementX, 0.0f, MovementY);
        RB.AddForce(movement * Speed);
    }

    //This goes off when the player touches a tag value
    private void OnTriggerEnter(Collider other)
    {

        //This is for when the user touches a collectible to delete, increase count, play sounds, and update the text
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Count = Count + 1 ;
            PickUpSound.Play();
            CountIncrease();
        }

        //This is for when the user touches the star to delete, and end the game
        else if (other.gameObject.CompareTag("Done"))
        {
            other.gameObject.SetActive(false);
            WinTest();
        }

        //This is for when the user touches the lava to delete, and end the game
        else if (other.gameObject.CompareTag("GameOver"))
        {
            LoseSound.Play();
            LoseTest();
        }
    }
}