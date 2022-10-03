using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class recordData : MonoBehaviour
{
  public string filename;

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

  private string recordedData = "";
  private int counter = 0;
  private Quaternion middle_aValue = Quaternion.identity;
  private Quaternion index_aValue = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting recording...");
  }

    // Update is called once per frame
    void Update()
    {

        if (middle_a.transform.rotation != middle_aValue || index_a.transform.rotation != index_aValue)
        {

            recordedData = recordedData + index_a.transform.rotation.x + "," + index_a.transform.rotation.y + "," + index_a.transform.rotation.z + "," + index_a.transform.rotation.w ; //+ thumb2.transform.rotation + thumb3.transform.rotation;

            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + index_b.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + index_c.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + index_end.transform.rotation[i];
            }

      for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle_a.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle_b.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle_c.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle_end.transform.rotation[i];
            }

            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring_a.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring_b.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring_c.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring_end.transform.rotation[i];
            }

            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + pinky_a.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + pinky_b.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + pinky_c.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + pinky_end.transform.rotation[i];
            }

            recordedData = recordedData + "\n";

            counter++;
            if (counter % 300 == 0)
            {
                //string variableNames = "t1x, t1y, t1z, t1w";
                string path = Path.Combine(Application.streamingAssetsPath, filename);
                StreamWriter writer = new StreamWriter(path, true);
                //writer.WriteLine(variableNames);

                writer.WriteLine(recordedData);
                recordedData = "";
                writer.Close();

                //Re-import the file to update the reference in the editor
                AssetDatabase.ImportAsset(path);
                TextAsset asset = (TextAsset)Resources.Load("kamen");

                //Print the text from the file
                Debug.Log("saved");
            }
        }

        middle_aValue = middle_a.transform.rotation;
        index_aValue = index_a.transform.rotation;

    }
}