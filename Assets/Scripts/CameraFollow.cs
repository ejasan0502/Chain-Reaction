using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public float speed = 10f;
    public GameObject obj;

    bool moveToOrigin = false;

	void Update () {
        if ( !moveToOrigin ){
	        if ( obj != null && Ball.playing ) {
                Vector3 endPos = obj.transform.position;
                endPos.z = transform.position.z;
                transform.position = Vector3.Lerp(transform.position,endPos,speed*Time.deltaTime);
            } else if ( !Ball.playing ){
                transform.position += new Vector3(Input.GetAxis("Horizontal")*speed,Input.GetAxis("Vertical")*speed,0);
            }
        } else {
            Vector3 pos = new Vector3(0,0,transform.position.z);
            transform.position = Vector3.Lerp(transform.position,pos,Time.deltaTime*10f);
            if ( Vector3.Distance(transform.position,pos) <= 0.1f ){
                moveToOrigin = false;
            }
        }
	}

    public void MoveToOrigin(){
        moveToOrigin = true;
    }
}
