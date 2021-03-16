using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private SpriteRenderer square_renderer;
    private SpriteRenderer background_renderer;
    public GameObject background;
    public GameObject dark_background;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.tag == "Yellow")
        {
            square_renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            //square_renderer.material.SetColor("_Color" , Color.white); 
            square_renderer.material.color = Color.blue;
            background.SetActive(false);
            dark_background.SetActive(true);
        }
       
    }
}
