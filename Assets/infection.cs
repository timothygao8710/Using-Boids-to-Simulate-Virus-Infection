using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infection : MonoBehaviour
{
    //Later: Implement degrees of infection and death
    public bool isInfected;
    public float infectionRadius;
    public float infectiveness;
    public Color infectColor;
    public Color healthy;
    private SpriteRenderer spriteRenderer;
    private float nextUpdate;
    public float updateDuration;
    public CircleCollider2D cur;
    public float maximmune;
    private float immune;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cur = GetComponent<CircleCollider2D>();
        if(isInfected){
            spriteRenderer.color = infectColor;
        }
        nextUpdate = Time.time + updateDuration;
        immune = Random.Range(0, maximmune);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextUpdate) return;
        
        if(isInfected){
            float random = Random.Range(0,10);
            if(random < immune){
                isInfected = false;
                spriteRenderer.color = healthy;
            }
        }
        if(!isInfected){
            List<GameObject> nearby = getNearByGameObjects();
            foreach(GameObject g in nearby){
                if(g.GetComponent<infection>().isInfected){
                    float random = Random.Range(0, 1);
                    if(random <= infectiveness){
                        isInfected = true;
                        spriteRenderer.color = infectColor;
                    }
                }
            }
        }
        nextUpdate += updateDuration;
    }

    public List<GameObject> getNearByGameObjects(){
        List<GameObject> ret = new List<GameObject>();
        Collider2D[] all = Physics2D.OverlapCircleAll(transform.position, infectionRadius);
        foreach(Collider2D c in all){
            if(c != cur){
                ret.Add(c.gameObject);
            }
        }
        return ret;
    }
}
