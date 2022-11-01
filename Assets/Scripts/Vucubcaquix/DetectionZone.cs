using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>();

    // Detect when object enter range
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == tagTarget) {
            detectedObjs.Add(collider);
        }
    }
    
        

    // Detect when object leaves range
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == tagTarget){
            detectedObjs.Remove(collider);
        }
        
    }
}
