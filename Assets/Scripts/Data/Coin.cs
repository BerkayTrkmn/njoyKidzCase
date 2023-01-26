using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Toplana paralar karakter özelliğinde(Inherited)
public class Coin : Character
{
    [SerializeField] private int order;
    [SerializeField] private TextMeshPro orderText;
    public int Order { get => order; set { order = value; OrderText.text = NumberToRomanNumber(order); } }
    public TextMeshPro OrderText { get => orderText = transform.GetChild(0).GetComponent<TextMeshPro>();}

   
    public void SetOrderText(int _number)
    {
        orderText.text = NumberToRomanNumber(_number);
    }
    public string NumberToRomanNumber(int _number)
    {
        switch (_number)
        {
            case 1: return "I";
            case 2: return "II";
            case 3: return "III";
            case 4: return "IV";
            case 5: return "V";
        }
        return _number.ToString();
    }

}
