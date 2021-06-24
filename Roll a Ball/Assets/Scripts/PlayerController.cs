using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {


	public float speed;
	public float jumpForce;
	public Text countText;
	public Text winText;
	public Text livesText;
	public Transform Ply;
	public Vector3 RespawnPos = new Vector3(0, 1, 0);


	private Rigidbody rb;
	private int count;
	private int levens;
	private int endgame;

	
	void Start ()
	{
		
		rb = GetComponent<Rigidbody>();

		endgame = 0;		

		count = 0;

		levens = 3;

		
		SetCountText ();
		SetLivesText();

		
		winText.text = "";
	}

	
	void FixedUpdate ()
	{
		if (endgame == 0)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");


			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


			rb.AddForce(movement * speed);
		}

	}
	void Update()
	{
		if (Ply.transform.position.x < 0 && Ply.transform.position.z < -11) {
			RespawnPos = new Vector3(0, 1, -15);
		}
		SetLivesText();
		if (Input.GetKeyDown(KeyCode.Space) && Ply.transform.position.y == 0.5)
		{
			Debug.Log("hi");
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
		if (Ply.transform.position.y < -3)
		{
			Ply.transform.position = RespawnPos;
			levens = levens - 1;
		}
	}


		void OnTriggerEnter(Collider other) 
	{

		if (other.gameObject.CompareTag ("Pick Up"))
		{
		
			other.gameObject.SetActive (false);

			count = count + 1;


			SetCountText ();
		}
	}

	void SetCountText()
	{

		countText.text = "Count: " + count.ToString ();

		if (count >= 12)
			if (endgame == 0)
			{
				{
					winText.text = "You Win!";
					endgame = 1;
				}
			}
	}
	void SetLivesText() {
		livesText.text = "Lives: " + levens.ToString();

		if (levens == 0)
			if (endgame == 0)
			{
				{
					winText.text = "You Lose";
					endgame = 1;
				}
			}
	}
}