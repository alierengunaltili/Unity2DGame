using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public Transform renderer;
    public Vector3 current_position;
    private float num;
    private Random rand;
    private bool isTouched = false;


    public void setBool()
    {
        isTouched = true;
    }

    public bool getBool()
    {
        return this.isTouched;
    }

    // Start is called before the first frame update
    void Start()
    {
 
        //rand = new Random();

        //num = Random.Range(-5f,5f);
        //current_position = new Vector3(transform.position.x + num, transform.position.y, transform.position.z);
        //transform.position = current_position;
    }

    // Update is called once per frame
    void Update()
    {
       


        if (transform.position.y > renderer.transform.position.y - 0.2)
        {
            Destroy(gameObject);
        } 
    }

    public void setRenderer(Transform renderer)
    {
        this.renderer = renderer;
    }
    public Transform getRenderer()
    {
        return this.renderer;
    }
}
