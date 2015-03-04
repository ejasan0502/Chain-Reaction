using UnityEngine;
using System.Collections;

public class Chain : MonoBehaviour {
    public Transform end;

    Vector3 offset = Vector3.zero;
    bool hold = false;
    GameObject obj;
    Quaternion startRot;
    public Vector3 startPos;

    void Awake(){
        startRot = transform.parent.rotation;
        startPos = transform.parent.position;
    }

    void FixedUpdate(){
        if ( hold ){
            obj.transform.position = transform.position + offset;
            transform.parent.RotateAround(end.position,transform.forward,1f);
            if ( transform.parent.rotation.eulerAngles.z > 165f ){
                transform.parent.rotation.SetEulerAngles(0,0,165f);
                obj.GetComponent<Rigidbody>().useGravity = true;
                hold = false;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if ( other.gameObject.tag == "Ball" ){
            obj = other.gameObject;
            obj.GetComponent<Rigidbody>().useGravity = false;
            offset = obj.transform.position - transform.position;
            hold = true;
        }
    }

    public void Reset(){
        if ( obj != null ) {
            transform.parent.position = startPos;
            transform.parent.rotation = startRot;
            obj = null;
        }
    }
}
