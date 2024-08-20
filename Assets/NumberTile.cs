using TMPro;
using UnityEngine;

public class NumberTile : MonoBehaviour
{
    public int rowValue;
    public int columnValue;
    public int number;

    public void SetUpNumberText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();
    }

    [ContextMenu("Log Is Any Neighbor Tile Empty")]
    public void LogIsAnyNeighborTileEmpty()
    {
        var arrayWidth = TileManager.Instance.numberTileArray.GetLength(0) - 1;
        int bottomValue = -1;
        int topValue = -1;
        int rightValue = -1;
        int leftValue = -1;
        if (rowValue - 1 >= 0)
        {
            leftValue = TileManager.Instance.numberTileArray[rowValue - 1, columnValue].number;
        }
        if (rowValue + 1 <= arrayWidth)
        {
            rightValue = TileManager.Instance.numberTileArray[rowValue + 1, columnValue].number;
        }
        if (columnValue + 1 <= arrayWidth)
        {
            topValue = TileManager.Instance.numberTileArray[rowValue, columnValue + 1].number;
        }
        if (columnValue - 1 >= 0)
        {
            bottomValue = TileManager.Instance.numberTileArray[rowValue, columnValue - 1].number;
        }
        bool isAnyNeighborTileEmpty = bottomValue * topValue * rightValue * leftValue == 0;

        Debug.Log($"isAnyNeighborTileEmpty: {isAnyNeighborTileEmpty}");
    }

    [ContextMenu("Log Neighbor Tile Values")]
    private void LogNeighborTileValues()
    {
        var arrayWidth = TileManager.Instance.numberTileArray.GetLength(0) - 1;

        if (rowValue - 1 >= 0)
        {
            var leftNeighbor = TileManager.Instance.numberTileArray[rowValue - 1, columnValue];
            Debug.Log($"left: {leftNeighbor.number}");
        }
        if (rowValue + 1 <= arrayWidth)
        {
            var rightNeighbor = TileManager.Instance.numberTileArray[rowValue + 1, columnValue];
            Debug.Log($"right: {rightNeighbor.number}");
        }
        if (columnValue + 1 <= arrayWidth)
        {
            var topNeighbor = TileManager.Instance.numberTileArray[rowValue, columnValue + 1];
            Debug.Log($"top: {topNeighbor.number}");
        }
        if (columnValue - 1 >= 0)
        {
            var bottomNeighbor = TileManager.Instance.numberTileArray[rowValue, columnValue - 1];
            Debug.Log($"bottom: {bottomNeighbor.number}");
        }
    }
}
