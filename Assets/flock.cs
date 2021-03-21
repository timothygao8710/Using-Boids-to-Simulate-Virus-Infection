using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public List<GameObject> boids = new List<GameObject>();
    public int size = 10;
    public GameObject prefab;
    public float percentInfected;
    void Start()
    {
        init();
    }

    public void destory(){
        foreach(GameObject g in boids){
            Destroy(g);
        }
        boids = new List<GameObject>();
    }
    public void init(){
        Camera cam = Camera.main; //assumes that center point is 0, 0
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        float spriteHalfHeight = prefab.GetComponent<SpriteRenderer>().bounds.size.x/2;
        float spriteHalfWidth = prefab.GetComponent<SpriteRenderer>().bounds.size.y/2;
        for (int i = 0; i <size; i++)
        {
            Vector3 random = new Vector3(
                Random.Range(spriteHalfWidth-halfWidth, halfWidth - spriteHalfWidth),
                Random.Range(spriteHalfHeight-halfHeight, halfHeight -spriteHalfHeight),
                0
            );
            GameObject n = Instantiate(prefab, random, Quaternion.identity);
            n.transform.Rotate(new Vector3(0,0,Random.Range(0, 359)));
            n.name = "Boid: " + i;
            boids.Add(n);
            if(i < size*percentInfected){
                boids[i].GetComponent<infection>().isInfected = true;
            }
        }
    }
}
