using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeMovement : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 direction;
    public GameOverScreen1 gameOver;
    public GameObject coinTemple;
    public ParticleSystem death;
    public ParticleSystem dust;
    public Transform fallDetector;
    public float speed = 0f;
    public GameObject plank_spawn;
    public GameObject redPlankSpawn;
    private float movement = 0f;
    private float jump_speed = -18f;
    private Rigidbody2D rigidbody;
    public Transform groundcheckpoint;
    public float groundcheckradius;
    public LayerMask groundlayer;
    private bool isTouchingGround;
    public TMP_Text timeRemainingMesh;
    public int Score;
    public TMP_Text scoreText_TMP;
    public TMP_Text highScoreText;
    private float time_remaining;
    private float adder = 2f;
    private int counter = 0;
    private BombPlank bombPlank;
    private PlankController plankController;
    private Vector3 first_Position;
    private Color currentColor;
    private string initTime = "60";
    string highScoreKey = "Highscore";
    private string highScoreKeyTimeLimited = "HighScoreTL";
    public int highScore = 0;
    private Vector3 position_last_plank;
    private bool check_options;
    private float move_speed = 300;
    private float ScreenWidth;
    private int check_int;
    public GameObject sandWatch;
    private int time_counter;
    public GameObject background;
    private int jump_limit;



    // Start is called before the first frame update
    void Start()
    {
        jump_limit = 0;
        time_counter = 1;
        check_int = PlayerPrefs.GetInt("TimeLimiter"); //if it is 1 it means limitless mode. 
        ScreenWidth = Screen.width;
        if(check_int == 0)
        {
            highScore = PlayerPrefs.GetInt(highScoreKey);
        }
        if(check_int == 1)
        {
            highScore = PlayerPrefs.GetInt(highScoreKeyTimeLimited);
        }

        highScoreText.text = "Best score : " + highScore.ToString();
        currentColor = gameObject.GetComponent<Renderer>().material.color;
        time_remaining = 61;
        if(check_int == 1)
        {
            timeRemainingMesh.text = "Limitless Mode";
        }
        //plankController = FindObjectOfType<PlankController>();
        Score = 0;
        rigidbody = GetComponent<Rigidbody2D>();
        first_Position = this.transform.position;
    }

    private void OnDisable()
    {
        if(Score > highScore)
        {
            if(check_int == 1)
            {
                PlayerPrefs.SetInt(highScoreKeyTimeLimited, Score);
                PlayerPrefs.Save();
                highScoreText.text = "Best Score : " + PlayerPrefs.GetInt(highScoreKeyTimeLimited).ToString();
            }
            if(check_int == 0)
            {
                PlayerPrefs.SetInt(highScoreKey, Score);
                PlayerPrefs.Save();
                highScoreText.text = "Best Score : " + PlayerPrefs.GetInt(highScoreKey).ToString();
            }
           

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (check_int == 0 && time_remaining < 50 && time_counter < 3)
        {
            Instantiate(sandWatch, new Vector3(this.transform.position.x, this.transform.position.y - 10f - (50f * time_counter), this.transform.position.z), sandWatch.transform.rotation);
            time_counter++;
        }

        Vector2 current_position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
       /* if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
           
            /*if(touch_pos.x > ScreenWidth / 2)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    speed = 13f;
                    rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                }
            }
            else if(touch_pos.x < ScreenWidth / 2 )
            {
                if(touch.phase == TouchPhase.Began)
                {
                    speed = -13f;
                    rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                }
            } */
           /* if(touch.phase == TouchPhase.Began)
            {
                
                if (touch.position.x < first_Position.x)
                {
                    speed = -12f;
                    //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2f, this.transform.position.z);
                    rigidbody.velocity = new Vector2(speed, 0);
                }
                if (touch.position.x > first_Position.x) 
                {
                    UnityEngine.Debug.Log("Pressed");
                    speed = 12f;
                    rigidbody.velocity = new Vector2(speed, 0);
                }
            }
        } */
         //attempt to understand touch controls.
        if (this.gameObject.GetComponent<Renderer>().material.color != currentColor)
        {
            if(jump_limit < 5)
            {
                jump_speed = -30f;
            }
            if(jump_limit >= 5)
            {
                jump_speed = -18f;
            }
        }
        // if(this.gameObject.GetComponent<Renderer>().material.color == Color)
        //{

        //}
        isTouchingGround = Physics2D.OverlapCircle(groundcheckpoint.position, groundcheckradius, groundlayer);
        time_remaining = time_remaining - Time.deltaTime;
        if(check_int == 0)
        {
            showTime();
        }
        movementOnComputer();    
        spawnObject(20);
        Controller();


        /*int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                RunCharacter(1.0f);
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                RunCharacter(-1.0f);
            }
            ++i;
        }
        */
    }

    /*private void FixedUpdate()
    {
#if UNITY_EDITOR
        RunCharacter(Input.GetAxis("Horizontal"));
#endif
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            if(jump_limit < 5)
            {
                addScore();
            }
            //ScoreManager.instance.addScore();
            Destroy(collision.gameObject);
            addScore();

        }
        if(collision.gameObject.tag == "Bomb" /*&& currentColor == this.gameObject.GetComponent<Renderer>().material.color*/)
        {
            Instantiate(death, transform.position, death.transform.rotation);
            Destroy(this.gameObject);
            gameOver.Setup();
        }

        if(collision.gameObject.tag == "SandWatch")
        {
            time_remaining = 50;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Plank")
        {
            if (!(collision.gameObject.GetComponent<PlankController>().getBool()))
            {
                counter = 18;
                Score = Score + 3;
                collision.gameObject.GetComponent<PlankController>().setBool();
                if(this.gameObject.GetComponent<Renderer>().material.color != currentColor)
                {
                    jump_limit++;
                }
            }
        }
        if(collision.gameObject.tag == "Heart")
        {
            
        }


        /*if(collision.gameObject.tag == "Plank" && counter < 25)
        {
            adder = adder + 5f;
            float horizontal_place = Random.Range(-10f, 10f);
            GameObject a = Instantiate(plank_spawn, new Vector3(collision.gameObject.transform.position.x + horizontal_place, first_Position.y - adder, collision.gameObject.transform.position.z), collision.gameObject.transform.rotation);
            counter++;
            a.GetComponent<PlankController>().setRenderer(collision.gameObject.GetComponent<PlankController>().getRenderer());
        }*/
        scoreText_TMP.text = "Score : " + Score.ToString();
    }

    private void showTime()
    {
        if (time_remaining > 0)
        {
            float seconds = Mathf.Floor(time_remaining % 60);
            if(seconds > 0)
            {
                timeRemainingMesh.text = "Remaining time: " + seconds.ToString();
            }
            else
            {
                timeRemainingMesh.text = "Remaining time: " + initTime;
            }
           
            if (time_remaining < 10)
            {
                timeRemainingMesh.color = Color.red;
                timeRemainingMesh.text = seconds.ToString();
            }
        }
        else if (time_remaining < 1)
        {
            Destroy(this.gameObject);
            gameOver.Setup();
        }
    }
    void addScore()
    {
        Score= Score + 5;
    }

  

    private void RunCharacter(float horizontalInput)
    {
        //rigidbody.velocity = new Vector2(10f * horizontalInput, rigidbody.velocity.y);
        rigidbody.AddForce(new Vector2(horizontalInput * move_speed * Time.deltaTime, 0));

    }

    public void movementOnComputer()
    {
        movement = Input.GetAxis("Horizontal");
        if (movement != 0)
        {
            rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
        }
        //jump_speed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            dust.Play();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_speed);
        }
    }

    public Color getCurrentColor()
    {
        return this.currentColor;
    }

    
    void Controller()
    {
        // Mobile Actions
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchpos.z = 0;

            if (touchpos.x > -272)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        speed = 6f;
                        startPosition = touch.position;
                        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                        break;
                    case TouchPhase.Stationary:
                        if (speed < 13f)
                        {
                            speed += .4f;
                            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                        }
                        break;
                    case TouchPhase.Ended:
                        while (speed > 5f)
                        {
                            speed -= .001f;
                            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                        }
                        break;
                    case TouchPhase.Moved:
                        direction = touch.position - startPosition;
                        if(direction.y < 0 && isTouchingGround)
                        {
                            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_speed);
                        }
                        break;
                }

            }
            else if (touchpos.x < -272)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        speed = 6f;
                        startPosition = touch.position;
                        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                        break;
                    case TouchPhase.Stationary:
                        if (speed < 13f)
                        {
                            speed += .4f;
                            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                        }

                        break;
                    case TouchPhase.Ended:
                        while (speed > 5f)
                        {
                            speed -= .001f;
                            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);

                        }

                        break;

                    case TouchPhase.Moved:
                        direction = touch.position - startPosition;
                        if(direction.y < 0 && isTouchingGround)
                        {
                            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_speed);
                        }
                        break;
                }
            }

        }
    }

        void spawnObject(int limit)
    {
            //spawning obstacle planks in every frame till the 20 planks
            if (counter < limit)
            {
                adder = adder + 6.5f;
                float horizontal_place = Random.Range(-9f, 9f);
                GameObject a = Instantiate(plank_spawn, new Vector3(first_Position.x + horizontal_place, first_Position.y - adder, first_Position.z), plank_spawn.transform.rotation);
                counter++;
                a.GetComponent<PlankController>().setRenderer(fallDetector);
                if (counter % 2 == 0)
                {
                    Instantiate(coinTemple, new Vector3(first_Position.x + horizontal_place - 2f, first_Position.y - 2f - adder , first_Position.z), coinTemple.transform.rotation);
                }
                if (counter % 5 == 0)
                {
                    GameObject tmp = Instantiate(redPlankSpawn, new Vector3(first_Position.x + horizontal_place + 4f, this.transform.position.y - adder - 3f, first_Position.z), redPlankSpawn.transform.rotation);
                    tmp.GetComponent<BombPlank>().setRenderer(fallDetector);
                }
            }
    }    
}

