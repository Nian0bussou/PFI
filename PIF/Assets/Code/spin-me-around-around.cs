using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
    [Header("Rotation : indiquez la rotation à appliquer autour de chaque axe (degrée par seconde)")]
    public Vector3 rotationParSeconde;
    void Update()
    => transform.Rotate(rotationParSeconde * Time.deltaTime);

}
