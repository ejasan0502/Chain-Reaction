using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
    public Transform direction;

    private GameObject ball;

    void Start(){
        
    }

    void OnTriggerEnter(Collider other){
        if ( other.tag == "Ball" ){
            ball = other.gameObject;
            StartCoroutine("Launch");
        }
    }

    IEnumerator Launch(){
        yield return new WaitForSeconds(0.25f);
        ball.GetComponent<Rigidbody>().AddForce(direction.forward*25f,ForceMode.Impulse);
    }
}
