using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
    public float force = 10f;

    void OnCollisionEnter(Collision other){
        if ( other.gameObject.tag == "Ball" ){
            Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
            foreach (ContactPoint c in other.contacts){
                c.otherCollider.GetComponent<Rigidbody>().AddForce(-1*c.normal*force, ForceMode.Impulse);
            }
        }
    }
}
