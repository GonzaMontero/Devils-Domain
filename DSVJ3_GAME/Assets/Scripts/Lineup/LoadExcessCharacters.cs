using UnityEngine;
using UnityEngine.UI;

public class LoadExcessCharacters : MonoBehaviour
{
    [SerializeField] GameObject[] excessImages;
    [SerializeField] Sprite emptyFace;

    Player player;

    private void Awake()
    {
        player = Player.Get();

        for (short i = 0; i < excessImages.Length; i++)
        {
            if (player.characters.Count > i)
            {
                if (player.characters[i].so != null)
                {
                    excessImages[i].GetComponent<Image>().sprite = player.characters[i].so.lineupFaceSprite;                    
                }
                else
                {
                    excessImages[i].GetComponent<Image>().sprite = emptyFace;
                }
            }
            else
            {
                excessImages[i].GetComponent<Image>().sprite = emptyFace;
            }
            excessImages[i].GetComponentInParent<SwapCharacterButton>().GiveSlotOnList(i);
        }
        this.gameObject.SetActive(false);
    }
}
