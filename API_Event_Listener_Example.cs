using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit; // the api being used

public class PlayerStats : MonoBehaviour
{
 
    [SerializeField]
    public bool detectLife = false;

    // refernce to API object
    RangeSensor rangeSens = null;

    // the documentation lists the events as:
    
    // SensorEventHandler OnDetected            (Invoked when a new GameObject is detected)
    // SensorEventHandler OnLostDetection       (Invoked when a GameObject that was detected is no longer detected)
    
    // Our 'RangeSensor' object 'rangeSens' has access to these two 'SensorEventHandler' from its parent class,
    // the events are already invoked at the required time, we just need to add listeners.

    
    void Start()
    {
        // get the object
        rangeSens = GetComponent<RangeSensor>();
        
        // listen for 'on lost detection' events
        // - Method to execute (lostDetection) must match signature of event, in this case a sensor and an object (declared in method below)
        // - notice the method doesnt pass any explicit parameters, as they are implicit, object comes from object causing event,
        // - and sensor is 'rangeSens'
        rangeSens.OnLostDetection.AddListener(lostDetection);

        //listen for 'on detected' events
        rangeSens.OnDetected.AddListener(Detection);
        
    }

    // methods accepting required parameters, notice only one is made use of, but both are required
    // to ensure signatures match
    void lostDetection(GameObject o, Sensor s)
    {
        Outline x = o.GetComponent<Outline>();
        x.enabled = false;
    }

    void Detection(GameObject o, Sensor s)
    {
        if (detectLife)
        {
            Outline x = o.GetComponent<Outline>();
            x.enabled = true;
        }
        else
        {
            Outline x = o.GetComponent<Outline>();
            x.enabled = false;
        }
    }







}
