using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Play_recorded_data : MonoBehaviour
{
  public string oldFileName = "kamen.csv";
  public string newFileName = "kamen_filtered.csv";

  private List<Hand> rotations;
  private List<int> newRotations = new List<int>();
  private int currIndex = 0;


  public GameObject thumb1;
  public GameObject thumb2;
  public GameObject thumb3;
  public GameObject thumb4;

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

  float str2float(string str)
  {
    return float.Parse(str, System.Globalization.CultureInfo.InvariantCulture);
  }

  void csv2QuatList(string path, List<Hand> handList)
  {
    using (var reader = new StreamReader(path))
    {
      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        var values = line.Split(',');

        List<Quaternion> tmpJointsList = new List<Quaternion>();
        List<Finger> tmpFingersList = new List<Finger>();
        for (int i = 0; i < values.Length / 4; i++)
        {
          Quaternion tmpJoint = new Quaternion(str2float(values[i * 4]), str2float(values[i * 4 + 1]), str2float(values[i * 4 + 2]), str2float(values[i * 4 + 3]));
          tmpJointsList.Add(tmpJoint);

          if ((i + 1) % 4 == 0 && i != 0)
          {
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
          hand.thumb = tmpFingersList[0];
          hand.index = tmpFingersList[1];
          hand.middle = tmpFingersList[2];
          hand.ring = tmpFingersList[3];

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
    rotations = new List<Hand>();
    string oldPath = Path.Combine(Application.streamingAssetsPath, oldFileName);
    csv2QuatList(oldPath, rotations);
    showNextAngle();
  }

  void showNextAngle()
  {
    Debug.Log(currIndex);
    Hand currHand = rotations[currIndex];
    thumb1.transform.rotation = currHand.thumb.joint1;
    thumb2.transform.rotation = currHand.thumb.joint2;
    thumb3.transform.rotation = currHand.thumb.joint3;
    thumb4.transform.rotation = currHand.thumb.joint4;

    index1.transform.rotation = currHand.index.joint1;
    index2.transform.rotation = currHand.index.joint2;
    index3.transform.rotation = currHand.index.joint3;
    index4.transform.rotation = currHand.index.joint4;

    middle1.transform.rotation = currHand.middle.joint1;
    middle2.transform.rotation = currHand.middle.joint2;
    middle3.transform.rotation = currHand.middle.joint3;
    middle4.transform.rotation = currHand.middle.joint4;

    ring1.transform.rotation = currHand.ring.joint1;
    ring2.transform.rotation = currHand.ring.joint2;
    ring3.transform.rotation = currHand.ring.joint3;
    ring4.transform.rotation = currHand.ring.joint4;

  }

  void save_to_file(List<int> accRot)
  {

    //string variableNames = "t1x, t1y, t1z, t1w";
    string path = Path.Combine(Application.streamingAssetsPath, newFileName);
    StreamWriter writer = new StreamWriter(path, true);
    //writer.WriteLine(variableNames);

    //accepted rotation index for going through the accepted rotation array
    int accRotIndex = 0;
    //index in the original array of rotations of the accepted rotation
    int toAddIndex = accRot[accRotIndex];
    //string of accepted rotations to write to a file
    string accData = "";

    //going through the original rotations array
    for (int i = 0; i < rotations.Count; i++) {
      //if the rotation was accepted
      if (i == toAddIndex) {
        Hand currHand = rotations[i];
        Finger [] currFingers = currHand.getFingers();
        //loop through all the fingers on the hand
        for (int j = 0; j < currFingers.Length; j++) {
          Quaternion[] currJoints = currFingers[j].getJoints();
          //loop through all the joints in a finger
          for (int k = 0; k < currJoints.Length; k++) {
            accData += currJoints[k];

            //if it is last joint on the last finger than add new line
            if ( j == currFingers.Length - 1 && k == currJoints.Length - 1) {
              accData += "\n";
            }
            //else add a seperating coma between values
            else {
              accData += ",";
            }
          }
        }
        writer.WriteLine(accData);
        accData = "";
        //go to next accepted rotation index
        accRotIndex++;
        toAddIndex = accRot[accRotIndex];
      }
    }

    writer.Close();

    //Re-import the file to update the reference in the editor
    AssetDatabase.ImportAsset(path);
    TextAsset asset = (TextAsset)Resources.Load(newFileName.Split('.')[0]);

    Debug.Log("Successfully wrote to file.");
  }

  public void onAccept()
  {
    newRotations.Add(currIndex);

    currIndex++;
    if (currIndex < rotations.Count)
    {
      showNextAngle();
    }
    else
    {
      save_to_file(newRotations);
    }

  }

  public void onDecline()
  {
    currIndex++;
    if (currIndex < rotations.Count)
    {
      showNextAngle();
    }
    else
    {
      save_to_file(newRotations);
    }
  }
}
