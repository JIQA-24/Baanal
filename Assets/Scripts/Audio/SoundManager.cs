using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    public enum Sound {
        PlayerMove,
        PlayerHit,
        PlayerAttackCervatana,
        EnemyHit,
        EnemyDieVucubCaquix,
        Cacao,
        Avispero,
        Potion,
        Dash,
        Jump,
        SpearShot,
        RegularShot,
        BowShot,
        RegularShotImpact,
        OpenInventory,
        EquipInventory,
        UIButton,
        UIButtonAccept,
        JumpImpact,
        CacaoPick,
        VucubImpacto,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void Initialize() {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
        soundTimerDictionary[Sound.VucubImpacto] = 0f;
    }

    public static void PlaySound(Sound sound) {
        if (CanPlaySound(sound)) {
            if (oneShotGameObject == null) {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound) {
        switch (sound) {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound)) {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .30f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else {
                        return false;
                    } 
                }
                else
                {
                    return true;
                }
            case Sound.VucubImpacto:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float vucubImpactTimerMax = 0.7f;
                    if (lastTimePlayed + vucubImpactTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound) { 
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.i.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sonido " + sound + " no encontrado");
        return null;
    }

}
