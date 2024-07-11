/*
En un corralón de materiales hay un empleado que debe atender a N clientes 
de acuerdo al orden de llegada. Cuando un cliente es llamado por el empleado
para ser atendido, le da una lista con los productos que comprará, y espera
a que el empleado le entregue el comprobante de la compra realizada.
*/

Monitor Admin{
    cola fila;
    cond empleado;
    cond espera[N];
    text lista, comprobantes[N];
    int idPersona;

    Procedure Comprar(idP: IN int; l: IN text; comp: OUT text){
      push(fila, (idP, l));
      signal(empleado);
      wait(espera[idP]);
      comp = comprobantes[idP];
    }

    Procedure Siguiente(idP: OUT int; l: OUT text){
      if (empty(C)) wait(empleado);
      pop(fila, (idP,l));
    }

    Procedure Resultado(idP: IN int; comp: IN text){
      comprobantes[idP] = comp;
      signal(espera[idP]);
    }
}

Process Cliente[id:0..N-1]
{ texto: lista, comprobante;
  lista = generarLista();
  Admin.Comprar(id, lista, comprobante);
}

Process Empleado
{ int i, idP; text l, comp;
  for i in 1..N{
    Admin.Siguiente(idP, l);
    comp = hacerComprobante(l);
    Admin.Resultado(idP,comp);
  }
}
