using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float sliderVolume;
    //public float health;

    public PlayerData(float sliderVolume/*, float health*/) {
        this.sliderVolume = sliderVolume;
        //this.health = health;   
    }

    public override string ToString() { 
        return $"El nivel del volumen es {sliderVolume}";
    }
}
