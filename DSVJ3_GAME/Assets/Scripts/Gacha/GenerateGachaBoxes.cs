using UnityEngine;

public class GenerateGachaBoxes : MonoBehaviour
{
    [SerializeField] GameObject gachaHolderPrefab;
    [SerializeField] GameObject gachaAnchor;

    private void OnEnable()
    {
            float sizeX = GetComponent<RectTransform>().rect.width / 11;
            float sizeY = GetComponent<RectTransform>().rect.height;

            for (short i = 0; i < 4; i++)
            {
                //Instanciate objects
                GameObject gachaBox = Instantiate(gachaHolderPrefab, Vector3.zero, Quaternion.identity, transform);
                Rect rectTransform = gachaBox.GetComponent<RectTransform>().rect;

                //Modify box prefab
                rectTransform.width = sizeX;
                rectTransform.height = sizeY;
                gachaBox.transform.localScale = Vector3.one;

                //Move box to Location
                Vector3 spawnLocation = new Vector3((gachaAnchor.transform.position.x + (rectTransform.width / 2) + (rectTransform.width * i)), transform.position.y);
                gachaBox.transform.position = spawnLocation;

                gachaBox.transform.tag = "Gacha Boxes";
            }
        transform.gameObject.GetComponent<GenerateGachaBoxes>().enabled = false;
    }
}
