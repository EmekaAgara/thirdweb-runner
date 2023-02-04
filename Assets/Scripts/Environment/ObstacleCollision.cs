using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    // public AudioSource coinFX;
    public GameObject thePlayer;
    public GameObject charModel;
    public AudioSource crashSound;
    public GameObject levelControl;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        thePlayer.GetComponent<PlayerMove>().enabled = false;
        charModel.GetComponent<Animator>().Play("Slipping");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashSound.Play();
        levelControl.GetComponent<EndRunSequence>().enabled = true;
    }

}
