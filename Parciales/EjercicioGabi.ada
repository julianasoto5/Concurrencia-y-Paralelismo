-- 3. Resolver con ADA el siguiente problema. Hay una empresa de análisis genético. 
-- Hay N clientes que sucesivamente envían secuencias de ADN a la empresa para que 
-- sean analizadas y esperan los resultados para poder envían otra secuencia a 
-- analizar. Para resolver estos análisis la empresa cuenta con 4 servidores que van 
-- rotando su uso para no exigirlos de más (en todo momento uno está trabajando y los
-- otros descansando); cada 6 horas cambia en servidor con el que se trabaja siguiendo
-- un orden circular (1-2-3-4-1-2...). El servidor que está trabajando, toma un pedido
-- (de acuerdo al orden de llegada de los mismos), lo resuelve y devuelve el resultado
-- al cliente correspondiente.
-- Nota: suponga que existe una función Resolver(texto) que utiliza cada Servidor para
-- resolver el análisis de una secuencia de tipo texto y devuelve el resultado que es
-- un entero.


Procedure Empresa is

Task Admin is
  Entry enviarSecuencia(ADN:IN text; idServer: OUT integer); 
  Entry finTimer();
  Entry Siguiente();
End Admin;

Task Type Servidor is
  Entry analizameEsta(ADN: IN text);
  Entry RecibirResultado (Resultado: OUT integer);
End Servidor;

Task Type Cliente is
End Cliente;

Task Timer is
  Inicio(time: IN integer);
end Timer;

arregloClientes: array(1..N) of Cliente;
arregloServidores: array(1..4) of Servidor;

Task Body Cliente is
  ADN, Resultado: text;
  idServer: integer;
  loop
    ADN := generarSecuencia();
    Admin.enviarSecuencia(ADN, idServer);
    Servidor[idServer].RecibirResultado(Resultado);
  end loop; 
End Cliente;

Task Body Servidor is
Res: integer;
loop
  accept AnalizameEsta(ADN: IN text) do
    Res:= Resolver(ADN);
  end AnalizameEsta;
  accept RecibirResultado (Resultado: OUT integer) do 
    Resultado := Res;
  end RecibirResultado;
  Admin.Siguiente(); --esto se soluciona mejor mandando el id de la persona pero estoy negada ajajja (creo q podria ser igual una solucion valida porque solo hay 1 servidor activo y entonces tiene q esperar a terminar antes de empezar otro, pero sino esto no funciona).
End Servidor;

Task Body Admin is
  idS: integer;
  pruebaADN: text;
Begin
  idS:= 1; --el primer server activo es el 1
  Timer.Inicio(21600);
  loop
    SELECT
      Accept finTimer() do
        idS:= idS+1;
        if (idS = 5)
          idS:= 1;
        Timer.Inicio(21600);
      end finTimer;
    OR
      when (finTimer'count <> 0) => Accept enviarSecuencia(ADN: IN text; idServer: OUT integer) do
                                      idServer:= idS;
                                      pruebaADN:= ADN;
                                    end enviarSecuencia;
                                    --si tengo algo para enviar le aviso al servidor activo
                                    Servidor[idS].analizameEsta(pruebaADN);
                                    accept Siguiente(); --se deberia quedar esperando a que termine de enviarle el resultado antes de habilitar el timer o una nueva secuencia
    END SELECT;
  end loop;
End Admin;


Task Body Timer is
t: integer;
Begin
  loop
    Accept Inicio(time: IN integer) do
      t:= time;
    end Inicio;
    delay(t);
    Admin.finTimer();
  end loop;
End Timer;


Begin
  null;
ID!!!!!!
End Empresa;
