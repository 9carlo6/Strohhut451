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
    //Per gestire le monete correnti
    public TMP_Text normal_coin_text;
    public TMP_Text roger_coin_text;

    //Per gestire il sessionController
    public GameObject sessionController;
    public SessionController sc;

    // Start is called before the first frame update
    void Awake()
    {
        sessionController = GameObject.FindWithTag("SessionController");
        sc = sessionController.GetComponent<SessionController>();
    }

    void Update()
    {
        LoadData();
    }

    public void LoadData()
    {
        normal_coin_text.text = sc.coins.normal_coins.ToString() + " $";
        roger_coin_text.text = sc.coins.roger_coins.ToString() + " R$";
    }

}
