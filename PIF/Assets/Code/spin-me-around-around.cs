using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
    [Header("Rotation : indiquez la rotation � appliquer autour de chaque axe (degr�e par seconde)")]
    public Vector3 rotationParSeconde;
    void Update()
    => transform.Rotate(rotationParSeconde * Time.deltaTime);

}
