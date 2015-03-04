using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    public GameObject[] objects;

    Vector3 scrollPos;

    void OnGUI(){
        GUI.skin.horizontalScrollbar = null;
        GUI.skin.verticalScrollbar = null;
        scrollPos = GUI.BeginScrollView(new Rect(Screen.width-Screen.height*0.1f,0,Screen.height*0.1f,Screen.height),
                                        scrollPos,
                                        new Rect(0,0,Screen.height*0.1f,Screen.height*0.1f*objects.Length));

        for (int i = 0; i < objects.Length; i++){
            if ( GUI.Button(new Rect(0,Screen.height*0.1f*i,Screen.height*0.1f,Screen.height*0.1f),objects[i].name) ){
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0f;

                GameObject o = (GameObject)Instantiate(objects[i]);
                o.transform.position = pos;
            }
        }

        GUI.EndScrollView();
    }
}
