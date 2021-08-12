using UnityEngine;

public class Cell { }

public static class CellSelector
{
    public static Cell GetCellOnMousePosition()
    {
        RaycastHit cellHitted;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        if(Physics.Raycast(cameraRay, out cellHitted, 100, LayerMask.GetMask("Cells")))
        {
            return cellHitted.transform.GetComponent<Cell>();
        }

        return null;
    }
}