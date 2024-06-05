/*
Se quiere modelar la cola de un banco que atiende un solo empleado, los clientes llegan y
si esperan más de 10 minutos se retiran.
*/


Procedure Banco is
  Task Empleado is
    Entry Atender(datos: IN texto; Res: OUT texto);
  End Empleado;

  Task Type Cliente;
  arregloClientes: array(1..N) of Cliente;

  Task Body Cliente is
    Respuesta: texto;
  Begin
    SELECT
      Empleado.Atender("datos", Respuesta);
    OR DELAY 600 //ESPERA 10 MINUTOS
        NULL;
    END SELECT;
  End Cliente;

  Task Body Empleado is
  Begin
    loop
      Accept Atender(datos: IN texto; Res: OUT texto) do
        Res:= atenderPedido(datos);
      end Atender;
    end loop;
  End Empleado

Begin
    NULL;
End Banco;
