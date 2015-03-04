using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
    public float launchTime = 3f;

    GameObject obj;

    void OnTriggerEnter(Collider other){
        if ( other.tag == "Ball" ){
            obj = other.gameObject;
            StartCoroutine("Launch");
        }
    }

    void OnTriggerExit(Collider other){
        if ( other.tag == "Ball" ){
            obj = null;
        }
    }

    IEnumerator Launch(){
        yield return new WaitForSeconds(launchTime);
        if ( obj != null ){
            obj.GetComponent<Rigidbody>().AddForce(transform.up*30f,ForceMode.Impulse);
            obj = null;
        }
    }
}
