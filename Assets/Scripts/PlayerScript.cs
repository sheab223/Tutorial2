using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Animator anim;

    private Rigidbody2D rd2d;
    
    public float speed;

    public float jumpForce;

    private bool facingRight = true;

    public Text score;

    public Text gameOver;

    public Text livesText; 

    private int lives;

    private int scoreValue;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;
    
    //public Text hozText;

    //public Text jumpText;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        lives = 3;
        SetScoreValue();
        SetLivesText();
        gameOver.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        /*if (hozMovement > 0 && facingRight == true)
        {
           Debug.Log ("Facing Right");
           hozText.text = "Facing Left"; 
        }

        if (vertMovement > 0 && isOnGround == false)
        {
            Debug.Log ("Jumping");
            jumpText.text = "Jumping";
        }
        else if (vertMovement == 0 && isOnGround == true)
        {
            Debug.Log ("Not Jumping");
            jumpText.text = "Not Jumping";
        }*/
    }

    

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreValue();
            Destroy(collision.collider.gameObject);
        }
         else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            Destroy(gameObject);
            gameOver.text = "You Lose! \n Game created by Shea Barber";
        }
    }
  
    void SetScoreValue()
    {
        score.text = "Score: " + scoreValue.ToString();
        if(scoreValue == 4)
        {
            lives = 3;
            SetLivesText();
            transform.position = new Vector3(-9.0f, 16.5f, 0.0f);
        }
        if (scoreValue >=8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            gameOver.text = "You Win! \n Game created by Shea Barber";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" && isOnGround)
        { 
            anim.SetInteger("state", 0);
            
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("state", 1);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("state", 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("state", 1);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("state", 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("state", 2);
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}