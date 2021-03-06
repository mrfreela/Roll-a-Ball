using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
	public Text countText;
	public Text winText;
	public float jumpPower;
	public AudioSource jumpSound;
	public AudioSource landSound;

	
	private bool isGrounded;
	private Rigidbody rb;
	private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
		
    }

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
			
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 8)
		{
			winText.text = "You Win!";
		}
	}
	

	private void OnCollisionStay(Collision collision) {
		if (!collision.gameObject.CompareTag("Ground")){
			return;
		}
		isGrounded = true;
	}
	
	
	private void Update(){
		if (Input.GetButton("Jump") && isGrounded){
			rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
			isGrounded = false;
			jumpSound.Play();
		}
	}
	
}