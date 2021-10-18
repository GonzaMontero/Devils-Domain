using UnityEngine;
using UnityEngine.UI;

public class SpawnLineupRectangles : MonoBehaviour
{
    [SerializeField] float offsetX;
    [SerializeField] GameObject characterLineupAnchor;
    [SerializeField] GameObject characterLineupHover;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player player1 = player.GetComponent<Player>();

        float sizeX = GetComponent<RectTransform>().rect.width / 7;
        float sizeY = GetComponent<RectTransform>().rect.height;

        for (short i = 0; i < player1.characters.Count; i++)
        {
            //Instanciate objects
            GameObject characterHover = Instantiate(characterLineupHover, Vector3.zero, Quaternion.identity, transform);
            Rect rectTransform = characterHover.GetComponent<RectTransform>().rect;

            //Modify box prefab
            rectTransform.width = sizeX;
            rectTransform.height = sizeY;
            characterHover.transform.localScale = Vector3.one;

            //Move box to Location
            Vector3 spawnLocation = new Vector3(characterLineupAnchor.transform.position.x + (rectTransform.width / 2) + (rectTransform.width * i), transform.position.y);
            characterHover.transform.position = spawnLocation;

            if (characterHover != null)
            {
                characterHover.GetComponent<Image>().sprite = player1.characters[i].so.sprite;
                characterHover.GetComponent<DragDropScript>().SetValues(spawnLocation, i);
            }
        }
        transform.gameObject.GetComponent<SpawnLineupRectangles>().enabled = false;
    }
}
