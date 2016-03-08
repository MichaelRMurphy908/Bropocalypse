var sound : AudioSource;
var brains : AudioClip;

function OnTriggerEnter (col:Collider) {
	if(col.gameObject.tag == "Player"){
		sound = gameObject.GetComponent("AudioSource");
		gameObject.Find("Spawn12").SendMessage("Spawn");
        gameObject.Find("Spawn13").SendMessage("Spawn");
        gameObject.Find("Spawn14").SendMessage("Spawn");
        gameObject.Find("Spawn15").SendMessage("Spawn");
        gameObject.Find("Spawn16").SendMessage("Spawn");
        gameObject.Find("Spawn17").SendMessage("Spawn");
        gameObject.Find("Spawn18").SendMessage("Spawn");
		sound.clip = brains;
		sound.Play();		
        Destroy(this);
           }
}
