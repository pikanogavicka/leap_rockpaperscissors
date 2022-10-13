using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class knn : MonoBehaviour
{
    //K for k nearest neighbours
    public int K = 12;

    public GameObject pinky_a;
    public GameObject pinky_b;
    public GameObject pinky_c;
    public GameObject pinky_end;

    public GameObject index1;
    public GameObject index2;
    public GameObject index3;
    public GameObject index4;

    public GameObject middle1;
    public GameObject middle2;
    public GameObject middle3;
    public GameObject middle4;

    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;
    public GameObject ring4;

    private List<Hand> paperRot;
    private List<Hand> scissorsRot;
    private List<Hand> rockRot;

    float MatrixDiff(Quaternion rot1, Quaternion rot2) {
      float angle = Quaternion.Angle(rot1, rot2);
      return angle;
    }

    float HandDiff(Hand hand1, Hand hand2) {
      float[] distances = new float [15];

      Finger[] fingersArray = hand1.getFingers();
      for (int i=0; i < fingersArray.Length; i++) {
        Finger tmpFinger = fingersArray[i];
        Quaternion[] jointsArray = tmpFinger.getJoints();
        for (int j=0; j < jointsArray.Length; j++) {
          Quaternion tmpJoint = jointsArray[j];
          float tmpDistance = MatrixDiff(tmpJoint, hand2.getFingers()[i].getJoints()[j]);
          distances[i*j] = tmpDistance;
        }
      }

      //remove smallest and biggest element and sum the rest
      float distance = 0;
      float minDist = distances.Min();
      float maxDist = distances.Max();
      for (int i=0; i<distances.Length; i++) {
        distance += distances[i];
      }
      distance = distance - minDist - maxDist;
      return distance;
    }

    // distance is sum of differences between each joint rotations
    public int findClosest() {

      //define arrays to store the closest k elements
      float[] closestDist = new float[K];
      int[] closestType = new int[K];
      for (int i=0; i <K; i++) {
        closestDist[i] = float.MaxValue;
        closestType[i] = -1;
      }

      List<List<Hand>> allRotLists = new List<List<Hand>>();
      allRotLists.Add(paperRot);
      allRotLists.Add(scissorsRot);
      allRotLists.Add(rockRot);

      //get current player hand position
      Finger thumb = new Finger();
      thumb.joint1 = pinky_a.transform.rotation;
      thumb.joint2 = pinky_b.transform.rotation;
      thumb.joint3 = pinky_c.transform.rotation;
      thumb.joint4 = pinky_end.transform.rotation;
      Finger index = new Finger();
      index.joint1 = index1.transform.rotation;
      index.joint2 = index2.transform.rotation;
      index.joint3 = index3.transform.rotation;
      index.joint4 = index4.transform.rotation;
      Finger middle = new Finger();
      middle.joint1 = middle1.transform.rotation;
      middle.joint2 = middle2.transform.rotation;
      middle.joint3 = middle3.transform.rotation;
      middle.joint4 = middle4.transform.rotation;
      Finger ring = new Finger();
      ring.joint1 = ring1.transform.rotation;
      ring.joint2 = ring2.transform.rotation;
      ring.joint3 = ring3.transform.rotation;
      ring.joint4 = ring4.transform.rotation;
      Hand playerHand = new Hand();
      playerHand.thumb = thumb;
      playerHand.index = index;
      playerHand.middle = middle;
      playerHand.ring = ring;

    //type glede na zaporedje rotListov v allRotLists, ne spreminji zaporedja!
    int type = 0;
      //loop thorugh list for each type
      foreach (List<Hand> rotList in allRotLists) {
        //loop through each finger position for each type
        foreach (Hand hand in rotList) {
        //loop through the buffer of last buffer_size player hands
          float tmpDistance = HandDiff(hand, playerHand);
          float tmpMax = closestDist.Max();
          if (tmpDistance < closestDist.Max())
          {
            int tmpIndex = closestDist.ToList().IndexOf(tmpMax);
            closestDist[tmpIndex] = tmpDistance;
            closestType[tmpIndex] = type;
          }
        }
        type += 1;
      }

      // type 0 = paper, type 1 = scissors, type 2 = rock
      int paper = 0;
      int scissors = 0;
      int rock = 0;
      for (int i=0; i<K; i++) {
        if (closestType[i] == 0) paper++;
        else if (closestType[i] == 1) scissors++;
        else rock++;
      }

      if (paper > scissors && paper > rock) return 0;
      else if (scissors > rock) return 1;
      else return 2;
    }

    float str2float(string str) {
      return float.Parse(str, System.Globalization.CultureInfo.InvariantCulture);
    }

    void csv2QuatList(string path, List<Hand> handList) {
      using(var reader = new StreamReader(path))
      {
        while (!reader.EndOfStream)
        {
          var line = reader.ReadLine();
          var values = line.Split(',');

          List<Quaternion> tmpJointsList = new List<Quaternion>();
          List<Finger> tmpFingersList = new List<Finger>();
          for (int i=0; i < values.Length / 4; i++) {
            Quaternion tmpJoint = new Quaternion(str2float(values[i * 4]), str2float(values[i * 4 +1]), str2float(values[i * 4 +2]), str2float(values[i * 4+3]));
            tmpJointsList.Add(tmpJoint);

            if ((i+1) % 4 == 0 && i != 0) {
              Finger tmpFinger = new Finger();
              tmpFinger.joint1 = tmpJointsList[0];
              tmpFinger.joint2 = tmpJointsList[1];
              tmpFinger.joint3 = tmpJointsList[2];
              tmpFinger.joint4 = tmpJointsList[3];
              tmpFingersList.Add(tmpFinger);
              tmpJointsList = new List<Quaternion>();
            }
          }

          if (tmpFingersList.Count == 4)
          {
            Hand hand = new Hand();
            hand.thumb = tmpFingersList[3];
            hand.index = tmpFingersList[0];
            hand.middle = tmpFingersList[1];
            hand.ring = tmpFingersList[2];

            handList.Add(hand);
          }

        }
      }
      Debug.Log("Successfully imported file.");
    }

    // Start is called before the first frame update
    void Start()
    {
      //upload csv files with pre-recorded rotations
      paperRot = new List<Hand>();
      string paperPath = Path.Combine(Application.streamingAssetsPath, "papir2.csv");
      csv2QuatList(paperPath, paperRot);

      scissorsRot = new List<Hand>();
      string scissorsPath = Path.Combine(Application.streamingAssetsPath, "skarje2.csv");
      csv2QuatList(scissorsPath, scissorsRot);

      rockRot = new List<Hand>();
      string rockPath = Path.Combine(Application.streamingAssetsPath, "kamen2.csv");
      csv2QuatList(rockPath, rockRot);

    }

}
