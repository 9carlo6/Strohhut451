using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;


public class GameItemsCanvasHandler : MonoBehaviour
{
    public GameItems gameItems;

    //Json
    public TextAsset gameItemsTextJSON;

    //Per gestire gli item correnti
    public TMP_Text skulls_amount_text;
    public TMP_Text helms_amount_text;
    public TMP_Text telescopes_amount_text;

    void Awake()
    {
        gameItems = JsonUtility.FromJson<GameItems>(gameItemsTextJSON.text);
        skulls_amount_text.text = gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "skull").amount.ToString();
        helms_amount_text.text = gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "helm").amount.ToString();
        telescopes_amount_text.text = gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "telescope").amount.ToString();
    }
}
