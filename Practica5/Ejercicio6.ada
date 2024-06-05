-- En una playa hay 5 equipos de 4 personas cada uno (en total 20 personas 
-- donda cada una conoce previamente a qué equipo pertenece). Cuando las 
-- personas van llegando esperan con los de su equipo hasta que el mismo
-- esté completo (hayan llegado los 4 integrantes), a partir de ese 
-- momento el equipo comienza a jugar. El juego consiste en que cada 
-- integrante del grupo junta 15 monedas de a una en una playa (las monedas
-- pueden ser de 1, 2 o 5 pesos) y se suman los montos de las 60 monedas
-- conseguidas en el grupo. Al finalizar cada persona debe conocer qué
-- grupo juntó más dinero. 
-- Suponga que para simular la búsqueda de una moneda por parte de una 
-- persona existe una función Moneda() que retorna el valor de la moneda
-- encontrada.


Procedure Juego is

  Task Type Persona;

  Task Type Equipo is
    Entry llegadaIntegrante(team: IN integer);
    Entry empezarJuego();
    Entry sumarMonedas(monto: IN integer);
    Entry ganador(winner: OUT integer);
  End Equipo;

  Task Type Arbitro is
    Entry comparar(id: IN integer; monto: IN integer);
    Entry ganador(winner: OUT integer);
  End Arbitro;

  arregloPersonas: array(1..20) of Persona;
  arregloEquipos: array(1..5) of Equipo;

  Task Body Arbitro is
    i,winner,max: integer;  
  Begin
    max:=0;
    for i in 1..5 loop --por cada equipo
      Accept comparar(id: IN integer; monto: IN integer) do
        if (monto > max)
          max:= monto;
          winner:= id;
        end if;
      end comparar;
    end loop;
  --consultar si no seria mejor mandarle directamente a cada persona con un for i..20 (para mi no maximiza la concu pero qsy)
    for i in 1..5 loop --devuelve a cada equipo el ganador y cada equipo le va a comunicar a cada persona en forma concurrente el ganador
      Accept ganador(winner);
    end loop;
  End Arbitro;


  Task Body Equipo is
    i, total,id: integer;
    arregloIdIntegrantes: array(1..4) of integer;
  Begin
    --espera la llegada de los cuatro integrantes (usa el primero para guardarse el id del team)
    Accept llegadaIntegrande(team: IN integer) do
      id:= team;
    end llegadaIntegrante;
    for i in 1..3 loop
      Accept llegadaIntegrande(team: IN integer);
    end loop;

    --hay que mandar mensaje de inicio juego;
    for i in 1..4 loop  
      Accept empezarJuego(); --manda al que haya hecho el entry call primero en vez de hacerlo por id (evitar demoras no?)
    end loop;

    --recibe los montos recaudados por cada uno de los integrantes
    for i in 1..4 loop
      Accept sumarMonedas(monto: IN integer) do
        total:= total + monto;
      end sumarModenas;
    end loop;

    --manda al Arbitro el total de monedas del grupo
    Arbitro.comparar(id,total); 

    --espera a que el arbitro devuelva el ganador para comunicarselo a sus integrantes (se hace aca o en el arbitro?)
    Arbitro.ganador(winner: OUT integer);

    -- le manda a cada integrante el ganador
    for i in 1..4 loop
      Accept ganador(winner);
    end loop;

  End Equipo;

  Task Body Persona is
    team, i, monto: integer;
  Begin
    
    team:= averiguarEquipo();
    arregloEquipos(team).llegadaIntegrante(team); --avisa al equipo que llego (y le dice qué equipo es)
    --espera a que llegue el resto y le den el ok de empezar el juego
    arregloEquipos(team).empezarJuego(); --si lo hago asi entonces no necesito saber el id de persona

    for i in 1..15 loop --una iteracion por moneda
      monto:= monto + Moneda();
    end loop;

    arregloEquipos(team).sumarMonedas(monto); --manda el monto total de las monedas juntadas
    arregloEquipos(team).ganador(winner: IN integer); --se lo pide al arbitro o al equipo? 
  End;


Begin
  NULL;
End Juego;
