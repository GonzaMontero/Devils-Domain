using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
	[SerializeField] Player player;
	[SerializeField] Slider volumeSlider;
	//[SerializeField] float volume = 100;

    //Unity Events
    private void Start()
    {
        player = Player.Get();
    }

    //Methods
    public void DeleteData()
    {
        player.DeleteData();
    }

    //Event Receivers
    public void OnVolumeChange()
    {
        Debug.Log(volumeSlider.value);
        //AkSoundEngine.SetRTPCValue("master_volume", newVolume); //TEMP, CHANGE MASTER VOLUME TO NEW NAME
    }
}