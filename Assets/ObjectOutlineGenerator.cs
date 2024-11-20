using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutlineGenerator : MonoBehaviour
{
    private GameObject lastObject = null;
    private int layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("InteractionableObject");
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, layerMask))
        {
            // 여기에 layermask 거꾸로 해서 날리고, 만약 없으면
            // OK, 근데 만약 layermask거꾸로 해서 뭔가 걸리면 사이에 오브젝트가 있으므로
            // 레이케스팅 실패
            
            
            if (lastObject != hit.collider.gameObject)
            {
                //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10f);
                if (lastObject != null)
                {
                    Destroy(lastObject.GetComponent<Outline>());
                }

                lastObject = hit.collider.gameObject;
                lastObject.AddComponent<Outline>();
            }
        }
        else
        {
            if (lastObject != null)
            {
                Destroy(lastObject.GetComponent<Outline>());
                lastObject = null;
            }
        }
    }
}
