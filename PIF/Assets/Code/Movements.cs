using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour {
    [SerializeField] float speed = 0;
    [SerializeField]
    private KeyCode[] keys = // ideally read the keycodes from a config file instead
        {   KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
        };
    private Vector3[] directions =
        {   Vector3.forward,
            Vector3.left,
            Vector3.back,
            Vector3.right,
        };

    private void Update() {
        Move();
    }

    public void Move() {
        for (int i = 0;i < keys.Length;i++) {
            if (Input.GetKey(keys[i])) {
                transform.Translate(speed * Time.deltaTime * directions[i]);
            }
        }

    }

}
