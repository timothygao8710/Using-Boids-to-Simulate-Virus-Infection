using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{

    public float speedMultiplier = 1f;
    //for jerky movement:
    public float smoothTime = 0.5f;
    
//transform.forward is for z axis
//transform.up is for y axis
    // void Start()
    // {
    //     rb = this.GetComponent<Rigidbody2D>();
    //     Quaternion q = transform.rotation;
    //     Vector3 rot = q.eulerAngles;
    //     Vector2 forward = new Vector2(-Mathf.Sin(rot.z * Mathf.Deg2Rad), Mathf.Cos(rot.z * Mathf.Deg2Rad));
    //     //negative sin because rotation on the unit circle is counter clockwise but in unity its clockwise
    //     //this only effects y tho
    //     //also, y and x axis switched because unit circle starts at (1,0) but unity starts at (0,1)
    //     rb.velocity = forward*initialVelocity;
    // }

    void Update(){
        wrap();
    }

    //torus
    void wrap(){
        Camera cam = Camera.main;
        float d = 0.00001f; //for edge cases like when moving straight horizontally or vertically near edge
        float halfHeight = cam.orthographicSize - this.GetComponent<SpriteRenderer>().bounds.size.y/2;
        float halfWidth = cam.aspect * cam.orthographicSize - this.GetComponent<SpriteRenderer>().bounds.size.x/2;
        bool changed = false;
        if(-halfWidth >= this.transform.position.x){
            this.transform.position = new Vector2(halfWidth-d, this.transform.position.y);
            changed = true;
        }
        if(!changed && halfWidth <= this.transform.position.x){
            this.transform.position = new Vector2(d-halfWidth, this.transform.position.y);
        }
        changed = false;
        if(-halfHeight >= this.transform.position.y){
            this.transform.position = new Vector2(this.transform.position.x, halfHeight-d);
            changed = true;
        }
        if(!changed && halfHeight <= this.transform.position.y){
            this.transform.position = new Vector2(this.transform.position.x, d-halfHeight);
        }
    }
    Vector2 currentVelocity; //helper variable for smoothdamp
    public void Move(Vector2 dir){
        dir *= speedMultiplier;
        dir = Vector2.SmoothDamp(this.transform.up, dir, ref currentVelocity, smoothTime);
        transform.up = dir; //rotate to that angle
        transform.position += (Vector3)dir * Time.deltaTime;
    }
}
