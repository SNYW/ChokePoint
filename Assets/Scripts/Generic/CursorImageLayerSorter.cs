using UnityEngine;

public class CursorImageLayerSorter : MonoBehaviour
{
    public Grid grid;
    private void LateUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = (grid.LocalToCell(mousePosition));
        GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)(0 - gridPos.x) - gridPos.y;
    }
}
