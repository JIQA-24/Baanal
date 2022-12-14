using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{   public CharacterController2D facing;
    private void Update() {
        if(PauseMenu.gameIsPaused){
            return;
        }
        Vector3 mousePos = Input.mousePosition;
        

        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        // float angleLeft = Mathf.Atan2(-mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && facing.m_FacingRight) {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        }
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && facing.m_FacingRight){
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && !facing.m_FacingRight) {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
        }
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && !facing.m_FacingRight) {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 180f, -angle));
        }


    }
}
