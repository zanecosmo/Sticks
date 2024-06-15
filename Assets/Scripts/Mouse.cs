using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    [SerializeField] Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldspacePoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(worldspacePoint.x, worldspacePoint.y, 0);
    }
}
