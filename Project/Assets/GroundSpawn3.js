var sound : AudioSource;
var brains : AudioClip;

function OnTriggerEnter (col:Collider) {
	if(col.gameObject.tag == "Player"){
		sound = gameObject.GetComponent("AudioSource");
		gameObject.Find("Spawn8").SendMessage("Spawn");
        gameObject.Find("Spawn9").SendMessage("Spawn");
        gameObject.Find("Spawn10").SendMessage("Spawn");
        gameObject.Find("Spawn11").SendMessage("Spawn");
		sound.clip = brains;
		sound.Play();
        Destroy(this);
           }
}
