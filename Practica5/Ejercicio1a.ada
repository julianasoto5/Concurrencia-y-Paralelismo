
/**
Se requiere modelar un puente de un solo sentido, el puente solo soporta el peso de 5 unidades 
de peso. Cada auto pesa 1 unidad, cada camioneta pesa 2 unidades y cada camión 3 unidades. 
Suponga que haY una cantidad interminable de vehículos (A autos, B camionetas y C camiones).
a) Realice la solución suponiendo que todos los vehículos tienen la misma prioridad.
b) Modifique la solución para que tengan mayor prioridad los camiones que el resto de los 
vehículos.
**/



Procedure Puente is
	Task Admin is 
		Entry pedidoAuto();
		Entry pedidoCamioneta();
		Entry pedidoCamion();
		Entry salirAuto();
		Entry salirCamioneta();
		Entry salirCamion();

	End Admin;
	
	Task Type Auto;
	Task Type Camioneta;
	Task Type Camion;

	arregloAutos: array (1..A) of Auto;
	arregloCamionetas: array (1..B) of Camioneta;
	arregloCamiones: array (1..C) of Camion;
	
	Task Body Admin is
		total:integer:=5;
	Begin
		loop
			SELECT
			      	when (total >= 1) => Accept pedidoAuto() do // si hay al menos una unidad libre, permite el paso del auto.
			        			total--;
			                            End pedidoAuto();
			        End when;
			        when (total >= 2) => Accept pedidoCamioneta() do // si hay dos unidades libres, permite el paso de la camioneta (y la otra condicion tambien da verdadero)
			        			total:= total-2;
			                            End pedidoCamioneta();
			        End when;
			        when (total >= 3) => Accept pedidoCamion() do // si hay tres unidades libres, permite el paso del camion (y las otras dos condiciones tambien dan verdadero)
			        			total:= total-3;
			                            End pedidoCamion();
			        End when;
      
			
			OR
					accept SalirAuto() is
						total := total + 1;
					end SalirAuto;
			OR
					accept SalirCamioneta() is
						total := total + 2;
					end SalirCamioneta;
			OR
					accept SalirCamion() is
						total := total + 3;
					end SalirCamion;
			END SELECT;

		End loop;
	End Admin;
			
			
	Task Body Auto is
	Begin
		Admin.pedidoAuto();
		pasarPuente();
		Admin.salirAuto();
  	End Auto;
  			
	Task Body Camioneta is
	Begin
		Admin.pedidoCamioneta();
    		pasarPuente();
    		Admin.salirCamioneta();
  	End Camioneta;			
	
 	Task Body Camion is
	Begin
		Admin.pedidoCamion();
		pasarPuente();
		Admin.salirCamion();
  	End Camion;		
End Puente;
