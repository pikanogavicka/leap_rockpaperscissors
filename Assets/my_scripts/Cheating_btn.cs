using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cheating_btn : MonoBehaviour
{

  public GameObject cheating_txt;
  private Gameplay gameplay_script;
  public GameObject white_fire;
  public GameObject blue_fire;

  public void Start()
  {
    //cheating_txt = GameObject.Find("cheating_txt");
    cheating_txt.GetComponent<Text>().enabled = false;
    gameplay_script = GameObject.Find("scripts_object").GetComponent<Gameplay>();

    //white_fire = GameObject.Find("fire_white");
    //blue_fire = GameObject.Find("fire_blue");

    if (gameplay_script.cheating)
    {
      cheating_txt.GetComponent<Text>().text = "cheating on";
      blue_fire.SetActive(true);
      white_fire.SetActive(false);
    }
    else
    {
      cheating_txt.GetComponent<Text>().text = "cheating off";
      white_fire.SetActive(true);
      blue_fire.SetActive(false);
    }
  }

  public void MouseOver()
  {
    cheating_txt.GetComponent<Text>().enabled = true;
  }

  public void MouseExit()
  {
    cheating_txt.GetComponent<Text>().enabled = false;
  }

  public void MouseDown()
  {
    Debug.Log("Click!");
    gameplay_script.cheating = !gameplay_script.cheating;

    if (gameplay_script.cheating)
    {
      cheating_txt.GetComponent<Text>().text = "cheating on";
      blue_fire.SetActive(true);
      white_fire.SetActive(false);
    }
    else
    {
      cheating_txt.GetComponent<Text>().text = "cheating off";
      white_fire.SetActive(true);
      blue_fire.SetActive(false);
    }

  }
}
