using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    public NumberTile[,] numberTileArray;
    [SerializeField] private NumberTile numberTilePrefab;
    [SerializeField] private Transform canvasParentTransform;
    public static event System.EventHandler OnGameWon;

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
    private void Start()
    {
        SpawnAndAdjustNumberTiles();
        NumberTile.OnTileMoved += CheckForWinningCondition;
        // CheckForWinningCondition();
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
        int edgeTileCount = 4;

        var edgeValue = Screen.width / edgeTileCount;
        for (int i = 0; i < edgeTileCount; i++)
        {
            for (int j = 0; j < edgeTileCount; j++)
            {
                NumberTile numberTile = Instantiate(numberTilePrefab);
                numberTile.transform.SetParent(canvasParentTransform);
                numberTile.transform.localPosition = new Vector3(i * edgeValue - edgeTileCount / 2 * edgeValue + edgeValue / 2, j * edgeValue - edgeTileCount / 2 * edgeValue + edgeValue / 2, 0);
                numberTile.transform.DOScale(transform.localScale, 0.5f).From(Vector3.zero);
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
                    numberTile.gameObject.name = "Empty Tile";
                    continue;
                }
                numberTileArray[i, j].SetUpNumberText();
                numberTile.gameObject.name = $"Number Tile {numberTile.number}";
            }
        }
    }

    public void ResetCurrentAndSpawnNewTiles()
    {
        foreach (var numberTile in numberTileArray)
        {
            Destroy(numberTile.gameObject);
        }
        SpawnAndAdjustNumberTiles();
    }

    private void CheckForWinningCondition(object sender, System.EventArgs e)
    {
        for (int i = numberTileArray.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < numberTileArray.GetLength(1); j++)
            {
                if (true)
                {
                    var validValue = (numberTileArray.GetLength(0) - 1 - i) * 4 + j + 1;
                    if (numberTileArray[i, j].number != validValue)
                    {
                        return;
                    }
                }
            }
        }
        Debug.Log("You won!");
        OnGameWon?.Invoke(this, System.EventArgs.Empty);
    }
}
