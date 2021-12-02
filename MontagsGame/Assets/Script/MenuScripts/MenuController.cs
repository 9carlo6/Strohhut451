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

    //Per gestire il sessionController
    public GameObject sessionController;
    public SessionController sc;

    // Start is called before the first frame update
    void Awake()
    {
        sessionController = GameObject.FindWithTag("SessionController");
        sc = sessionController.GetComponent<SessionController>();

        coins = sc.coins;
        gameItems = sc.gameItems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
