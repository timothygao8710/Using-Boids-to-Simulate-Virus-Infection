using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behavior : MonoBehaviour
{   
    CircleCollider2D cur;
    controller control;
    public float rangeOfVision;
    public float avoidanceRadius;

    public float seperatationFactor;
    public float AllignmentFactor;
    public float CohesionFactor;
    
    //Seperation: steer to avoid overcrowding
    //Allignment: steer to average heading of flock
    //Cohesion: Steer to center of mass
    void Start()
    {
        cur = this.GetComponent<CircleCollider2D>();
        control = this.GetComponent<controller>();
        cur.radius = rangeOfVision;
    }

    void Update()
    {
        List<Transform> near = getNearby();
        Vector2 ret = Cohesion(near)*CohesionFactor;
        ret += Allignment(near)*AllignmentFactor;
        ret += Seperation(near)*seperatationFactor;
        control.Move(ret);
    }

    Vector2 Seperation(List<Transform> near){
        Vector2 ret = Vector2.zero;
        int inr = 0;
        foreach(Transform t in near){
            if(Vector2.SqrMagnitude(t.transform.position - transform.position) <= avoidanceRadius){
                inr++;
                ret += (Vector2)(transform.position - t.transform.position);
            }
        }
        if(inr != 0) ret /= inr;
        return ret;
    }

    Vector2 Allignment(List<Transform> near){
        if(near.Count == 0) return Vector2.zero; //needed cuz /0 is bad
        Vector2 ret = Vector2.zero;
        foreach(Transform t in near){
            ret += (Vector2)t.up; //luckily no math cuz unity turns quaternion into vector for us
            //has default mag of 1
        }
        ret /= near.Count;
        return ret;
    }

     //returns center of mass
    Vector2 Cohesion(List<Transform> near){
        if(near.Count == 0) return Vector2.zero;
        Vector2 ret = Vector2.zero;
        foreach(Transform t in near){
            ret += (Vector2)t.position;
        }
        ret /= near.Count;
        //create offset
        ret -= (Vector2)transform.position;
        return ret;
    }

    //not the most efficient to call this on everything, can improve later if its a problem
    //alternate solution: ontriggerenter on triggerexit add/remove object from hashset
    //problem: If both objects are isTrigger true then the collision does not happen for some reason
    //it must be one is isTrigger = true and the other is isTrigger = false (Unity do be annoying like dat)
    public List<Transform> getNearby(){
        List<Transform> ret = new List<Transform>();
        Collider2D[] all = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        foreach(Collider2D c in all){
            if(c != cur){
                ret.Add(c.gameObject.transform);
            }
        }
        return ret;
    }
}
