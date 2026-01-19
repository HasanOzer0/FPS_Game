using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        // Bu obje 5 saniye sonra kendini yok etsin
        Destroy(gameObject, 5f);
    }
}
