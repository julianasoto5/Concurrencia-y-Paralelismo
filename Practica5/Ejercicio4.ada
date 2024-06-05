-- En una clínica existe un médico de guardia que recibe continuamente peticiones de 
-- atención de las E enfermeras que trabajan en su piso y de las P personas que llegan
-- a la clínica a ser atendidos.
-- Cuando una persona necesita que la atiendan espera a lo sumo 5 minutos a que el 
-- médico lo haga, si pasado ese tiempo no lo hace espera 10 minutos y vuelve a 
-- requerir la atención del médico. Si no es atendida tres veces, se enoja y se retira
-- de la clínica. 
-- Cuando una enfermera requiere la atención del médico, si este no lo atiende
-- inmediatamente le hace una nota y se la deja en el consultorio para que esta resuelva
-- su pedido en el momento que pueda (el pedido puede ser que el médico le firme algún
-- papel). Cuando la petición ha sido recibida por el médico o la nota ha sido dejada en 
-- el escritorio, continúa trabajando y haciendo más peticiones.
-- El médico atiende los pedidos dándoles prioridad a los enfermos que llegan para ser
-- atendidos. Cuando atiende un pedido, recibe la solicitud y la procesa durante un 
-- cierto tiempo. Cuando está libre aprovecha a procesar las notas dejadas por las 
-- enfermeras.


--A que se refiere cno que "el pedido puede ser que el medico le firme algun papel"? le medico deberia saber a quien devolverle el papel? => deberia mandar id de enfermera.

Procedure Clinica is
  Task Medico is
    Entry atencionP(sintomas: IN texto; diagnostico: OUT texto);
    Entry atencionE(pedidoEnfermera: IN texto);
  End Medico;


  Task Consultorio is
    Entry dejarNota(nota: IN texto);
    Entry agarrarNota(nota: OUT texto);
  End Consultorio;

  Task Type Enfermera;

  Task Type Persona;

  enfermeras: array(1..E) of Enfermera;
  personas: array(1..P) of Persona;

  
  Task Body Consultorio is --se encarga del intercambio de notas entre medico y enfermera
    Notas: cola;
  Begin
    loop
      SELECT
        Accept dejarNota(nota: IN texto) do
          push (Notas,nota);
        End dejarNota;
      OR
        Accept agarrarNota(nota: OUT texto) do
          pop (Notas, nota);
        End agarrarNota;
    end loop;
  End Consultorio;



-- El médico atiende los pedidos dándoles prioridad a los enfermos que llegan para ser
-- atendidos. Cuando atiende un pedido, recibe la solicitud y la procesa durante un 
-- cierto tiempo. Cuando está libre aprovecha a procesar las notas dejadas por las 
-- enfermeras.

  Task Body Medico is
    idE: integer;
    nota: texto;
  Begin
    loop
      SELECT
        Accept atencionP(sintomas: IN texto; diagnostico: OUT texto) do
          diagnostico:= atenderPersona(sintomas);
        end atencionP;
      OR
        when (atencionP'count = 0) =>  --solo lo va a hacer si no hay personas esperando atencion 
            Accept atencionE(pedidoEnfermera: IN texto) do --acepta la atencion (inmediata) de la enfermera
              procesarPedido(pedidoEnfermera);
            End atencionE;
      ELSE --si no hay pacientes esperando ni una enfermera que requiera atencion, se procesa una nota dejada en el consultorio (si es que hay).
          SELECT 
            Consultorio.agarrarNota(idE, nota);
            resolverPedido(nota);
          ELSE   
              NULL;
          END SELECT;                         
      END SELECT;
    end loop;
  End Medico;

-- Cuando una persona necesita que la atiendan espera a lo sumo 5 minutos a que el 
-- médico lo haga, si pasado ese tiempo no lo hace espera 10 minutos y vuelve a 
-- requerir la atención del médico. Si no es atendida tres veces, se enoja y se retira
-- de la clínica. 
  Task Body Persona is
  cant: integer;
  sintomas, diagnostico: texto;
  atendido: boolean;
  Begin
        cant:= 0;
        atendido:= FALSE;
        sintomas:= averiguarSintomas();
        while ((not (atendido)) & (cant < 3)) loop
          SELECT
            Medico.atencionP(sintomas,diagnostico);
            atendido:= TRUE; --levanta flag de atencion
          OR DELAY 300 --espera 5 minutos a ser atendido 
            --si no es atendido, espera otros 10 minutos y vuelve a requerir atencion
            cant++;
            DELAY(600); --espera 10 minutos
          END SELECT;
        end loop;
  End Persona;


-- Cuando una enfermera requiere la atención del médico, si este no lo atiende
-- inmediatamente le hace una nota y se la deja en el consultorio para que esta resuelva
-- su pedido en el momento que pueda (el pedido puede ser que el médico le firme algún
-- papel). Cuando la petición ha sido recibida por el médico o la nota ha sido dejada en 
-- el escritorio, continúa trabajando y haciendo más peticiones.

  Task Body Enfermera is
    nota: texto;
  Begin
    loop
      SELECT
        Medico.atencionE();
      ELSE
        nota:= hacerNotaParaMedico();
        Consultorio.dejarNota(nota); 
      END SELECT;
      trabajar();
    end loop;
  End Enfermera;


Begin
   NULL;
End Clinica;
