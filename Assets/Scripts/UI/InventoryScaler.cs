using UnityEngine;
using UnityEngine.UI;

public class InventoryScaler : MonoBehaviour
{
    private void Start()
    {
        float width = GetComponent<RectTransform>().rect.width;
        float heihgt = GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2(width/6, heihgt / 5f);
        GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
