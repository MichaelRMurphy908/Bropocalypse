var speed : float = 8.0;
var walkSpeed : float = 8.0;
var runSpeed : float = 16.0;
var chargeSpeed : float = 32.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;
var jumping : boolean = false;
var charging : boolean = false;
var chargeTime : float = 2.0;
var chargeTimer : float = 0;
var x : int = 0;
var z : int = 0;
var sigCharging : boolean = false;

private var moveDirection : Vector3 = Vector3.zero;


function Start()
{
	animation["walk"].speed = 2.0;
}

function Update() {
	var controller : CharacterController = GetComponent(CharacterController);
	//var atk : FootbroAttack = GetComponent(FootbroAttack);
	//var atk : FootbroAttack = gameObject.GetComponent("FootbroAttack");
	if ((Input.GetKey(KeyCode.LeftShift)) && (!charging))
	{
		speed = runSpeed;
	}
	//else if ((Input.GetKeyDown(KeyCode.LeftControl)) && (chargeTimer == 0))
	else if (sigCharging)
	{
		charging = true;
		chargeTimer = chargeTime;
		speed = chargeSpeed;
	}
	else
	{
		if(!charging)
			speed = walkSpeed;
	}
	if ((controller.isGrounded) && (!charging)) {
		// We are grounded, so recalculate
		// move direction directly from axes
		x = 0;
		z = 0;
		
		if (Input.GetKey(KeyCode.D)) x = 1;
		else if (Input.GetKey(KeyCode.A)) x = -1;
		if (Input.GetKey(KeyCode.W)) z = 1;
		else if (Input.GetKey(KeyCode.S)) z = -1;
		//transform.Rotate(0, 0, 0);
		moveDirection = Vector3(x * -1, 0, z * -1); //switch Horizontal and Verticle for orientation of level
		moveDirection.Normalize();
		//moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		//Debug.Log(moveDirection.x);
		
		if (Mathf.Abs(moveDirection.magnitude) > walkSpeed)
		//if (speed > walkSpeed)
		{
			animation.CrossFade ("run");
		}
		else if ((Mathf.Abs(moveDirection.magnitude) > 0) && (Mathf.Abs(moveDirection.magnitude) <= runSpeed))
		//else if ((speed > 0) && (speed <= runSpeed))
		{
			animation.CrossFade ("walk");
		}
		else 
		{
			animation.CrossFade("idle");
		}
		
		if (Input.GetButton ("Jump")) 
		{
			moveDirection.y = jumpSpeed;
			jumping = true;
		}
		
		
	}
	else if (controller.isGrounded)
	{
		x = 0;
		z = 0;
		
		if (Input.GetKey(KeyCode.D)) x = 1;
		else if (Input.GetKey(KeyCode.A)) x = -1;
		if (Input.GetKey(KeyCode.W)) z = 1;
		else if (Input.GetKey(KeyCode.S)) z = -1;
		moveDirection = Vector3(x * -1, 0, z * -1); //switch Horizontal and Verticle for orientation of level
		//moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		animation["charge"].speed = 2.5;
		animation.CrossFade ("charge");
		chargeTimer -= Time.deltaTime;
		if (chargeTimer < 0)
		{
			charging = false;
			chargeTimer = 0;
		}
		if (Input.GetButton ("Jump")) 
		{
			moveDirection.y = jumpSpeed;
			jumping = true;
		}
	}
	// Apply gravity
	moveDirection.y -= gravity * Time.deltaTime;
	if (moveDirection.y <= 0)
		jumping = false;
	
	// Move the controller
	controller.Move(moveDirection * Time.deltaTime);
	if(controller.velocity.magnitude > 0)
	{
		var derp : Vector3;
		derp = (transform.position + controller.velocity);
		derp.y = transform.position.y;
		//derp.x = transform.position.x;
		//derp.z = 0;
		//derp.y = 0;
		transform.LookAt(derp);
	}
	
}
function IsJumping () {
	return jumping;
}
function beginCharge ()
{
	sigCharging = true;
}

function endCharge()
{
	sigCharging = false;
}