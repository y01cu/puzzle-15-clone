using UnityEngine;

public class EmptyTileSearch
{
    private int arrayWidth;
    private NumberTile[,] numberTileArray;
    private NumberTile currentNumberTile;
    public NumberTile TryFinding(NumberTile givenNumberTile)
    {
        currentNumberTile = givenNumberTile;
        numberTileArray = TileManager.Instance.numberTileArray;
        arrayWidth = numberTileArray.GetLength(0) - 1;

        NumberTile foundEmptyTile = null;

        for (int i = currentNumberTile.columnValue; i <= arrayWidth; i++)
        {
            if (numberTileArray[currentNumberTile.rowValue, i].number == 0)
            {
                foundEmptyTile = numberTileArray[currentNumberTile.rowValue, i];
            }
        }
        for (int i = currentNumberTile.columnValue; i >= 0; i--)
        {
            if (numberTileArray[currentNumberTile.rowValue, i].number == 0)
            {
                foundEmptyTile = numberTileArray[currentNumberTile.rowValue, i];
            }
        }
        for (int i = currentNumberTile.rowValue; i <= arrayWidth; i++)
        {
            if (numberTileArray[i, currentNumberTile.columnValue].number == 0)
            {
                foundEmptyTile = numberTileArray[i, currentNumberTile.columnValue];
            }
        }
        for (int i = currentNumberTile.rowValue; i >= 0; i--)
        {
            if (numberTileArray[i, currentNumberTile.columnValue].number == 0)
            {
                foundEmptyTile = numberTileArray[i, currentNumberTile.columnValue];
            }
        }

        return foundEmptyTile;
    }
}
