using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;

public class MenuController : MonoBehaviour
{
    public Coins coins;
    public GameItems gameItems;

   

    //Json
    public TextAsset gameItemsTextJSON;
    public TextAsset coinTextJSON;

    // Start is called before the first frame update
    void Awake()
    {
        coins = JsonUtility.FromJson<Coins>(coinTextJSON.text);
        gameItems = JsonUtility.FromJson<GameItems>(gameItemsTextJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
