﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsCount : MonoBehaviour
{

    public GameObject BluePlayer;
    public GameObject RedPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BluePlayer")
        {
            if(BluePlayer != null)
                BluePlayer.GetComponent<NeuralController>().hitTheBall++;
        }
        else
        {
            if (other.gameObject.tag == "RedPlayer")
            {
                if (RedPlayer != null)
                    RedPlayer.GetComponent<NeuralController>().hitTheBall++;
            }
        }
    }

}
