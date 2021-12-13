using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFeatures;


public class Telescope : MonoBehaviour
{
    public PlayerController playerController;

    //Per la gestione del numero di skull correnti
    [HideInInspector] public GameObject levelController;
    [HideInInspector] public LevelController lc;

    void Awake()
    {
        levelController = GameObject.FindWithTag("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    public void EnableEffect()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (lc.sc.telescopes_amount > 0 && (bool)((playerController.features)[HumanFeature.FeatureType.FT_INCREASED_FOV]).currentValue==false)
        {
            //qua ci va un modificatore non una modifica alla feature

            playerController.addModifier(lc.modifiersjson.getModifierbyCID("telescope"));
            FindObjectOfType<AudioManager>().Play("DropItem");



            //((playerController.features)[HumanFeature.FeatureType.FT_INCREASED_FOV]).currentValue = true;

            //Per gestire l'aggiornamento dell'ammontare dei telescopi posseduti
            lc.sc.telescopes_amount--;
            lc.UpdateGameItemsAmountText();

            //Per gestire il timer del telescope
            lc.HandleTelescopeTimer(playerController);
        }
        else
        {
            Debug.Log("Il giocatore non possiede Telescopi o ne sta gia usando uno ");
        }
    }
}
