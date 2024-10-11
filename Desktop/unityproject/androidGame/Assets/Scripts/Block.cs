using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Limite inf�rieure apr�s laquelle le bloc sera d�truit
    private float destroyThresholdY = -6f;
   
    // Update is called once per frame
    void Update()
    {
        // V�rifie si la position Y du bloc est en dessous du seuil
        if (transform.position.y < destroyThresholdY)
        {
            Destroy(gameObject); // D�truit l'objet quand il est trop bas
        }
    }
}
