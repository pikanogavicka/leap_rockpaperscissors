using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Exit_btn : MonoBehaviour {

  private GameObject exit_txt;

  public void Start()
  {
    exit_txt = GameObject.Find("exit_txt");
    exit_txt.GetComponent<Text>().enabled = false;
  }

  public void Update()
  {
    if (Input.GetKey("escape"))
    {
      Application.Quit();
    }
  }

  public void MouseOver()
    {
      exit_txt.GetComponent<Text>().enabled = true;
      Debug.Log("Mouse is over btn.");
    }

    public void MouseExit()
    {
      exit_txt.GetComponent<Text>().enabled = false;
      Debug.Log("Mouse is no longer on btn.");
    }

    public void MouseDown()
    {
      Debug.Log("Click!");
      Application.Quit();
    }
}
