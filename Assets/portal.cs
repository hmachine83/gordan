using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour {

    public GameObject dje;
    public GameObject pla;
    public void ajde()
    {
        pla.transform.position = dje.transform.position;
        pla.gameObject.GetComponent<player>().destination = dje.transform.position;
    }

}
