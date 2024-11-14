using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutlineGenerator : MonoBehaviour
{
    private GameObject lastObject = null;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (lastObject != hit.collider.gameObject)
            {
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10f);
                Destroy(lastObject.GetComponent<Outline>());
                lastObject = hit.collider.gameObject;
                lastObject.AddComponent<Outline>();
            }
        }
        else
        {
            Destroy(lastObject.GetComponent<Outline>());
            lastObject = null;
        }
    }
}
