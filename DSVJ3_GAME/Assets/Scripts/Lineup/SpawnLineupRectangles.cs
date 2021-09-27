using UnityEngine;
using UnityEngine.UI;

public class SpawnLineupRectangles : MonoBehaviour
{
    [SerializeField] float offsetX;
    [SerializeField] GameObject characterLineupAnchor;
    [SerializeField] GameObject characterLineupHover;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for(short i=0; i < player.GetComponent<Player>().characters.Count; i++)
        {
            Vector3 spawnLocation = new Vector3((characterLineupAnchor.transform.position.x * i) + offsetX, 0);
            GameObject CharacterHolder = Instantiate(characterLineupHover, spawnLocation, Quaternion.identity, transform);
            CharacterHolder.GetComponent<Image>().sprite = player.GetComponent<Player>().characters[i].so.sprite;
        }
    }
}
