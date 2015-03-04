using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject particles;
    public static bool playing = false;
    public static float points;

    float deltaPoints = 0;
    GameObject canvas;
    Text pointsText;
    GameObject controls;
    Vector3 origPos;

    void Awake(){
        canvas = GameObject.Find("Canvas");
        pointsText = canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
        controls = GameObject.Find("Controls");
        origPos = transform.position;
        playing = false;
    }

    void Update(){
        if ( playing && deltaPoints != points ){
            deltaPoints = Mathf.Lerp(deltaPoints,points,Time.deltaTime);
            pointsText.text = (int)deltaPoints + " points, " + (int)(transform.position.y) + " height";
        }
    }

    void OnGUI(){
        if ( playing ){
            if ( GUI.Button(new Rect(0,0,Screen.height*0.1f,Screen.height*0.1f),"Redo") ){
                playing = false;

                foreach (Draggable o in GameObject.FindObjectsOfType<Draggable>()){
                    if ( o.GetComponent<MovingBox>() != null ){
                        o.GetComponent<MovingBox>().Reset();
                    } else if ( o.transform.GetChild(0).GetComponent<Chain>() != null ){
                        o.transform.GetChild(0).GetComponent<Chain>().Reset();
                    }

                    o.enabled = true;
                }

                ShowCanvas();
                controls.SetActive(true);

                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;

                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = origPos;
            }
        }
    }

    public static void AddPoints(float x){
        points += x;
    }

    public void Play(){
        foreach (Draggable o in GameObject.FindObjectsOfType<Draggable>()){
            if ( o.GetComponent<MovingBox>() != null ){
                o.GetComponent<MovingBox>().startTime = Time.time;
            }

            o.enabled = false;
        }

        HideCanvas();
        controls.SetActive(false);

        points = 0;
        deltaPoints = 0;

        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        playing = true;
    }

    void OnCollisionEnter(Collision other){
        Instantiate(particles,other.contacts[0].point,Quaternion.identity);
    }

    void HideCanvas(){
        for (int i = 1; i < canvas.transform.childCount; i++){
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void ShowCanvas(){
        for (int i = 1; i < canvas.transform.childCount; i++){
            canvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
