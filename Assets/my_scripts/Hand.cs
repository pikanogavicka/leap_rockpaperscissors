using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

  public Finger thumb { get; set; }
  public Finger index { get; set; }
  public Finger middle { get; set; }
  public Finger ring { get; set; }
  // public Finger pinky { get; set; }

  public Finger[] getFingers() {
    //Finger[] fingersArray = {thumb, index, middle, ring, pinky};
    Finger[] fingersArray = {thumb, index, middle, ring};
    return fingersArray;
  }

}
