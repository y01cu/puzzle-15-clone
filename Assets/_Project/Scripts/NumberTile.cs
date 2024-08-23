using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class NumberTile : MonoBehaviour, IPointerDownHandler
{
    public static event EventHandler OnTileMoved;
    private EmptyTileSearch emptyTileSearch = new();
    public int rowValue;
    public int columnValue;
    public int number;
    public int edgeValue;

    private void Start()
    {
        edgeValue = Screen.width / 4;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(edgeValue, edgeValue);
    }
    public void SetUpNumberText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();
    }

    public void MoveTileToEmptyArea(NumberTile emptyTile)
    {
        var numberTileArray = TileManager.Instance.numberTileArray;
        if (emptyTile == null)
        {
            Debug.Log("empty tile is null");
            return;
        }
        int rowDifference = rowValue - emptyTile.rowValue > 0 ? 1 : -1;
        int columnDifference = columnValue - emptyTile.columnValue > 0 ? 1 : -1;

        if (Mathf.Abs(rowDifference) > 1)
        {
            numberTileArray[rowValue + rowDifference, columnValue].MoveTileToEmptyArea(emptyTile);
            var newEmptyTile = numberTileArray[rowValue + rowDifference, columnValue];
            MoveTileToEmptyArea(newEmptyTile);
        }
        else if (Mathf.Abs(columnDifference) > 1)
        {
            numberTileArray[rowValue, columnValue + columnDifference].MoveTileToEmptyArea(emptyTile);
            var newEmptyTile = numberTileArray[rowValue, columnValue + columnDifference];
            MoveTileToEmptyArea(newEmptyTile);
        }

        int tempRowValue = emptyTile.rowValue;
        int tempColumnValue = emptyTile.columnValue;

        emptyTile.rowValue = rowValue;
        emptyTile.columnValue = columnValue;

        rowValue = tempRowValue;
        columnValue = tempColumnValue;

        var rectTransform = GetComponent<RectTransform>();
        Vector3 tempPosition = rectTransform.position;

        rectTransform.DOMove(emptyTile.GetComponent<RectTransform>().position, 0.2f);
        emptyTile.GetComponent<RectTransform>().position = tempPosition;

        numberTileArray[rowValue, columnValue] = this;
        numberTileArray[emptyTile.rowValue, emptyTile.columnValue] = emptyTile;
        emptyTile.number = 0;
        OnTileMoved?.Invoke(this, EventArgs.Empty);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateTiles();
    }

    private void UpdateTiles()
    {
        var numberTileArray = TileManager.Instance.numberTileArray;
        var foundEmptyTile = emptyTileSearch.TryFinding(this);
        if (foundEmptyTile == null)
        {
            Debug.Log("empty tile is not found");
            return;
        }

        int rowDifference = rowValue - foundEmptyTile.rowValue > 0 ? 1 : -1;
        int columnDifference = columnValue - foundEmptyTile.columnValue > 0 ? 1 : -1;

        if (rowValue - foundEmptyTile.rowValue != 0)
        {
            if (rowDifference > 0)
            {
                while (foundEmptyTile.rowValue <= rowValue)
                {
                    numberTileArray[foundEmptyTile.rowValue + 1, columnValue].MoveTileToEmptyArea(foundEmptyTile);
                }
            }
            else
            {
                while (foundEmptyTile.rowValue >= rowValue)
                {
                    numberTileArray[foundEmptyTile.rowValue - 1, columnValue].MoveTileToEmptyArea(foundEmptyTile);
                }
            }
        }
        if (columnValue - foundEmptyTile.columnValue != 0)
        {
            if (columnDifference > 0)
            {
                while (foundEmptyTile.columnValue <= columnValue)
                {
                    numberTileArray[rowValue, foundEmptyTile.columnValue + 1].MoveTileToEmptyArea(foundEmptyTile);
                }
            }
            else
            {
                while (foundEmptyTile.columnValue >= columnValue)
                {
                    numberTileArray[rowValue, foundEmptyTile.columnValue - 1].MoveTileToEmptyArea(foundEmptyTile);
                }
            }
        }
    }
}