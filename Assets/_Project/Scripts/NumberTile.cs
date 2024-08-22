using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

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

    private void MoveTileToEmptyArea(NumberTile emptyTile)
    {
        Debug.Log($"empty tile {emptyTile}");
        if (emptyTile == null)
        {
            Debug.Log("empty tile is null");
            return;
        }

        isClickable = false;
        int tempRowValue = emptyTile.rowValue;
        int tempColumnValue = emptyTile.columnValue;

        emptyTile.rowValue = rowValue;
        emptyTile.columnValue = columnValue;

        rowValue = tempRowValue;
        columnValue = tempColumnValue;

        var rectTransform = GetComponent<RectTransform>();
        Vector3 tempPosition = rectTransform.position;

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
        MoveTileToEmptyArea(NeighborRelation.ReturnEmptyCellIfPossible(rowValue, columnValue));
    }
}