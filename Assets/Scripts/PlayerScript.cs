using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    //public Text gameOver;

    //public Text livesText; 

    //private int lives = 3;

    private int scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetScoreValue();
        scoreValue = 0;
        //SetLivesText();
       
        //gameOver.text = "";
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
          Application.Quit();
        }
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreValue();
            Destroy(collision.collider.gameObject);
        }
    }

    void SetScoreValue()
    {
        score.text = "Score: " + scoreValue.ToString();
        //if(scoreValue >= 4)
        //{
            //gameOver.text = "You Win! Game is created by Shea Barber";
        //}
    }
    
    //void SetLivesText()
    //{
        //livesText.text = "Lives: " + lives.ToString();
        //if (lives <= 0)
        //{
            //gameOver.text = "You Lose! Game created by Shea Barber";
            //Destroy(gameObject);
        //}
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}