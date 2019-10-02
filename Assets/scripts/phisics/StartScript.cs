using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public Vector2 StartingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = StartingSpeed;
        DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, StartingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
