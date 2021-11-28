using UnityEngine;

public static class AudioManager
{
    //Volume
    public static void ChangeGeneralVolume(float newVolume)
    {
        AkSoundEngine.SetRTPCValue("Volumen_general", newVolume);
    }
    public static void ChangeMusicVolume(float newVolume)
    {
        AkSoundEngine.SetRTPCValue("Volumen_musica", newVolume);
    }
    public static void ChangeFXVolume(float newVolume)
    {
        AkSoundEngine.SetRTPCValue("Volumen_FX", newVolume);
    }

    //Musics
    public static void StartAutobattleMusic(GameObject audioSource)
    {
        AkSoundEngine.PostEvent("AutobattleMusic", audioSource);
    }
    public static void StartMenuMusic(GameObject audioSource)
    {
        AkSoundEngine.PostEvent("MenuMusic", audioSource);
    }
    public static void DisableAutobattleMusic(GameObject audioSource)
    {
        AkSoundEngine.PostEvent("GoToMenuButton", audioSource);
        AkSoundEngine.SetState("Autobattle", "None");
    }
    public static void DisableMenuMusic(GameObject audioSource)
    {
        AkSoundEngine.PostEvent("GoToAutobattleButton", audioSource);
        AkSoundEngine.SetState("Music", "None");
    }
    public static void SetAutobattleMusic()
    {
        AkSoundEngine.SetState("Autobattle", "Combat");
    }
    public static void SetVictoryMusic()
    {
        AkSoundEngine.SetState("Autobattle", "Win");
    }
    public static void SetIdleMusic()
    {
        AkSoundEngine.SetState("Music", "Idle");
    }
    public static void SetMenuMusic()
    {
        AkSoundEngine.SetState("Music", "Menu");
    }
}