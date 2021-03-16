using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Camera camera;
    private Vector2 screenBounds;
    public GameObject player;
    private Vector3 playerPosition;
    public float offset;
    public float offsetSmooth;
    // Start is called before the first frame update
    void Start()
    {
        camera = this.gameObject.GetComponent<Camera>();
        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        //loadObjects(backgroundObjects);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        if (player.transform.localScale.y > 0f)
        {
            playerPosition = new Vector3(transform.position.x, player.transform.position.y + offset, transform.position.z);
        }
        else
        {
            playerPosition = new Vector3( transform.position.x, player.transform.position.y - offset, transform.position.z);
        }
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmooth + Time.deltaTime);
    }

    void loadObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded =  (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for(int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
}
