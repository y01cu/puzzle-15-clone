using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    public NumberTile[,] numberTileArray;
    [SerializeField] private NumberTile numberTilePrefab;
    [SerializeField] private Transform canvasParentTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        SpawnAndAdjustNumberTiles();
    }

    private void SpawnAndAdjustNumberTiles()
    {
        // Initiations of number tile and unique value arrays
        numberTileArray = new NumberTile[4, 4];
        int[] uniqueNumbers = new int[16];

        for (int i = 0; i < uniqueNumbers.Length; i++)
        {
            uniqueNumbers[i] = i;
        }

        // Shuffling the unique numbers array
        for (int i = 0; i < uniqueNumbers.Length; i++)
        {
            int temp = uniqueNumbers[i];
            int randomIndex = Random.Range(i, uniqueNumbers.Length);
            uniqueNumbers[i] = uniqueNumbers[randomIndex];
            uniqueNumbers[randomIndex] = temp;
        }

        // Spawning the number tiles with unique 
        int indexForUniqueNumbers = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                NumberTile numberTile = Instantiate(numberTilePrefab);
                numberTile.transform.SetParent(canvasParentTransform);
                numberTile.transform.localPosition = new Vector3(i * 100, j * 100, 0);
                numberTile.GetComponent<NumberTile>().rowValue = i;
                numberTile.GetComponent<NumberTile>().columnValue = j;
                numberTileArray[i, j] = numberTile.GetComponent<NumberTile>();
                numberTileArray[i, j].rowValue = i;
                numberTileArray[i, j].columnValue = j;
                numberTileArray[i, j].number = uniqueNumbers[indexForUniqueNumbers];
                indexForUniqueNumbers++;
                if (numberTileArray[i, j].number == 0)
                {
                    numberTileArray[i, j].GetComponent<Image>().enabled = false;
                    numberTileArray[i, j].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                    continue;
                }
                numberTileArray[i, j].SetUpNumberText();
                numberTile.gameObject.name = $"NumberTile {numberTile.number}";
            }
        }
    }

    public void LogArrayContent()
    {
        for (int i = 0; i < numberTileArray.GetLength(0); i++)
        {
            for (int j = 0; j < numberTileArray.GetLength(1); j++)
            {
                Debug.Log($"numberTileArray[{i},{j}]: {numberTileArray[i, j].number}");
            }
        }
    }
}
