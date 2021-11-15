using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;


public class CoinsCanvasHandler : MonoBehaviour
{
    public Coins coins;

    //Per gestire le monete correnti
    public TextAsset coinTextJSON;
    public TMP_Text normal_coin_text;
    public TMP_Text roger_coin_text;

    void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        coins = JsonUtility.FromJson<Coins>(coinTextJSON.text);
        normal_coin_text.text = coins.normal_coins.ToString() + " $";
        roger_coin_text.text = coins.roger_coins.ToString() + " R$";
    }

}
