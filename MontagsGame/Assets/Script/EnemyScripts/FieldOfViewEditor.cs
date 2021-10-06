using UnityEditor;
using UnityEngine;

//Tutto quello che è nella cartella Editor non verrà compilato al lancio del gioco

[CustomEditor(typeof(EnemyIA))] //quando è attivo EnemyIA questo editor funzionerà

public class FieldOfViewEditor : Editor
{ 
    //OnSceneGUI consente all'editor di gestire un evento nella visualizzazione della scena
    private void OnSceneGUI()
    {
        EnemyIA fov = (EnemyIA)target;
        Handles.color = Color.white;   //GUI di controllo e disegno 3D nella scena. 

        //Ci costruiamo il raggio
        //DrawWireArc disegna un arco circolare con questi parametri
        //centro, normale del cerchio(asse y), la direzione del punto sulla circonferenza del cerchio relativo al centro dove iniziano gli archi (asse z)
        //l'angolo dell'arco(360), il raggio del cerchio
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        //Aggiungiamo l'angolo di visuale
        //Creiamo due vettori, uno per il lato sinistro dell'angolo, uno per il destro
        //stiamo convertendo un angolo in una direzione (un vettore)
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

    //Vogliamo ottenere una direzione a partire dall'angolo
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        //aggiungiamo l'angolo di eulero Y all'angolo in gradi
        angleInDegrees += eulerY;

        //ritorniamo un nuovo vettore con
        //seno dell'angolo in radianti, y = 0, coseno dell'angolo in radianti
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
