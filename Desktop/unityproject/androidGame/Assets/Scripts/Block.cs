using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Limite inférieure après laquelle le bloc sera détruit
    private float destroyThresholdY = -6f;
   
    // Update is called once per frame
    void Update()
    {
        // Vérifie si la position Y du bloc est en dessous du seuil
        if (transform.position.y < destroyThresholdY)
        {
            Destroy(gameObject); // Détruit l'objet quand il est trop bas
        }
    }
}
