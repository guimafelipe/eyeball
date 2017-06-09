using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private enum skillPressed : int {skillQ, skillW, skillE, skillR, skillD, none};
	private enum stateOfSkill : int {waitingSkill, waitingAim};

	private int activeSkill;
	private int myState;
	private float t;
	[Range(0f,5f)]
	public float omega = 2.3f;
	[Range(0f,1f)]
	public float amp = 0.5f;
	[Range(0f,5f)]
	public float hSpeed;
	public int hp, maxHp;

	public bool isDead;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		isDead = false;
		t = 0;
		omega = 2.3f;
		amp = 0.5f;
		hSpeed = 1.4f;
		hp = maxHp = 99999;
	}
	
	// Update is called once per frame
	void Update () {

		DoFloatMov ();

		if (myState == (int)stateOfSkill.waitingAim) {
			switch(activeSkill){
			case (int)skillPressed.skillQ:
				if (!gameObject.GetComponent<SkillQ> ().IsOnCD ()) {
					gameObject.GetComponent<SkillQ> ().Aim ();
				}
				activeSkill = (int)skillPressed.none;
				break;
		
			case (int)skillPressed.skillW:
				if (!gameObject.GetComponent<SkillW> ().IsOnCD ()) {
					gameObject.GetComponent<SkillW> ().Aim ();
				}
				activeSkill = (int)skillPressed.none;
				break;
			//case (int)skillPressed.skillE:

			//case (int)skillPressed.skillR:
				
			//case (int)skillPressed.skillD:
				
			default:
				//Debug.Log ("Waiting");
				break;
			}

		}

		if(Input.GetKeyDown(KeyCode.Q)){
			activeSkill = (int)skillPressed.skillQ;
			myState = (int)stateOfSkill.waitingAim;
		}else if (Input.GetKeyDown(KeyCode.E)){
			activeSkill = (int)skillPressed.skillW;
			myState = (int)stateOfSkill.waitingAim;
		} /* else if (Input.GetKeyDown(KeyCode.E)){
			activeSkill = (int)skillPressed.skillE;
			myState = (int)stateOfSkill.waitingAim;
		} else if (Input.GetKeyDown(KeyCode.R)){
			activeSkill = (int)skillPressed.skillR;
			myState = (int)stateOfSkill.waitingAim;
		} else if (Input.GetKeyDown(KeyCode.D)){
			activeSkill = (int)skillPressed.skillD;
			myState = (int)stateOfSkill.waitingAim;
		}*/


	}
		

	void DoFloatMov(){
		float walk = hSpeed * Input.GetAxis ("Horizontal");
		if (walk > 0 && gameObject.transform.position.x < 20) {
			rb.velocity = new Vector3 (walk, amp * Mathf.Sin (omega * t), 0);
		} else if (walk < 0 && gameObject.transform.position.x > -20) {
			rb.velocity = new Vector3 (walk, amp * Mathf.Sin (omega * t), 0);
		} else {
			rb.velocity = new Vector3 (0, amp * Mathf.Sin (omega * t), 0);
		}
		t += Time.deltaTime;
	}

	public void TakeDamage(int x){
		hp -= x;
		if (hp <= 0) {
			Died ();
		}
	}

	public void Died(){
		isDead = true;
	}

}
