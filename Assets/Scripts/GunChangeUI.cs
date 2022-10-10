using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GunChangeUI : MonoBehaviour
{

    [SerializeField] private Image defaultGun;
    [SerializeField] private Image defaultGunBlack;
    [SerializeField] private Image chaacMask;
    [SerializeField] private Image chaacMaskBlack;
    [SerializeField] private Image jaguarMask;
    [SerializeField] private Image jaguarMaskBlack;

    [SerializeField] private Image bombs;
    [SerializeField] public TextMeshProUGUI numberOfBombsText;
    public int numberOfBombs;
    public float cooldownBombs = 3f;
    public bool isBombsCooldown = false;

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
            default:
            case 2:
                Restart();
                jaguarMaskBlack.fillAmount = 1;
                break;
            case 1:
                Restart();
                chaacMaskBlack.fillAmount = 1;
                break;
            case 0:
                Restart();
                defaultGunBlack.fillAmount = 1;
                break;
        }
    }

    private void Restart()
    {
        defaultGun.fillAmount = 0;
        defaultGunBlack.fillAmount = 0;
        chaacMask.fillAmount = 0;
        chaacMaskBlack.fillAmount = 0;
        jaguarMask.fillAmount = 0;
        jaguarMaskBlack.fillAmount = 0;
        isChangeCooldown = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Restart();
        defaultGun.fillAmount = 1;
        isChangeCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfBombsText.text = numberOfBombs.ToString();
        if(isBombsCooldown)
        {
            bombs.fillAmount += 1 / cooldownBombs * Time.deltaTime;

            if(bombs.fillAmount >= 1)
            {
                bombs.fillAmount = 1;
                isBombsCooldown = false;
            }
        }

        if (isChangeCooldown && fireArm.fireArm == 2)
        {

            jaguarMask.fillAmount += 1 / maskCooldown * Time.deltaTime;

            if (jaguarMask.fillAmount >= 1)
            {
                jaguarMask.fillAmount = 1;
                isChangeCooldown = false;
            }

        }

        if (isChangeCooldown && fireArm.fireArm == 1){

            chaacMask.fillAmount += 1 / maskCooldown * Time.deltaTime;

            if(chaacMask.fillAmount >= 1){
                chaacMask.fillAmount = 1;
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

    public void ChangeBomb()
    {
        bombs.fillAmount = 0;
        numberOfBombs -= 1;
        isBombsCooldown = true;
    }
}
