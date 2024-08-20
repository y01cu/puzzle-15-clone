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
}
