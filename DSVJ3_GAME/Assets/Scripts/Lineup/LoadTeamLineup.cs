using UnityEngine;
using UnityEngine.UI;

public class LoadTeamLineup : MonoBehaviour
{
    [SerializeField] float offsetX;
    [SerializeField] GameObject characterLineupAnchor;
    [SerializeField] GameObject teamLineup;

    void Awake()
    {
        Player player = Player.Get();

        float sizeX = GetComponent<RectTransform>().rect.width / 5;
        float sizeY = GetComponent<RectTransform>().rect.height;

        for (short i = 0; i < player.lineup.Length; i++)
        {
            //Instanciate objects
            GameObject characterHover = Instantiate(teamLineup, Vector3.zero, Quaternion.identity, transform);
            Rect rectTransform = characterHover.GetComponent<RectTransform>().rect;

            //Modify box prefab
            rectTransform.width = sizeX;
            rectTransform.height = sizeY;
            characterHover.transform.localScale = Vector3.one;

            //Move box to Location
            Vector3 spawnLocation = new Vector3(characterLineupAnchor.transform.position.x + (rectTransform.width / 2) + (rectTransform.width * i), transform.position.y);
            characterHover.transform.position = spawnLocation;

            BoxCollider2D box = characterHover.AddComponent<BoxCollider2D>();
            box.size = new Vector2(sizeX, sizeY);

            if (player.lineup[i].so != null)
            {
                characterHover.GetComponent<Image>().sprite = player.lineup[i].so.sprite;
                characterHover.GetComponent<SwapLineupSpot>().SetPosInArray(i);
            }
            characterHover.tag = "Character Team Holder";
        }
        transform.gameObject.GetComponent<LoadTeamLineup>().enabled = false;
    }
}
