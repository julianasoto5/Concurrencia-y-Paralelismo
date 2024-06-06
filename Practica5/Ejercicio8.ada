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
  Task Type Camion;
  Task Admin is
  End Admin;
  arregloCamiones: array(1..3) of Camion;
  arregloPersonas: array(1..P) of Persona;

  Task Body Persona is
    reclamo: texto;
    pasoCamion: boolean:= FALSE;
  Begin
    while(not (pasoCamion)) loop
      reclamo:= reclamo+1;
    end loop;
  End Persona;
BEGIN
  NULL;
END Limpieza;
