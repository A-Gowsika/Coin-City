using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class CubeController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float scaleSpeed;
    public Vector3 scaleDirection;
    private int score;
    private int health;

    public TextMeshProUGUI ScoreTextObj;
    public TextMeshProUGUI HealthTextObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      score=0;
      ScoreTextObj.text="Score: "+score;

      health = 5;
      HealthTextObj.text="Health: "+health;
      
    }

    // Update is called once per frame
    void Update()
    {

      
      //wasPressedThisFrame,wasReleasedThisFrame;
      if(Keyboard.current.wKey.isPressed)
         transform.Translate(moveSpeed * Vector3.up*Time.deltaTime);
         if(Keyboard.current.sKey.isPressed)
         transform.Translate(moveSpeed * Vector3.down*Time.deltaTime);
         if(Keyboard.current.aKey.isPressed)
         transform.Translate(moveSpeed * Vector3.left*Time.deltaTime);
         if(Keyboard.current.dKey.isPressed)
         transform.Translate(moveSpeed * Vector3.right*Time.deltaTime);

      //if(Keyboard.current.upArrowKey.wasPressedThisFrame)
         //transform.Translate(moveSpeed * Vector3.forward*Time.deltaTime);
       // Debug.Log("Updated kowsi");
       //Debug.Log(transform.position);
      // transform.position += moveSpeed * new Vector3(0, 0, -1)* Time.deltaTime;
      // transform.Translate(moveSpeed * new Vector3(0, 0, 1))* Time.deltaTime;
       //transform.Translate(moveSpeed * Vector3.up * Time.deltaTime);
      // transform.Rotate(rotateSpeed* Vector3.up*Time.deltaTime);
      // transform.localScale += scaleSpeed * scaleDirection * Time.deltaTime;
    
    }
private void OnCollisionEnter(Collision collision){
    
   // Debug.Log(collision.gameObject.name);

   if(collision.gameObject.tag=="Enemies"){
      health--;
      HealthTextObj.text="Health: "+health;
      //Debug.Log("Health: " + health);
      if(health <= 0){
         Destroy(gameObject);
      }
   }
}
private void OnTriggerEnter(Collider other){
   if(other.gameObject.tag == "Coins"){
      score++;
       ScoreTextObj.text="Score: "+score;
      ///Debug.Log("Score: " + score);
      Destroy(other.gameObject);
   }
}
}
