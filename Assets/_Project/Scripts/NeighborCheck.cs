using UnityEngine;

public class NeighborRelation : MonoBehaviour
{
    public static NumberTile ReturnEmptyCellIfPossible(int rowValue, int columnValue)
    {
        var numberTileArray = TileManager.Instance.numberTileArray;

        NumberTile leftNeighbor = new NumberTile();
        NumberTile rightNeighbor = new NumberTile();
        NumberTile topNeighbor = new NumberTile();
        NumberTile bottomNeighbor = new NumberTile();

        var arrayWidth = numberTileArray.GetLength(0) - 1;
        if (rowValue - 1 >= 0)
            leftNeighbor = numberTileArray[rowValue - 1, columnValue];
        {
            Debug.Log($"left: {leftNeighbor.number}");
        }
        if (rowValue + 1 <= arrayWidth)
        {
            rightNeighbor = numberTileArray[rowValue + 1, columnValue];
            Debug.Log($"right: {rightNeighbor.number}");
        }
        if (columnValue + 1 <= arrayWidth)
        {
            topNeighbor = numberTileArray[rowValue, columnValue + 1];
            Debug.Log($"top: {topNeighbor.number}");
        }
        if (columnValue - 1 >= 0)
        {
            bottomNeighbor = numberTileArray[rowValue, columnValue - 1];
            Debug.Log($"bottom: {bottomNeighbor.number}");
        }
        NumberTile[] neighbors = new NumberTile[] { leftNeighbor, rightNeighbor, topNeighbor, bottomNeighbor };
        for (int i = 0; i < neighbors.Length; i++)
        {

            if (neighbors[i].number == 0 && neighbors[i] != null)
            {
                Debug.Log("empty cell is found and returning");
                return neighbors[i];
            }
        }
        Debug.Log("empty cell is not found");
        return null;
    }
}
