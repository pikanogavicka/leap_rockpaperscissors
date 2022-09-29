/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class recordData : MonoBehaviour
{

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

    private string recordedData = "";
    private int counter = 0;
    private Quaternion thumb1Value = Quaternion.identity;
    private Quaternion index1Value = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {

        if (thumb1.transform.rotation != thumb1Value || index1.transform.rotation != index1Value)
        {

            recordedData = recordedData + thumb1.transform.rotation.x + "," + thumb1.transform.rotation.y + "," + thumb1.transform.rotation.z + "," + thumb1.transform.rotation.w ; //+ thumb2.transform.rotation + thumb3.transform.rotation;

            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + thumb2.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + thumb3.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++)
            {
              recordedData = recordedData + "," + thumb4.transform.rotation[i];
            }

            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + index1.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + index2.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + index3.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + index4.transform.rotation[i];
            }

            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle1.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle2.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle3.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + middle4.transform.rotation[i];
            }

            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring1.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring2.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring3.transform.rotation[i];
            }
            for (int i = 0; i < 4; i++) {
              recordedData = recordedData + "," + ring4.transform.rotation[i];
            }

            recordedData = recordedData + "\n";

            counter++;
            if (counter % 200 == 0)
            {
                //string variableNames = "t1x, t1y, t1z, t1w";
                string path = "Assets/my_scripts/kamen.csv";
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

        thumb1Value = thumb1.transform.rotation;
        index1Value = index1.transform.rotation;

    }
}
*/