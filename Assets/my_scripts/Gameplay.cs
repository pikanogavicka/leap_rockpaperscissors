using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{

    public bool cheating = true;  

    public Text countdownTxt;
    public Text scoreTile;

    //texts that show which move the player and the bot did
    public Text botMoveTxt;
    public Text playerMoveTxt;

    public GameObject newGameBtn;
    public GameObject botHand;
    public float speedInSec = 1.0f;

    private float savedTime;
    private int countdownInt;
    private bool idleAnimation = true;
    private string[] options = new string[] {"paper", "scissors", "rock"};
    private string[] tekst_opcije = new string[] { "papir", "škarje", "kamen" };
    private string[] idle_options = new string[] { "come_here", "peace", "wave"};
    private knn knnScript;
    private Renderer MeshComponent;
    private Animator bothand_animator;
    //private Explode explode_script;

    /**void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (countdownInt == -1 && other.name == "L_index_end") {
            Debug.Log ("You have clicked the button with leap!");
            startBtnClick();
        }
    }**/

    public void startBtnClick(){
      playerMoveTxt.text = "";
      botMoveTxt.text = "";
      savedTime = Time.time - speedInSec/2;
      countdownInt = 3;
      idleAnimation = false;
      //toggle UI
      //explode_script.Main();
      //explode_script.buttonActive = false;
      newGameBtn.gameObject.SetActive(false);
      //newGameBtn.gameObject.GetComponent<MeshRenderer>().enabled = false;
      //MeshComponent.enabled = false;
      scoreTile.gameObject.SetActive(false);
      bothand_animator.SetInteger("boot_choice", 3);
      bothand_animator.Play("kamen_up_down");
      //Debug.Log(bothand_animator.GetInteger("boot_choice"));
    }

    string whoWins(int player, int bot) {
      if (player == bot) return "Izenačenje";
      else if ((player == 0 && bot == 2) ||
              (player == 1 && bot == 0) ||
              (player == 2 && bot == 1)) return "Zmagal si!";
      else return "Izgubil si!";
    }

    // Start is called before the first frame update
    void Start()
    {
      //Button btn = newGameBtn.GetComponent<Button>();
      //btn.onClick.AddListener(startBtnClick);
      countdownInt = -1;
      //MeshComponent = this.gameObject.GetComponentInChildren<Renderer>();
      bothand_animator = botHand.GetComponent<Animator>();
      bothand_animator.Play("wave");
      savedTime = Time.time;

      //explode_script = GameObject.Find("scripts_object").GetComponent<Explode>();
      knnScript = gameObject.GetComponent<knn>();

  }

    // Update is called once per frame
    void Update()
    {
      //start button was pressed so start the countdown
      if (countdownInt > 0) {
        if (Time.time - savedTime > speedInSec) {
          countdownTxt.text = "";
          countdownInt--;
          savedTime = Time.time;
        }
        else if (Time.time - savedTime > speedInSec/2) {
          if (countdownInt < 3) countdownTxt.text = countdownInt.ToString();
        }
      }
      //end of countdown, release the hounds
      if (countdownInt == 0){
        if (Time.time - savedTime > speedInSec/2) {
          //call knn to recognize the players move
          int playerMove = knnScript.findClosest();
          //display players move as text
          playerMoveTxt.text = tekst_opcije[playerMove];

          //choose a random move for the bot
          int botMove = Random.Range(0, options.Length);
          if (cheating)
          {
            botMove = (playerMove + 1) % options.Length;
          }
          //call the animation for the bot move
          bothand_animator.SetInteger("boot_choice", botMove);
          //bothand_animator.Play(options[botMove], 0, 0.5f);
          bothand_animator.CrossFade(options[botMove], 0.06f);

          countdownTxt.text = "";
          //display the bot move as text
          botMoveTxt.text = tekst_opcije[botMove];

          //check who won
          string scoreTxt = whoWins(playerMove, botMove);
          //display who won text
          scoreTile.gameObject.SetActive(true);
          scoreTile.GetComponentInChildren<Text>().text = scoreTxt;

          savedTime = Time.time;

          countdownInt--;
        }
        //to make the countdown text blink
        else if (Time.time - savedTime > speedInSec/2) {
          countdownTxt.text = "";
        }
      }

    // do the idle animation (before first game or between games)
    if (countdownInt == -1)
    {
      newGameBtn.gameObject.SetActive(true);
      //newGameBtn.gameObject.GetComponent<MeshRenderer>().enabled = true;
      //explode_script.buttonActive = true;

      if (Time.time - savedTime > 2.0f)
      {
        scoreTile.GetComponentInChildren<Text>().text = "   Igraj!   ";
      }

      //if 5 seconds has passed randomly pick a new idle animation
      if (Time.time - savedTime > 8.0f)
      {
        savedTime = Time.time;
        // pick a random animation out of three (peace, wave, come_here)
        int pickedAnim = Random.Range(0, idle_options.Length);
        bothand_animator.SetInteger("random_idle", pickedAnim);
        bothand_animator.CrossFade(idle_options[pickedAnim], 0.4f);
      }
    }
  }
}