using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{

  public Quaternion joint1 { get; set; }
  public Quaternion joint2 { get; set; }
  public Quaternion joint3 { get; set; }
  public Quaternion joint4 { get; set; }

  public Quaternion[] getJoints() {
    Quaternion[] jointsArray = {joint1, joint2, joint3, joint4};
    return jointsArray;
  }
}
