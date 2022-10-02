using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Cam pointed at the player
    public Transform camFocus;

    public List<GameObject> objInRange;

    // Start is called before the first frame update
    void Start()
    {
        objInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        transform.RotateAround(transform.position, Vector3.up, hor);

        float ver = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        // Rot lock hacks
        Vector3 euler = camFocus.localRotation.eulerAngles;
        euler.x -= ver;
        euler.x = Mathf.Clamp(euler.x, 0, 80);
        camFocus.localRotation = Quaternion.Euler(euler);


        // Raycast objects
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask(new string[] { "Selectable" });

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, mask))
        {
            GameObject obj = hit.transform.gameObject;

            if (objInRange.Contains(obj))
            {
                Debug.Log("Scanning valid object");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objInRange.Remove(other.gameObject);
    }
}
