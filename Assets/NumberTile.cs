using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Numerics;

public class NumberTile : MonoBehaviour, IPointerDownHandler
{
    public int rowValue;
    public int columnValue;
    public int number;

    private bool isClickable = true;

    public void SetUpNumberText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();
    }

    private void LogIsAnyNeighborTileEmpty()
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

        Debug.Log($"isAnyNeighborTileEmpty: {isAnyNeighborTileEmpty} | b: {bottomValue} t: {topValue} r: {rightValue} l: {leftValue}");
    }

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

    private void MoveToEmptyCell()
    {
        if (!isClickable)
        {
            return;
        }

        var numberTileArray = TileManager.Instance.numberTileArray;
        var arrayWidth = numberTileArray.GetLength(0) - 1;
        // Vector3 emptyCellPosition = initialTestValue;
        NumberTile emptyTile = null;

        int bottomValue = -1;
        int topValue = -1;
        int rightValue = -1;
        int leftValue = -1;
        if (rowValue - 1 >= 0)
        {
            leftValue = numberTileArray[rowValue - 1, columnValue].number;
            if (leftValue == 0)
            {
                emptyTile = numberTileArray[rowValue - 1, columnValue];
            }
        }
        if (rowValue + 1 <= arrayWidth)
        {
            rightValue = numberTileArray[rowValue + 1, columnValue].number;
            if (rightValue == 0)
            {
                emptyTile = numberTileArray[rowValue + 1, columnValue];
            }
        }
        if (columnValue + 1 <= arrayWidth)
        {
            topValue = numberTileArray[rowValue, columnValue + 1].number;
            if (topValue == 0)
            {
                emptyTile = numberTileArray[rowValue, columnValue + 1];
            }
        }
        if (columnValue - 1 >= 0)
        {
            bottomValue = numberTileArray[rowValue, columnValue - 1].number;
            if (bottomValue == 0)
            {
                emptyTile = numberTileArray[rowValue, columnValue - 1];
            }
        }

        if (emptyTile != null)
        {
            ExchangeValues(emptyTile);
        }
    }

    private void ExchangeValues(NumberTile emptyTile)
    {
        isClickable = false;
        int tempRowValue = emptyTile.rowValue;
        int tempColumnValue = emptyTile.columnValue;

        emptyTile.rowValue = rowValue;
        emptyTile.columnValue = columnValue;

        rowValue = tempRowValue;
        columnValue = tempColumnValue;

        var rectTransform = GetComponent<RectTransform>();
        UnityEngine.Vector3 tempPosition = rectTransform.position;

        rectTransform.DOMove(emptyTile.GetComponent<RectTransform>().position, 0.2f).onComplete += () => isClickable = true;
        emptyTile.GetComponent<RectTransform>().position = tempPosition;

        var numberTileArray = TileManager.Instance.numberTileArray;
        numberTileArray[rowValue, columnValue] = this;
        numberTileArray[emptyTile.rowValue, emptyTile.columnValue] = emptyTile;
        emptyTile.number = 0;

        Debug.Log("new setup");
        TileManager.Instance.LogArrayContent();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        LogIsAnyNeighborTileEmpty();
        LogNeighborTileValues();
        MoveToEmptyCell();
    }
}