using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAssignment : MonoBehaviour {

	public enum Team {
		TEAM_A,
		TEAM_B,
	}

	public Team myTeam;

	public Vector3 startPos;

	public List <string> p1Inputs = new List<string>();
	public List <string> p2Inputs = new List<string>();

	// INPUT ASSIGNMENT //

	public string horizontal, vertical, fire, rStick, rStick2, cancel, move, recording, play, rewind, block;

	// Use this for initialization
	void Start () {

		startPos = transform.position;


		if (myTeam == Team.TEAM_A) {
			horizontal = "P1_HORIZONTAL";
			p1Inputs.Add (horizontal);

			vertical = "P1_VERTICAL";
			p1Inputs.Add (vertical);

			rewind = "P1_L2";
			p1Inputs.Add (rewind);

			fire = "P1_CROSS";
			p1Inputs.Add (fire);

			rStick = "P1_RSTICK";
			p1Inputs.Add (rStick);

			rStick2 = "P1_RSTICK_2";
			p1Inputs.Add (rStick2);

			move = "P1_R2";
			p1Inputs.Add (move);

			cancel = "P1_R3";
			p1Inputs.Add (cancel);

			recording = "P1_L1";
			p1Inputs.Add (recording);

			play = "P1_R1";
			p1Inputs.Add (play);

			block = "P1_SQUARE";
			p1Inputs.Add (block);




		} else if (myTeam == Team.TEAM_B) {
			horizontal = "P2_HORIZONTAL";
			p2Inputs.Add (horizontal);

			vertical = "P2_VERTICAL";
			p2Inputs.Add (vertical);

			rewind = "P2_L2";
			p2Inputs.Add (rewind);

			fire = "P2_CROSS";
			p2Inputs.Add (fire);

			rStick = "P2_RSTICK";
			p2Inputs.Add (rStick);

			rStick2 = "P2_RSTICK_2";
			p2Inputs.Add (rStick2);

			move = "P2_R1";
			p2Inputs.Add (move);

			cancel = "P2_R3";
			p2Inputs.Add (cancel);

			recording = "P2_L1";
			p1Inputs.Add (recording);

			play = "P2_R1";
			p1Inputs.Add (play);

			block = "P2_SQUARE";
			p2Inputs.Add (block);
		}
		
	}
	

}
