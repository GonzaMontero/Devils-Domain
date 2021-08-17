using UnityEngine;

public class PlayerClickManager : MonoBehaviour
{
    private LayerMask roomLayer;

    void Start()
    {
        roomLayer = LayerMask.GetMask("Rooms");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Rooms"))
            {
                Debug.Log("You clicked an object lmao");
                //hit.transform.GetComponent<UpgradeRoom>().Upgrade()
            }
        }
    }
}
