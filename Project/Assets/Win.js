function OnTriggerEnter (col:Collider) {
	if(col.gameObject.tag == "Player"){
		Application.LoadLevel(4);
           }
}
