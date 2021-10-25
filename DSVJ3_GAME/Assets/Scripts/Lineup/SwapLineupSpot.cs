using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapLineupSpot : MonoBehaviour, IPointerDownHandler
{
    private int positionOnArray;
    public Player player;

    private BattleCharacterData lineupChosenCharacter;

    private GameObject[] characterList;

    private void Start()
    {
        player = Player.Get();
        lineupChosenCharacter = null;
        characterList = GameObject.FindGameObjectsWithTag("Player Team List");
    }

    public void swapPositionOnArray(BattleCharacterData characterToSwap)
    {
        player.SwapPositions(positionOnArray, characterToSwap);
        this.GetComponent<Image>().sprite = player.lineup[positionOnArray].so.sprite;
        foreach (GameObject charater in characterList)
        {
            charater.GetComponent<DragDropScript>().SetCharacterToSwap(null);
        }
        this.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lineupChosenCharacter = player.lineup[positionOnArray];
        this.GetComponent<Image>().color = Color.green;
        foreach (GameObject charater in characterList)
        {
            charater.GetComponent<DragDropScript>().SetCharacterToSwap(this.gameObject);
        }
    }

    public void SetPosInArray(int val)
    {
        positionOnArray = val;
    }

    public Sprite GetSprite()
    {
        return player.lineup[positionOnArray].so.sprite;
    }
}
