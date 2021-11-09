using UnityEngine;
using UnityEngine.UI;

public class LoadExcessCharacters : MonoBehaviour
{
    [SerializeField] GameObject[] excessImages;
    [SerializeField] Sprite emptyFace;

    Player player;

    private void Start()
    {
        player = Player.Get();

        for(short i = 0; i < excessImages.Length; i++)
        {
            if (player.characters[i].so != null)
            {
                excessImages[i].GetComponent<Image>().sprite = player.characters[i].so.lineupFaceSprite;
            }
            else if(player.characters.Count < i)
            {
                excessImages[i].GetComponent<Image>().sprite = emptyFace;
            }
            else
            {
                excessImages[i].GetComponent<Image>().sprite = emptyFace;
            }
        }
    }
}
