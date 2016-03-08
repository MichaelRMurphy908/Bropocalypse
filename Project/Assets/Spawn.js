var enemy : Rigidbody;

function Spawn () {
		var cloneEnemy : Rigidbody;
        cloneEnemy = Instantiate(enemy, transform.position, transform.rotation);
           }

