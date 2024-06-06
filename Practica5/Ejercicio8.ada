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
  idMax, maxR: integer;
  Begin
    for i in 1..P loop
      reclamos(i):= 0;
      arregloPersonas(i).Ident(i);
    end loop;
    maxR:= 0;
    loop
      SELECT
        Accept hacerReclamo(idP: IN integer; dir: IN texto) do
					reclamos(idP)+=1;
					if (reclamos(idP) = 1) --hay que agregarla a la cola
						push (Casas,(idP,dir));
          if (reclamos(idP) > max)
            max:= reclamos(id)
            idMax:= id;
          end if;
        end hacerReclamo;
			OR 
				when (maxR > 0) => Accept obtenerDireccionPersona(idP: OUT integer; direc: OUT direc);
													 maxR:= 0;
    end loop;
  End Empresa;

  Task Body Camion is
		direc: texto;
		idP: integer;
  Begin
    loop
			Empresa.obtenerDireccionPersona(idP,direc);
			recolectarBasura();
			arregloPersonas(idP).llegue();
		end loop;

  End Camion;

  Task Body Persona is
    id: integer;
		direc: texto;
    pasoCamion: boolean:= FALSE;
  Begin
    Accept Ident(idP: IN integer) do  
      id:= idP;
    end Ident;
		direc:= getDireccion();
    while(not (pasoCamion)) loop
      Empresa.hacerReclamo(id, direc);
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
