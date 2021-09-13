using UnityEngine;
using UnityEngine.UI;

public class GachaClick : MonoBehaviour
{
    [SerializeField] Texture gachaBannerTexture;
    [SerializeField] RawImage gachaBannerImage;

    public void ModifyBannerImage()
    {
        gachaBannerImage.texture = gachaBannerTexture;
    }
}
