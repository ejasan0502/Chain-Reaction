using UnityEngine;
using System.Collections;

public class MovingBox : MonoBehaviour {
    public Transform pos;
    public float timeRate = 3f;

    bool reverse = false;
    public Vector3 startPos;
    Vector3 direction;
    public float startTime;
    public GameObject obj;

    void Awake(){
        startPos = transform.position;
    }

    void Update(){
        if ( pos != null && Ball.playing ){
            if ( reverse ){
                if ( Time.time - startTime >= timeRate ){
                    transform.position = Vector3.Lerp(transform.position,startPos,Time.deltaTime);
                    if ( obj != null ) obj.transform.position = Vector3.Lerp(obj.transform.position,startPos,Time.deltaTime);
                    if ( Vector3.Distance(transform.position,startPos) <= 0.1f ){
                        startTime = Time.time;
                        reverse = false;
                    }
                }
            } else {
                if ( Time.time - startTime >= timeRate ){
                    transform.position = Vector3.Lerp(transform.position,pos.position,Time.deltaTime);
                    if ( obj != null ) obj.transform.position = Vector3.Lerp(obj.transform.position,pos.position,Time.deltaTime);
                    if ( Vector3.Distance(transform.position,pos.position) <= 0.1f ){
                        startTime = Time.time;
                        reverse = true;
                    }
                }
            }
        }
    }

    public void Reset(){
        transform.position = startPos;
        startTime = Time.time;
    }

    void OnTriggerEnter(Collider other){
        obj = other.gameObject;
    }

    void OnTriggerExit(Collider other){
        obj = null;
    }
}
