using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
	[SerializeField] Player player;
    [SerializeField] Slider generalVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    //Unity Events
    private void Start()
    {
        player = Player.Get();
        SetSliders();
    }

    //Methods
    public void DeleteData()
    {
        player.DeleteData();
    }
    void SetSliders()
    {
        generalVolumeSlider.value = player.settings.generalVolume;
        musicVolumeSlider.value = player.settings.musicVolume;
        soundVolumeSlider.value = player.settings.fxVolume;
    }

    //Event Receivers
    public void OnGeneralVolumeChange()
    {
        player.settings.generalVolume = generalVolumeSlider.value;
        AudioManager.ChangeGeneralVolume(player.settings.generalVolume);
    }
    public void OnSoundVolumeChange()
    {
        player.settings.fxVolume = soundVolumeSlider.value;
        AudioManager.ChangeFXVolume(player.settings.fxVolume);
    }
    public void OnMusicVolumeChange()
    {
        player.settings.musicVolume = musicVolumeSlider.value;
        AudioManager.ChangeMusicVolume(player.settings.musicVolume);
    }
}