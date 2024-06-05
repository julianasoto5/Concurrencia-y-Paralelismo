-- Hay un sistema de reconocimiento de huellas dactilares de la policía
-- que tiene 8 Servidores para realizar el reconocimiento, cada uno de 
-- ellos trabajando con una Base de Dato propia; a su vez hay un 
-- Especialista que la utiliza indefinidamente. El sistema funciona de 
-- la siguiente manera: el Especialista toma una imagen de una huella 
-- (TEST) y se la envía a los servidores para que cada uno de ellos le 
-- devuelve el código y el valor de similitud de la huella que más se 
-- asemeja a TEST en su BD; al final del procesamiento, el especialista
-- debe conocer el código de la huella con mayor valor de similitud entre 
-- las devueltas por los 8 servidores. Cuando ha terminado de procesar
-- una huella comienza nuevamente todo el ciclo.
-- Suponga que existe una función Buscar(test, código, valor) que utiliza
-- cada Servidor donde recibe como parámetro de entrada la huella test,
-- y devuelve como parámetros de salida el código y el valor de similitud 
-- de la huella más parecida a test en la BD correspondiente. Maximizar
-- la concurrencia y no generar demora innecesaria.

Procedure Sistema is
  Task Especialista is
    Entry mandarHuella(test: OUT HuellaDactilar);
    Entry recibirResultado(cod: IN integer; parecido: IN integer);
  End Especialista;

  Task Type Servidor;
  arregloServidores: array(1..8) of Servidor;

  Task Body Especialista is
    test: HuellaDactilar;
    i, similitud, codigo: integer; --estoy suponiendo que similitud es un valor entre 0 y 10 que dice que tan parecida es (10 igual)
  Begin
    loop
      test:= tomarImagenHuella();
      i:= 0; --cantidad de servidores que mandaron los resultados de la huella test a analizar
      similitud:= 0;
      while (i < 8) loop
        SELECT
          Accept mandarHuella(test);
        OR
          Accept recibirResultado(cod: IN integer; parecido: IN integer) do
            if (parecido > similitud) --este calculo de maximo vale la pena hacerlo afuera? deberia guardarme en otras variables los valores recibidos pero libero antes al servidor
              similitud:= parecido;
              codigo:= cod;
            end if;
            i++;
          end recibirResultado;
        END SELECT; --se queda esperando a que un servidor pida una huella o que le mande el resultado 
      end loop; --hasta no tener todos los resultados no sale
      --
            
    end loop;
  End Especialista;
Begin
End Sistema;
