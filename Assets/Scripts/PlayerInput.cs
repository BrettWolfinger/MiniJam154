using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] GameObject aimingSprite;

    Vector3 mouseWorldPos;
    GameObject aimingSpriteInstance;
    Shooter shooter;

    void Awake() {
        shooter = GetComponent<Shooter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 1.0f;
        aimingSpriteInstance = Instantiate(aimingSprite,mouseWorldPos,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //draw marker at mouse position
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 1.0f;
        aimingSpriteInstance.transform.position = mouseWorldPos;
        if(Input.GetMouseButtonDown(0))
        {
            shooter.Fire(mouseWorldPos);
        }
    }
}
