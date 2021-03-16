using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class CubeStoppper : MonoBehaviour
{
    public GameOverScreen1 gameOver;
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.transform.position.y + 12f < transform.position.y)
        //{
          //  transform.position = new Vector3(transform.position.x, player.transform.position.y - 5f, transform.position.z);
        //}
        //if(player.transform.position.y + 12f > transform.position.y)
        if(player.transform.position.y + 14f < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 8f, transform.position.z);
        }       
        //playerPosition = new Vector3(transform.position.x, player.transform.position.y + offset, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            gameOver.Setup();
        }
    }
}
