using UnityEngine;

public class NeighborRelation : MonoBehaviour
{
    private NumberTile leftNeighbor;
    private NumberTile rightNeighbor;
    private NumberTile topNeighbor;
    private NumberTile bottomNeighbor;
    public NumberTile ReturnEmptyCellIfPossible(int rowValue, int columnValue)
    {
        leftNeighbor = new();
        rightNeighbor = new();
        topNeighbor = new();
        bottomNeighbor = new();

        var numberTileArray = TileManager.Instance.numberTileArray;


        var arrayWidth = numberTileArray.GetLength(0) - 1;
        if (rowValue - 1 >= 0)
        {
            leftNeighbor = ProcessGivenValuesAndGetEmptyIfPossible(rowValue - 1, columnValue, true, -1);
        }
        if (rowValue + 1 <= arrayWidth)
        {
            rightNeighbor = ProcessGivenValuesAndGetEmptyIfPossible(rowValue + 1, columnValue, true, 1);
        }
        if (columnValue + 1 <= arrayWidth)
        {
            topNeighbor = ProcessGivenValuesAndGetEmptyIfPossible(rowValue, columnValue + 1, false, 1);
        }
        if (columnValue - 1 >= 0)
        {
            bottomNeighbor = ProcessGivenValuesAndGetEmptyIfPossible(rowValue, columnValue - 1, false, -1);
        }
        NumberTile[] neighbors = new NumberTile[] { leftNeighbor, rightNeighbor, topNeighbor, bottomNeighbor };
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] != null && neighbors[i].number == 0)
            {
                return neighbors[i];
            }
        }
        return null;
    }

    private NumberTile ProcessGivenValuesAndGetEmptyIfPossible(int rowValue, int columnValue, bool isItRow, int operation)
    {
        var numberTileArray = TileManager.Instance.numberTileArray;
        var arrayWidth = numberTileArray.GetLength(0) - 1;

        if (numberTileArray[rowValue, columnValue].number == 0)
        {
            return numberTileArray[rowValue, columnValue];
        }

        if (isItRow)
        {
            if (arrayWidth <= rowValue + operation || rowValue + operation < 0)
            {
                return null;
            }
            return ProcessGivenValuesAndGetEmptyIfPossible(rowValue + operation, columnValue, isItRow, operation);
        }
        else
        {
            if (arrayWidth <= columnValue + operation || columnValue + operation < 0)
            {
                return null;
            }
            return ProcessGivenValuesAndGetEmptyIfPossible(rowValue, columnValue + operation, isItRow, operation);
        }
    }

}
