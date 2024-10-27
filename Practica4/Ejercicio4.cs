/**
Simular la atención en un locutorio con 10 cabinas telefónicas, que tiene un 
empleado que se encarga de atender a los clientes. Hay N clientes que al llegar 
esperan hasta que el empleado les indica a qué cabina ir, la usan y luego se 
dirigen al empleado para pagarle. El empleado atiende a los clientes en el orden
en que se hacen los pedidos, pero siempre dando prioridad a los que termianron de 
usar la cabina. 
Suponga que hay una función Cobrar() llamada por el empleado que simula que el 
empleado le cobra al cliente.
**/


//hay dos ordenes: el de llegada para la seleccion de las cabinas y otro del pago de acuerdo a cuando termines de usar la cabina 
//tienen prioridad de la atencion del empleado los que terminaron de usar la cabina antes que los que llegan

chan Llegadas(int); // para cuando lleguen los clientes
chan FilaPagar(int); //para cuando terminen de usar la cabina
chan OcuparCabina[N](int); //para que el empleado le avise al cliente que cabina usar
chan CabinasDisponibles(int);  //el empleado carga los 10 ids antes de empezar a atender
chan ChequeoPago[N](Ticket); //el empleado le avisa que ya realizo el pago

Process Cliente[id: 0..N-1]
{ int idCabina; Ticket ticket;
  while (true){
    // Avisa que llego
    send Llegadas(id);
    // Espera a que el empleado indique a que cabina ir // Recibe el nro de cabina
    receive OcuparCabina[id](idCabina);
    
    // Usa la cabina y la libera (la agrega en la cola)
    usarCabina(idCabina); //si esta usando la cabina es porque el empleado la saco del canal

    send FilaPagar(id); //se agrega a la fila para pagar con prioridad
   
    send CabinasDisponibles(idCabina); //libera la cabina
    
    
    // Espera para pagar al empleado
    receive chequeoPago[id](ticket);

    //termina
  }
}


Process Empleado
{  int idC1,idC2, idCabina;


  //carga de las cabinas
  for (i = 0..9){
    send CabinasDisponibles(i);
  }

  while (true){
    
    if (not empty (FilaPagar))->
        receive FilaPagar (idC2);
        Cobrar();
        send chequeoPago[idC2](1);
      
   []if ((not empty (CabinasDisponibles)) and (not empty (Llegadas)) and (empty(FilaPagar)))-> //si hay cabinas disponibles puede asignar a alguien y no hay nadie para pagar
      receive CabinasDisponibles(idCabina);  //recibe el id de una cabina disponible
      receive Llegadas(idC1); //recibe el id de la persona a ubicar en la cabina - seguro que no esta vacio porq alguien envio el Aviso    
      send OcuparCabina[idC1](idCabina);
    fi
  }  
}
