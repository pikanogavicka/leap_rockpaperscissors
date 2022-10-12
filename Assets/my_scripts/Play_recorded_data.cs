using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class Play_recorded_data : MonoBehaviour
{
  public string oldFileName = "kamen2.csv";
  public string newFileName = "kamen_filtered.csv";

  private List<Hand> rotations;
  private List<int> newRotations = new List<int>();
  private int currIndex = 0;

  public GameObject index_a;
  public GameObject index_b;
  public GameObject index_c;
  public GameObject index_end;

  public GameObject middle_a;
  public GameObject middle_b;
  public GameObject middle_c;
  public GameObject middle_end;

  public GameObject ring_a;
  public GameObject ring_b;
  public GameObject ring_c;
  public GameObject ring_end;

  public GameObject pinky_a;
  public GameObject pinky_b;
  public GameObject pinky_c;
  public GameObject pinky_end;

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
        Debug.Log(values.Length);

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
          hand.thumb = tmpFingersList[3];
          hand.index = tmpFingersList[0];
          hand.middle = tmpFingersList[1];
          hand.ring = tmpFingersList[2];

          handList.Add(hand);
        }

      }
    }
    Debug.Log(rotations.Count);
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
    pinky_a.transform.rotation = currHand.thumb.joint1;
    pinky_b.transform.rotation = currHand.thumb.joint2;
    pinky_c.transform.rotation = currHand.thumb.joint3;
    pinky_end.transform.rotation = currHand.thumb.joint4;

    index_a.transform.rotation = currHand.index.joint1;
    index_b.transform.rotation = currHand.index.joint2;
    index_c.transform.rotation = currHand.index.joint3;
    index_end.transform.rotation = currHand.index.joint4;

    middle_a.transform.rotation = currHand.middle.joint1;
    middle_b.transform.rotation = currHand.middle.joint2;
    middle_c.transform.rotation = currHand.middle.joint3;
    middle_end.transform.rotation = currHand.middle.joint4;

    ring_a.transform.rotation = currHand.ring.joint1;
    ring_b.transform.rotation = currHand.ring.joint2;
    ring_c.transform.rotation = currHand.ring.joint3;
    ring_end.transform.rotation = currHand.ring.joint4;

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
      if (accRotIndex < accRot.Count) {
        toAddIndex = accRot[accRotIndex];
        //if the rotation was accepted
        if (i == toAddIndex) {
          Hand currHand = rotations[i];
          Finger[] currFingers = currHand.getFingers();
          //loop through all the fingers on the hand
          for (int j = 0; j < currFingers.Length; j++)
          {
            Quaternion[] currJoints = currFingers[j].getJoints();
            //loop through all the joints in a finger
            for (int k = 0; k < currJoints.Length; k++)
            {
              string tmp = currJoints[k].ToString().Replace("(", "").Replace(")", "");
              Debug.Log(tmp);
              accData += tmp;

              //if it is last joint on the last finger than add new line
              if (j == currFingers.Length - 1 && k == currJoints.Length - 1)
              {
                accData += "\n";
              }
              //else add a seperating coma between values
              else
              {
                accData += ",";
              }
            }
          }
          //go to next accepted rotation index
          accRotIndex++;
        }
      }
    }

    writer.WriteLine(accData);
    writer.Close();

    //Re-import the file to update the reference in the editor
    //AssetDatabase.ImportAsset(path);
    //TextAsset asset = (TextAsset)Resources.Load(newFileName.Split('.')[0]);

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
