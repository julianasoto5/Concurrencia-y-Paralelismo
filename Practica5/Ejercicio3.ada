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
    Entry Senial1;
    Entry Senial2;
  End Central;

  Task Proceso1;

  Task Proceso2;

  Task Body Proceso is
  Begin
    Central.Senial1();
    loop
      
    end loop;
  End Proceso;

BEGIN
END Sistema;
