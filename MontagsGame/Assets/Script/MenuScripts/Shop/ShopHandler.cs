using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;

public class ShopHandler : MonoBehaviour
{
    public GameItems gameItems;

    //Json
    public TextAsset gameItemsTextJSON;
    public TextAsset coinTextJSON;

    //Per gestire l'ammontare dei gameItem nella pagina principale dello shop
    public TMP_Text skulls_amount_text;
    public TMP_Text helms_amount_text;
    public TMP_Text telescopes_amount_text;

    //Per gestire valori del gameItem nelle pagine deidicate ai singoli gameItem
    public string selected_gameItem;
    public TMP_Text gameItem_amount_text;

    //Per gestire il numero di gameItem da comprare
    public float current_amount;
    public TMP_Text gameItem_current_amount_text;

    //Per gestire il valore secondo le due valute
    public TMP_Text normal_coins_price_text;
    public TMP_Text roger_coins_price_text;

    //Per gestire le monete
    public Coins coins;

    //Per gestire i Form
    public GameObject YesAndNoForm;
    public GameObject NoMoneyForm;
    public GameObject SingleGameItemShopForm;

    //Per gestire con quale moneta si paga
    public bool isNormalCoinSelected;

    void Awake()
    {
        //Per gestire l'ammontare corrente dei gameItems
        gameItems = JsonUtility.FromJson<GameItems>(gameItemsTextJSON.text);
        skulls_amount_text.text = "x " + gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "skull").amount.ToString();
        helms_amount_text.text = "x " + gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "helm").amount.ToString();
        telescopes_amount_text.text = "x " + gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "telescope").amount.ToString();

        //Per gestire l'ammontare corrente delle monete
        coins = JsonUtility.FromJson<Coins>(coinTextJSON.text);
    }

    //Funzione necessaria per settare i parametri nella pagina dedicata al singolo item
    public void SelectGameItem(string gameItem_name)
    {
        selected_gameItem = gameItem_name;

        //Gestione dell'ammontare del gameItem selezionato
        if (gameItem_name.Contains("skull")) gameItem_amount_text.text = skulls_amount_text.text;
        if (gameItem_name.Contains("helm")) gameItem_amount_text.text = helms_amount_text.text;
        if (gameItem_name.Contains("telescope")) gameItem_amount_text.text = telescopes_amount_text.text;

        //Gestione valore corrente del numero di gameItem da comprare
        current_amount = 1;
        gameItem_current_amount_text.text = current_amount.ToString();

        //Gestione del valore secondo le due valute
        normal_coins_price_text.text = gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).normal_coins_value.ToString();
        roger_coins_price_text.text = gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).roger_coins_value.ToString();
    }

    //Gestione incremento numero di gameItem da comprare
    public void AddValue()
    {
        if(current_amount < 20)
        {
            current_amount++;
            gameItem_current_amount_text.text = current_amount.ToString();

            //Aggiornamento prezzi secondo le due valute
            normal_coins_price_text.text = (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).normal_coins_value * current_amount).ToString();
            roger_coins_price_text.text = (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).roger_coins_value * current_amount).ToString();
        }
    }

    //Gestione decremento numero di gameItem da comprare
    public void RemoveValue()
    {
        if (current_amount > 1)
        {
            current_amount--;
            gameItem_current_amount_text.text = current_amount.ToString();

            //Aggiornamento prezzi secondo le due valute
            normal_coins_price_text.text = (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).normal_coins_value * current_amount).ToString();
            roger_coins_price_text.text = (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).roger_coins_value * current_amount).ToString();
        }
    }

    //Funzione richiamata quando si cerca di comprare con le monete normali
    public void BuyWithNormalCoins()
    {
        isNormalCoinSelected = true;

        if (coins.normal_coins >= Convert.ToSingle(normal_coins_price_text.text))
        {
            //Mostra il form per fare il check
            SingleGameItemShopForm.SetActive(false);
            YesAndNoForm.SetActive(true);
        }
        else
        {
            //Mostra il form per dire che non ci sono soldi
            SingleGameItemShopForm.SetActive(false);
            NoMoneyForm.SetActive(true);
        }
    }

    //Funzione richiamata quando si cerca di comprare con i roger coin
    public void BuyWithRogerCoins()
    {
        isNormalCoinSelected = false;

        if (coins.roger_coins >= Convert.ToSingle(roger_coins_price_text.text))
        {
            //Mostra il form per fare il check
            SingleGameItemShopForm.SetActive(false);
            YesAndNoForm.SetActive(true);
        }
        else
        {
            //Mostra il form per dire che non ci sono soldi
            SingleGameItemShopForm.SetActive(false);
            NoMoneyForm.SetActive(true);
        }
    }

    //Funzione necessaria per sovrascrivere i file json contenente i dati sulle monete e quelli sul numero di oggetti
    public void UpdateData()
    {
        //Gestione Monete
        if (isNormalCoinSelected)
        {
            coins.normal_coins = coins.normal_coins - Convert.ToSingle(normal_coins_price_text.text);
        }
        else
        {
            coins.roger_coins = coins.roger_coins - Convert.ToSingle(roger_coins_price_text.text);
        }

        string coins_json = JsonUtility.ToJson(coins);
        File.WriteAllText("Assets/Push-To-Data/Coins.txt", coins_json);


        //Gestione Oggetti
        gameItems.gameItems_list.FirstOrDefault(gi => gi.name == selected_gameItem).amount += current_amount;
        string gameItems_json = JsonUtility.ToJson(gameItems);
        File.WriteAllText("Assets/Push-To-Data/GameItems.txt", gameItems_json);
    }


}
