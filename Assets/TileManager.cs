using UnityEngine;

public class TileManager : MonoBehaviour
{
    public NumberTile[,] numberTileArray;
    [SerializeField] private NumberTile numberTilePrefab;
    [SerializeField] private Transform canvasParentTransform;
    void Start()
    {
        SpawnNumberTiles();
    }

    private void SpawnNumberTiles()
    {
        numberTileArray = new NumberTile[4, 4];
        int[] uniqueNumbers = new int[16];

        for (int i = 0; i < uniqueNumbers.Length; i++)
        {
            uniqueNumbers[i] = i;
        }

        for (int i = 0; i < uniqueNumbers.Length; i++)
        {
            int temp = uniqueNumbers[i];
            int randomIndex = Random.Range(i, uniqueNumbers.Length);
            uniqueNumbers[i] = uniqueNumbers[randomIndex];
            uniqueNumbers[randomIndex] = temp;
        }

        int indexForUniqueNumbers = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                NumberTile numberTile = Instantiate(numberTilePrefab);
                numberTile.transform.SetParent(canvasParentTransform);
                numberTile.transform.localPosition = new Vector3(j * 100, i * 100, 0);
                numberTile.GetComponent<NumberTile>().rowValue = i;
                numberTile.GetComponent<NumberTile>().columnValue = j;
                numberTileArray[i, j] = numberTile.GetComponent<NumberTile>();
                numberTileArray[i, j].number = uniqueNumbers[indexForUniqueNumbers];
                indexForUniqueNumbers++;
                numberTileArray[i, j].SetUpNumberText();
            }
        }
    }
}
