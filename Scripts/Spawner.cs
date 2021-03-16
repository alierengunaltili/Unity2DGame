using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject plank;
    public GameObject player;
    public float time = 2.0f;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.rotation.z));
        //StartCoroutine(plankSpawn());

    }

    private void Update()
    {
        
    }

    private void spawn()
    {
        GameObject a = Instantiate(plank) as GameObject;
        a.transform.position = new Vector3(Random.Range(-2f,2f), player.transform.position.y - 2, 10);

    }
    /*IEnumerator plankSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            spawn();
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Plank")
        {
            Destroy(gameObject);
            Vector3 newpos = new Vector3(Random.Range(-4f, 4f), player.transform.position.y - 10, player.transform.position.z);
            GameObject tmp = Instantiate(plank, newpos, plank.transform.rotation);
            tmp.SetActive(true);
        }
    }
}
