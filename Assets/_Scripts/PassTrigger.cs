﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PassTrigger : MonoBehaviour {

    public CarController carController;
    public LightAnimation lightQueue;
    public GameObject failTrigger;
    public StopLight stopLight;
    private ScoreCard scoreCard;
    private bool successfulStop = false;

	// Use this for initialization
	void Start () {
		scoreCard = FindObjectOfType<ScoreCard>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.ToString() == "Simulator Car (UnityEngine.BoxCollider)")
        {
            if (stopLight != null)
            {
                if (stopLight.getCurrentLight() == "RED")
                {
                    if (Math.Round(carController.CurrentSpeed) == 0)
                    {
                        if (!successfulStop)
                        {
                            scoreCard.SuccessfulStop();
                            successfulStop = true;
                            Debug.Log("Successful Stop at Light!");
                        }
                        //this.gameObject.SetActive(false);
                       // if (failTrigger != null)
                       // {
                            //failTrigger.SetActive(false);
                        //}
                    } /*else
                    {
                        Debug.Log("Failure to stop at red light!");
                        this.gameObject.SetActive(false);
                        if (failTrigger != null)
                        {
                            failTrigger.SetActive(false);
                        }
                    }*/
                }
            } else
            {
                if (Math.Round(carController.CurrentSpeed) == 0)
                {
                    Debug.Log("Successful Stop!");
                    scoreCard.SuccessfulStop();
                    this.gameObject.SetActive(false);
                    failTrigger.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.ToString() == "Simulator Car (UnityEngine.BoxCollider)")
        {
            if (lightQueue != null) {
                if (stopLight.getCurrentLight() != "RED")
                {

                    Debug.Log("Successful Traffic Light Sequence!");
                    scoreCard.SuccessfulLightSequence();
                    lightQueue.trafficLight = true;
                    this.gameObject.SetActive(false);
                    failTrigger.SetActive(false);
                }
            }
        } /*else
        {
            Debug.Log("Failed Traffic Light Sequence!");
            this.gameObject.SetActive(false);
            failTrigger.SetActive(false);
        }*/
    }
}
