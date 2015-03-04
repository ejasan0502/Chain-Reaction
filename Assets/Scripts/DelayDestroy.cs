using UnityEngine;
using System.Collections;

public class DelayDestroy : MonoBehaviour {
    IEnumerator Start(){
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
