/*
Se dispone de un sistema compuesto pot 1 central y dos procesos. Los procesos
envían señales a la central. La central comienza su ejecución tomando una señal
del proceso 1, luego toma aleatoriamente señales de cualquiera de los dos 
indefinidamente. Al recibir una señal de proceso 2, recibe señaes del mismo 
proceso durante 3 minutos.

El proceso 1 envía una señal que es considerada vieja (se deshecha) si en 2
minutos no fue recibida.
El proceso 2 envía una señal, si no es recibida en ese instante espera 1
minuto y vuelve a mandarla (no se deshecha).
*/





PROCEDURE Sistema IS
  Task Central is
    Entry Signal1 (senial: IN Signal);
    Entry Signal2 (senial: IN Signal);
    Entry FinTimer();
  End Central;

  Task Proceso1;
  Task Proceso2;
  Task Timer is
    Entry Iniciar(time: IN integer);
  End Timer;

  Task Body Central is
  Begin
    Accept Signal1(senial: IN Signal) do //se queda esperando la senial del proceso 1 para dar inicio al sistema.
      procesarSignal(senial);
    end Signal1;

    loop
      SELECT
        Accept Signal1(senial: IN Signal)do
          procesarSignal(senial);
        end Singal1;
      OR 
        Accept Signal2(senial: IN Signal) do
          procesarSignal(senial);
          Timer.Iniciar(180);
          while (FinTimer'count = 0) loop //todavia no se encolo el fin del timer
              Accept Signal2(senial:IN Signal) do
                procesarSignal(senial);
              end Signal2;
          end loop; //sale del loop cuando termina el tiempo (es decir, hay un entry call en el fin del timer)
          Accept FinTimer(); //desencola el FinTimer
        end Signal2;
      END SELECT;
    end loop;
    
  End;

  Task Body Timer is
  Begin
    Accept Iniciar(time: IN integer) do
      DELAY(time);
    End Iniciar;
    Central.FinTimer();
  End Timer;

  Task Body Proceso1 is
    senial: Signal;
  Begin
    loop
      senial:= generarSignal();
      SELECT
        Central.Signal1(senial);
      OR DELAY 120 // si no lo recibe para los 2 minutos la deshecha
        NULL;
      END SELECT;
    end loop;
  End Proceso1;

  Task Body Proceso2 is
    senial: Signal;
  Begin
    loop
      senial:= generarSignal();
      SELECT
        Central.Signal2(senial);
      ELSE //   si no lo recibe inmediatamente espera 1 minuto y vuelve a enviarla
        Delay(60);
        Central.Signal2(senial); //se queda esperando a que lo reciba la segunda vez (bn?)
      END SELECT;
    end loop;
  End Proceso2;

BEGIN
END Sistema;
