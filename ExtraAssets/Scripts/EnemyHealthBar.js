var HealthBar : Transform;
var MaxHealth : float;
var MaxBar : float;
var Health : float;
var m_Camera : Camera;


function Update()
{

    // --- Change Size Of Bar --- \\
    transform.localScale.x = (MaxBar) * (Health/MaxHealth);

  
}