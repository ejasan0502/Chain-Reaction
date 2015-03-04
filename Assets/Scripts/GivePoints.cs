using UnityEngine;
using System.Collections;

public class GivePoints : MonoBehaviour {
    public float points;

    void OnCollisionEnter(Collision other){
        if ( other.gameObject.tag == "Ball" ){
            if ( GetComponent<AudioSource>() != null ) GetComponent<AudioSource>().Play();
            Ball.AddPoints(points);
        }
    }

    void OnTriggerEnter(Collider other){
        if ( other.gameObject.tag == "Ball" ){
            if ( GetComponent<AudioSource>() != null ) GetComponent<AudioSource>().Play();
            Ball.AddPoints(points);
        }
    }
}
