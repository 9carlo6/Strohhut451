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
    public MenuController mc;

    //Per gestire le monete correnti
    public TMP_Text normal_coin_text;
    public TMP_Text roger_coin_text;

    void Update()
    {
        LoadData();
    }

    public void LoadData()
    {
        normal_coin_text.text = mc.coins.normal_coins.ToString() + " $";
        roger_coin_text.text = mc.coins.roger_coins.ToString() + " R$";
    }

}
