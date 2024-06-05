-- En un sistema para acreditar carreras universitarias, hay UN Servidor que
-- atiende pedidos de U Usuarios de a uno a la vez y de acuerdo al orden en
-- que se hacen los pedidos. Cada usuario trabaja en el documento a presentar,
-- y luego lo envía al servidor; espera la respuesta del mismo que le indica 
-- si está todo bien o hay algún error. Mientras haya algún error vuelve a 
-- trabajar con el documento y a enviarlo al servidor. Cuando el servidor le
-- responde que está todo bien el usuario se retira. Cuando un usuario envía 
-- un pedido espera a lo sumo dos minutos a que sea recibido por el servidor, 
-- pasado ese tiempo espera un minuto y vuelve a intentarlo (usando el mismo
-- documento). 


Procedure SIU is

  Task Servidor is
    Entry pedido(doc: IN texto; ok: OUT boolean);
  End Servidor;

  Task Type Usuario;

  arregloUsuarios: array(1..U) of Usuario;

  Task Body Servidor is
  Begin
    loop
      Accept pedido(doc: IN texto; ok: OUT boolean) do
        ok:= chequearDocumento(doc);
      end pedido;
    end loop;
  End Servidor;


  Task Body Usuario is
    ok, entregado: boolean;
    doc: texto;
  Begin
    ok:= FALSE;
    doc:= hacerDocumento(); --cada usuario manda un documento nomas.
    while (not ok) loop
      SELECT 
        Servidor.pedido(doc,ok); //se encola en los pedidos y si lo recibe en menos de 2 minutos, devuelve ok con el resultado de la operacion
        if (not ok) --si tiene errores lo corrige
          doc:= corregirDocumento();
        end if;
      OR DELAY 120
        DELAY(60) --espera 1 minuto antes de reenviar
      END SELECT;
    end loop;
  --se retira
  End Usuario;


Begin
End SIU;
