using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static float maxhex = 255;
    Camera camera;
    float startTime;
    public float switchTime;
    public float lerpTime;
    public float min = 10;
    public float max = 250;
    Color nextColor;
    // Update is called once per frame
    void Start(){
        min /= maxhex;
        max /= maxhex;
        camera = Camera.main;
        nextColor = new Color(Random.Range(min,max), Random.Range(min, max), max);
        startTime = Time.time;
    }
    void Update()
    {
        camera.backgroundColor = Color.Lerp(camera.backgroundColor, nextColor, lerpTime*Time.deltaTime);
        if(Time.time >= startTime + switchTime){
            nextColor = new Color(Random.Range(min,max), Random.Range(min, max), max);
            startTime = Time.time;
        }
    }
}
