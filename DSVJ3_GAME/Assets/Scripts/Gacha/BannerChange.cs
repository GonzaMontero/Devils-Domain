using UnityEngine;

public class BannerChange : MonoBehaviour
{
    [SerializeField] GameObject[] banners;
    private int currentBannerRank;
    private void Start()
    {
        currentBannerRank = 0;
    }
    public void LeftClick()
    {
        banners[currentBannerRank].SetActive(false);

        currentBannerRank--;
        if (currentBannerRank < 0)
            currentBannerRank = 2;

        banners[currentBannerRank].SetActive(true);
    }
    public void RightClick()
    {
        banners[currentBannerRank].SetActive(false);

        currentBannerRank++;
        if (currentBannerRank > 2)
            currentBannerRank = 0;

        banners[currentBannerRank].SetActive(true);
    }
}
