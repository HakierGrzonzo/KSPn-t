using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Orbits;
    public static Double GravityConst() => 6.674 * Math.Pow(10f, -11f);
    public const int PredictCount = 50;
    public GameObject PredictBody;
    private GameObject[] Predictions = new GameObject[PredictCount];
    void Start()
    {

    }

    private Vector2 GetDistVector(GameObject Target)
    {
        return -Target.GetComponent<Transform>().position + Orbits.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    private Vector2 GravityForce(GameObject Target)
    {
        Vector2 Force;
        Double calc;
        calc = Orbits.GetComponent<Rigidbody2D>().mass * Target.GetComponent<Rigidbody2D>().mass * GravityConst() * 1000000;
        Force = GetDistVector(Target);
        calc /= Force.sqrMagnitude;
        Force.Normalize();
        return Force * (float)calc;
    }
    void FixedUpdate()
    {
        //Debug.Log(GravityForce(gameObject));
        gameObject.GetComponent<Rigidbody2D>().AddForce(GravityForce(gameObject), ForceMode2D.Force);
        DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, GravityForce(gameObject));
        DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Rigidbody2D>().velocity);
        PredictPath();
    }

    private void PredictPath()
    {
        for (int i = 0; i < PredictCount; i++)
        {
           Destroy(Predictions[i]);
        }
        MakePrediction();
    }

    private void MakePrediction()
    {

        Vector2 velocity0 = gameObject.GetComponent<Rigidbody2D>().velocity;
        float TimeStep = 0.05f;
        Vector2 accel = GravityForce(gameObject) * TimeStep * TimeStep /(2*gameObject.GetComponent<Rigidbody2D>().mass);
        //Debug.Log(accel);
        Vector2 position = velocity0 * TimeStep + accel;
        Predictions[0] = Instantiate(PredictBody, position + (Vector2) gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
        for (int i = 1; i < PredictCount; i++)
        {
            velocity0 = position;
            accel = GravityForce(Predictions[i-1]) * (TimeStep *TimeStep /(2*gameObject.GetComponent<Rigidbody2D>().mass));
            position = velocity0 + accel;
            Predictions[i] = Instantiate(PredictBody, position + (Vector2) Predictions[i-1].GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
            Debug.Log(accel);
        }
    }

}
