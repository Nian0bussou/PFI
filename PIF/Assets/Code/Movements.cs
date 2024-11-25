using UnityEngine;
using System;
using UnityEngine.Events;

public class Movements : MonoBehaviour {
    [SerializeField] private TextAsset configFile;
    [SerializeField] float speed = 0;
    [SerializeField] Transform cameraTransform;
    private KeyCode[] keys = // default keybinds
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

    private KeyCode shoot = KeyCode.Space;
    [SerializeField] UnityEvent shootAct;

    private void Awake() {
        LoadKeyBindings();
    }

    private void Update() {
        Move();
        Actions();
    }

    void Actions() {
        if (Input.GetKey(shoot)) shootAct.Invoke();
    }



    void Move() {
        Vector3 movement = Vector3.zero;

        // Check for movement keys
        for (int i = 0;i < keys.Length;i++) {
            if (Input.GetKey(keys[i])) {
                movement += directions[i];
            }
        }

        if (movement != Vector3.zero) {
            // Normalize movement to prevent faster diagonal movement
            movement.Normalize();

            // Convert movement to camera-relative direction
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Ignore vertical components (if the game is 2D or flat)
            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate camera-relative movement direction
            Vector3 cameraRelativeMovement = ( cameraForward * movement.z + cameraRight * movement.x ).normalized;

            // Move the player
            transform.Translate(cameraRelativeMovement * speed * Time.deltaTime, Space.World);

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void LoadKeyBindings() {
        if (configFile == null) return;
        try {
            string json = configFile.text;
            ConfigData configData = JsonUtility.FromJson<ConfigData>(json);
            // Convert string keys to KeyCode
            keys = Array.ConvertAll(configData.keys, key => (KeyCode) Enum.Parse(typeof(KeyCode), key, true));
        } catch (Exception e) {
            Debug.LogError($"Error reading config file: {e.Message}");
        }
    }

    [Serializable]
    private class ConfigData {
        public string[] keys;
    }
}
