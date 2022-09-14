using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunChangeUI : MonoBehaviour
{

    [SerializeField] private Image defaultGun;
    [SerializeField] private Image mask1;
    [SerializeField] private Image defaultGunBlack;
    [SerializeField] private Image mask1Black;
    private Image varToFill;
    private float maskCooldown = 1f;
    public bool isChangeCooldown = false;
    public Shooter fireArm;

    
    // private void Cooldown(int var){
    //     while(isChangeCooldown){
    //         if(var.fillAmount >= 1){
    //             var.fillAmount = 1;
    //             isChangeCooldown = false;
    //         }else{
    //             var.fillAmount += 1 / maskCooldown * Time.deltaTime;
    //         }
        
    //     }
    // }

    public void ChangeUI(int _mask){
        switch(_mask){
            case 1:
                defaultGun.fillAmount = 0;
                defaultGunBlack.fillAmount = 0;
                mask1.fillAmount = 0;
                mask1Black.fillAmount = 1;
                isChangeCooldown = true;
                break;
            default:
                defaultGun.fillAmount = 0;
                defaultGunBlack.fillAmount = 1;
                mask1.fillAmount = 0;
                mask1Black.fillAmount = 0;
                isChangeCooldown = true;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultGun.fillAmount = 1;
        defaultGunBlack.fillAmount = 0;
        mask1.fillAmount = 0;
        mask1Black.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChangeCooldown && fireArm.fireArm == 1){

            mask1.fillAmount += 1 / maskCooldown * Time.deltaTime;

            if(mask1.fillAmount >= 1){
                mask1.fillAmount = 1;
                isChangeCooldown = false;
            }
        
        }
        if(isChangeCooldown && fireArm.fireArm == 0){

            defaultGun.fillAmount += 1 / maskCooldown * Time.deltaTime;

            if(defaultGun.fillAmount >= 1){
                defaultGun.fillAmount = 1;
                isChangeCooldown = false;
            }
        
        }

    }
}
