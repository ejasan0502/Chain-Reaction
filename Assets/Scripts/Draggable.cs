using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {
    public bool canRotate = false;
    public bool mustSetEndPos = false;
    public Renderer guiBounds;

    Rect bounds;
    bool selected = false;
    bool rotate = false;
    bool endPos = false;
    GUIStyle style;
    Vector3 startPos;
    GameObject obj = null;
    float delta = 0f;

    void Awake(){
        style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
    }

    void Update(){
        if ( selected ){
            if ( rotate ){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

                delta = pos.x - startPos.x;
                transform.rotation = Quaternion.EulerAngles(0,0,delta);

                if ( Input.GetMouseButtonDown(0) ){
                    ShowAllDraggables();
                    selected = false;
                    rotate = false;
                }
            } else if ( endPos ){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

                obj.transform.position = pos;
                obj.GetComponent<LineRenderer>().SetPosition(0,transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(1,obj.transform.position);

                if ( Input.GetMouseButtonDown(0) ){
                    ShowAllDraggables();
                    GetComponent<MovingBox>().pos = obj.transform;
                    selected = false;
                    endPos = false;
                }
            } else {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = pos;

                if ( GetComponent<MovingBox>() != null ){
                    if ( obj != null ){
                        obj.GetComponent<LineRenderer>().SetPosition(0,transform.position);
                    }
                }
            }
        }

        Vector3 min = Camera.main.WorldToScreenPoint(guiBounds.bounds.min);
        Vector3 max = Camera.main.WorldToScreenPoint(guiBounds.bounds.max);
        bounds = new Rect(min.x,Screen.height-max.y,max.x - min.x,max.y - min.y);
    }

    void OnGUI(){
        if ( selected ){
            string s = "";

            if ( rotate ) s = delta*57.2957795f+"°";

            if ( GUI.Button(bounds,s,style) ){
                ShowAllDraggables();

                if ( GetComponent<MovingBox>() != null ){
                    GetComponent<MovingBox>().startPos = transform.position;
                } else if ( transform.GetChild(0).GetComponent<Chain>() != null ){
                    transform.GetChild(0).GetComponent<Chain>().startPos = transform.position;
                }

                selected = false;
                rotate = false;
            }
        } else {
            if ( canRotate ){
                if ( GUI.Button(new Rect(bounds.x,bounds.y,bounds.width/2.0f,bounds.height),"M") ){
                    HideAllDraggables();
                    selected = true;
                    rotate = false;
                }
                if ( GUI.Button(new Rect(bounds.x+bounds.width/2.0f,bounds.y,bounds.width/2.0f,bounds.height),"R") ){
                    HideAllDraggables();
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10f;
                    startPos = Camera.main.ScreenToWorldPoint(mousePos);
                    selected = true;
                    rotate = true;
                }
            } else if ( mustSetEndPos ){
                if ( GUI.Button(new Rect(bounds.x,bounds.y,bounds.width/2.0f,bounds.height),"M") ){
                    HideAllDraggables();
                    selected = true;
                }
                if ( GUI.Button(new Rect(bounds.x+bounds.width/2.0f,bounds.y,bounds.width/2.0f,bounds.height),"E") ){
                    HideAllDraggables();
                    if ( obj == null ){
                        GameObject o = new GameObject("End Point");
                        LineRenderer l = o.AddComponent<LineRenderer>();
                        obj = o;
                        l.SetVertexCount(2);
                        l.SetWidth(0.1f,0.1f);
                    }

                    selected = true;
                    endPos = true;
                }
            } else {
                if ( GUI.Button(bounds,"M") ){
                    HideAllDraggables();
                    selected = true;
                }
            }  

            if ( GUI.Button(new Rect(bounds.x + bounds.width,bounds.y,Screen.height*0.05f,Screen.height*0.05f),"x") ){
                Destroy(gameObject);
            }
        }
    }

    void HideAllDraggables(){
        foreach (Draggable o in GameObject.FindObjectsOfType<Draggable>()){
            if ( o != this ) o.enabled = false;
        }
    }

    void ShowAllDraggables(){
        foreach (Draggable o in GameObject.FindObjectsOfType<Draggable>()){
            o.enabled = true;
        }
    }
}
