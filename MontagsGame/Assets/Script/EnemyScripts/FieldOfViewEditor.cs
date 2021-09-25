using UnityEditor;
using UnityEngine;

//Tutto quello che è nella cartella Editor non verrà compilato al lancio del gioco

[CustomEditor(typeof(EnemyIA))] //quando è attivo fieldOfView questo editor funzionerà

public class FieldOfViewEditor : Editor
{ 
    private void OnSceneGUI()
    {
        EnemyIA fov = (EnemyIA)target; //dobbiamo trasmettere il target di questo editor a FieldOfView
        Handles.color = Color.white;    //settiamo il colore del raggio a bianco
        //tracciamo prima il raggio di visuale
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        //aggiungiamo l'angolo di visuale
        //creiamo due vettori, uno per il lato sinistro dell'angolo, uno per il destro
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2); //fratto due perchè prenderà metà dell'angolo a destra e metà a sinistra
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2);

        //tracciamo l'angolo di visuale
        Handles.color = Color.yellow;
        //dal centro del nemico, dal centro + l'angolo di visuale 1 moltiplicato per il raggio
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.viewRadius);
        //dal centro del nemico, dal centro + l'angolo di visuale 2 moltiplicato per il raggio
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.viewRadius);


        if (fov.playerInSightRange)
        {
            Handles.color = Color.green;
            //quando il player è nel tracciamo una linea verde che parte dalla posizione del nemico alla posizione del player
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }


    //PlayerManager.instance.player.transform
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        //aggiungiamo l'euleroY all'angolo in gradi
        angleInDegrees += eulerY;

        //ritorniamo un nuovo vettore con
        //seno dell'angolo in in radianti, y = 0,  coseno dell'angolo in radianti
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
