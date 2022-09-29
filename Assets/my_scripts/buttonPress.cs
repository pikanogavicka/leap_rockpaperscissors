//In this example, the name of the GameObject that collides with your GameObject is output to the console. Then this checks the name of the Collider and if it matches with the one you specify, it outputs another message.

//Create a GameObject and make sure it has a Collider component. Attach this script to it.
//Create a second GameObject with a Collider and place it on top of the other GameObject to output that there was a collision. You can add movement to the GameObject to test more.

using UnityEngine;

public class buttonPress : MonoBehaviour {

  private Gameplay gp_script;

    //If your GameObject starts to collide with another GameObject with a Collider
    void OnTriggerEnter(Collider other)
    {
    //Output the Collider's GameObject's name
      Debug.Log(other.name);
      if (other.name == "L_index_end") {
        //Output the message
        Debug.Log("Click click!");
        gp_script.startBtnClick();
                
        }
      }


  private void Start()
  {
    gp_script = FindObjectOfType(typeof(Gameplay)) as Gameplay;
    Debug.Log("aaaaaaaaaaaaa");
  }

  //If your GameObject keeps colliding with another GameObject with a Collider, do something
  //void OnCollisionStay(Collision collision)
  //     {
  //         //Check to see if the Collider's name is "Chest"
  //         if (collision.collider.name == "bone3")
  //         {
  //             //Output the message
  //             //Debug.Log("Click click!");
  //         }
  //     }
}
