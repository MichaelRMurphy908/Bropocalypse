var sound : AudioSource;
var brains : AudioClip;

function OnTriggerEnter (col:Collider) {
	if(col.gameObject.tag == "Player"){
		sound = gameObject.GetComponent("AudioSource");
		gameObject.Find("Spawn").SendMessage("Spawn");
        gameObject.Find("Spawn2").SendMessage("Spawn");
        gameObject.Find("Spawn3").SendMessage("Spawn");
		sound.clip = brains;
		sound.Play();
        Destroy(this);
           }
}
