using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{

    private Camera cam;
    private Vector3 position;
    private SpriteRenderer sprite;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        position = cam.ScreenToWorldPoint(Input.mousePosition);
     
        Vector3 rotation = position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ); 

        if(rotZ > 90 && rotZ <= 180 || rotZ >= -180 && rotZ < -90)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }

        if(Input.GetMouseButton(0))
        {
            anim.SetBool("Shooting", true);
        }
        else
        {
            anim.SetBool("Shooting", false);
        }
    }
}
