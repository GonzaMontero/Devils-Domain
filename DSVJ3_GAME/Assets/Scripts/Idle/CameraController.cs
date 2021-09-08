using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public System.Action ZoomingOut;
    [SerializeField] float transitionTimer;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Vector3 originalPosition;
    [SerializeField] RoomManager roomManager;

    bool isOnTransition = false;


    #region Unity Events
    private void Start()
    {
        originalPosition = Camera.main.transform.position; //uses global to maximize compatibilty with RoomClicked
        roomManager.RoomClicked += OnRoomClicked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ZoomOut();
        }
    }
    #endregion

    #region Methods
    IEnumerator UpdatePosition(Vector3 newPosition)
    {
        isOnTransition = true;
        Vector3 currentPosition = transform.position; //uses global to maximize compatibilty with RoomClicked
        float timer = 0f;

        do
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, newPosition, timer / transitionTimer);
            yield return null;
        } while (timer <= transitionTimer);

        isOnTransition = false;
        yield break;
    }
    void OnRoomClicked(RoomController roomController, int buildCost)
    {
        if (!isOnTransition)
        {
            Vector3 roomPosition = roomController.transform.position;
            StartCoroutine(UpdatePosition(roomPosition + cameraOffset));
        }
    }
    void ZoomOut()
    {
        if (isOnTransition)
        {
            StopAllCoroutines(); //check GLOBAL danger later
        }

        StartCoroutine(UpdatePosition(originalPosition));

        ZoomingOut.Invoke();
    }
    #endregion
}