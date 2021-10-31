using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    private GameObject levelInfoCanvas;

    //Per gestire i punti accumulati nel livello
    public GameObject pointText;
    public int levelPoints = 0;
    public float timeRemaining = 10;


    // Start is called before the first frame update
    void Start()
    {
        //Per accedere al canvas relativo alle info del livello
        levelInfoCanvas = transform.Find("LevelInfoCanvas").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //Per modificare il valore del testo relativo ai punti accumulati nel livello (DA RIMUOVERE CONTATORE)
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        pointText.GetComponent<TMP_Text>().text = timeRemaining.ToString("0") + " " + "PT";
    }
}
