using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlank : MonoBehaviour
{
    private ParticleSystem death;
    private Rigidbody2D Rigidbody2;
    public Transform renderer;
    private Vector3 currentPosition;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Rigidbody2 = this.GetComponent<Rigidbody2D>();
        Rigidbody2.velocity = new Vector2(-8f, 0);
        currentPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if((currentPosition.x - this.transform.position.x) > (10f))
        {
            if(PlayerPrefs.GetInt("Difficulty") == 0)
            {
                Rigidbody2.velocity = new Vector2(11f, 0);
            }
            if(PlayerPrefs.GetInt("Difficulty") == 1)
            {
                Rigidbody2.velocity = new Vector2(11f, 5f);
            }
        }
        if((this.transform.position.x - currentPosition.x) > (10f))
        {
            Rigidbody2.velocity = new Vector2(-11f, 0);
        }


        if(transform.position.y > renderer.position.y - 0.2)
        {
            Destroy(gameObject);
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           
            //we cannot able to see the partcile system effect as we immediately destory the object so the problem had to be fixed is 
            //making the deathEffect visible before destroying the object.
            Destroy(collision.gameObject);
            //Time.timeScale = 0f; //pausing the game 
            
        }
    }*/

    public void setRenderer(Transform renderer)
    {
        this.renderer = renderer;
    }
}
