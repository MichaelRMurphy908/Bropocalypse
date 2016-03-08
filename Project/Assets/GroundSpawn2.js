var sound : AudioSource;
var brains : AudioClip;

function OnTriggerEnter (col:Collider) {
	if(col.gameObject.tag == "Player"){
		sound = gameObject.GetComponent("AudioSource");
		gameObject.Find("Spawn4").SendMessage("Spawn");
        gameObject.Find("Spawn5").SendMessage("Spawn");
        gameObject.Find("Spawn6").SendMessage("Spawn");
        gameObject.Find("Spawn7").SendMessage("Spawn");
		sound.clip = brains;
		sound.Play();		
        Destroy(this);
           }
}
