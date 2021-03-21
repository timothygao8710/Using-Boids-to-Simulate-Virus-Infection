using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject menu;
    public GameObject prefab;
    public GameObject controller;
    public flock flock;
    void Start(){
        flock = GetComponent<flock>();
        changePopSize(0.5f);
        percentageInitial(0.5f);
        infectiveness(0.5f);
        survivibility(0.5f);
        simspeed(0.5f);
        changeRadius(0.5f);
    } 
    public void pressed(){
        menu.SetActive(false);
        flock.destory();
        flock.init();
    }
//these are app. values can change later. Simply determined by experimentation
    public void changePopSize(float input){
        controller.GetComponent<flock>().size = 30 + (int)(200f*input);
    }

    public void percentageInitial(float input){
        controller.GetComponent<flock>().percentInfected = input*0.4f;
    }

    public void infectiveness(float input){
        prefab.GetComponent<infection>().infectiveness = input*0.1f;
    }

    public void survivibility(float input){
        prefab.GetComponent<infection>().maximmune = input*0.2f;
    }

    public void simspeed(float input){
        prefab.GetComponent<infection>().updateDuration = 0.2f + (1-input)*2f;
        prefab.GetComponent<controller>().smoothTime = 0.75f + (1-input);
    }

    public void changeRadius(float input){
        prefab.GetComponent<infection>().infectionRadius = 0.4f*input;
    }

    public void reset(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
