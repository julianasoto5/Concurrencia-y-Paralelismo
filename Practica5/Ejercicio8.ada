-- Una empresa de limpieza se encarga de recolectar residuos en una ciudad por medio de 3
-- camiones. Hay P personas que hacen continuos reclamos hasta que uno de los camiones
-- pase por su casa. Cada persona hace un reclamo, espera a lo sumo 15 minutos a que llegue
-- un camión y si no pasa vuelve a hacer el reclamo y a esperar a lo sumo 15 minutos a que 
-- llegue un camión y así sucesivamente hasta que el camión llegue y recolecte los residuos;
-- en ese momento deja de hacer reclamos y se va. Cuando un camión está libre la empresa
-- lo envía a la casa de la persona que más reclamos ha hecho sin ser atendido.

Procedure Limpieza is

  Task Type Persona is

  End Persona;

  Task Empresa is
  End;
  Task Type Camion;
  Task Admin is
  End Admin;
  arregloCamiones: array(1..3) of Camion;
  arregloPersonas: array(1..P) of Persona;

  Task Body Empresa is
  	reclamos: array(1..P) of integer;
	totalReclamos: integer:=0;
  Begin
    for i in 1..P loop
      reclamos(i):= 0;
      arregloPersonas(i).Ident(i);
    end loop;
    
    loop
      SELECT
        Accept hacerReclamo(idP: IN integer; dir: IN texto) do
					reclamos(idP)+=1;
					totalReclamos:= totalReclamos+1;
        end hacerReclamo;
			OR 
				when (totalReclamos > 0) => Accept Siguiente(idP: OUT integer) do --entra solo si hay reclamos vigentes (el valor se actualiza dentro)
											   	idP:= maximo(reclamos); --devuelve la posicion del maximo
												totalReclamos:= totalReclamos - reclamos(id);
												reclamos(id):= 0;
											end Siguiente;
    end loop;
  End Empresa;

  Task Body Camion is
		direc: texto;
		idP: integer;
  Begin
    loop
		Empresa.Siguiente(idP); --esta libre
		recolectarBasura();
		arregloPersonas(idP).llegue();
	end loop;

  End Camion;

  Task Body Persona is
    id: integer;
    pasoCamion: boolean:= FALSE;
  Begin
    Accept Ident(idP: IN integer) do  
      id:= idP;
    end Ident;
	
    while(not (pasoCamion)) loop
      Empresa.hacerReclamo(id);
      --Espera a lo sumo 15 minutos
      SELECT
        Accept llegue(); --pasa el camion
        pasoCamion:= TRUE;
      OR DELAY 900
        NULL;
      END SELECT;
    end loop;
  End Persona;
BEGIN
  NULL;
END Limpieza;
