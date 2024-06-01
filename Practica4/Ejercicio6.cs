/**
En un laboratorio de genética veterinaria hay 3 empleados. El primero de ellos 
se encarga de preparar las muestras de ADN lo más rápido posible; el segundo 
toma cada muestra de ADN preparada y arma el set de análisis que se deben
realizar con ella y espera el el resultado para archivarlo y continuar trabajando;
el tercer empleado se encarga de realizar el análisis y devolverle el resultado
al segundo empleado.
**/
//Envio !
//Recepcion ?


Process PrimerEmpleado
{ Muestra muestra;
  while (true){
    muestra = prepararMuestra();
    Admin!muestraLista(muestra); //le envia al administrador la muestra realizada
  }
}

Process SegundoEmpleado
{ Muestra muestra;
  Analisis setAnalisis, resultado;
  while (true){
    Admin!pedidoMuestra();  //envio
    Admin?obtenerMuestra (muestra);
    setAnalisis = armarSet(muestra);
    TercerEmpleado!analisisListo(setAnalisis);
    TercerEmpleado?enviarResultado (resultado);
  }
}

Process TercerEmpleado
{ Analisis setAnalisis,resultado;
  while (true){
    SegundoEmpleado?analisisListo (setAnalisis);  //se queda esperando a que el segundo empleado avise q termino
    resultado = realizarAnalisis (setAnalisis);
    SegundoEmpleado!enviarResultado (resultado); //le envia el resultado del analisis al segundo empleado
  }
}



/*
Es necesario el administrador para que el primer empleado haga su tarea lo mas rapido posible
*/
Process Admin
{
  cola Buffer;
  Muestra muestra;

  do PrimerEmpleado?muestraLista (muestra) -> push(Buffer, muestra);
  [] not empty(Buffer); SegundoEmpleado?pedidoMuestra() -> SegundoEmpleado!obtenerMuestra(pop(Buffer));
  od
}

/*la primera vez Buffer esta vacia asique si o si le va a pedir al primer empleado que devuelva
una muestra lista. A partir de la segunda puede seleccionar pedirle otra muestra al 1er empleado
o pedirle al 2do empleado que arme el set. 
*/







