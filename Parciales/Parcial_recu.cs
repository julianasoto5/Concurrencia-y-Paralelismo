/*
1. Resolver con SEMAFOROS el siguiente problema. En un restorán trabajan C cocineros
y M mozos. De forma repetida, los cocineros preparan un plato y lo dejan listo en la
bandeja de platos terminados, mientras que los mozos toman los platos de esta bandeja 
para repartirlos entre los comensales. Tanto los cocineros como los mozos trabajan de
a un plato por vez. Modele el funcionamiento del restorán considerando que la bandeja
de platos listos puede almacenar hasta P platos. No es necesario modelar a los 
comensales ni que los procesos terminen.
*/

cola platos; //recurso compartido
int cantP = 0; //recurso compartido
sem mutexC = P; //proteccion cola - solo P platos
sem mutex = 1; //uso de cola
sem aviso = 0; //avisa que hay un plato listo
Process Cocinero[id:1..C]
{ Plato plato;
  while (true){
    plato = cocinar();
    P(mutexC);
    
    P(mutex);
    push(platos, plato);
    V(mutex);
    
    V(aviso);
    
  }
}

Process Mozo[id:1..M]
{ Plato plato;
  while (true){
    P(aviso); //recibe aviso de que hay un plato listo

    //agarra plato
    P(mutex);
    pop(platos, plato);
    V(mutex);

    V(mutexC); //habilita la carga de otro plato

    repartir(plato);
  }
}  

/*

2. Resolver con MONITORES. En una planta verificadora de vehículos existen 5 estaciones
de verificación. Hay 75 vehículos que van para ser verificados, cada uno conoce el número
de estación a la cual debe ir. Cada vehículo se dirige a la estación correspondiente y 
espera a que lo atiendan. Una vez que le entregan el comprobante de verificación, el 
vehículo se retira. Considere que en cada estación se atienden a los vehículos de acuerdo
al orden de llegada. NOTA: maximizar la concurrencia.
*/

Process Vehiculo[id:0..74]
{ int idE; text comprobante;
  idE = getIdE();
  Estacion[idE].Verificar(id,comprobante)
}

Process Estacion[id:0..4]
{  int idV; text comp;
  while (true){
    Estacion[id].Atender(idV);
    comp = verificarVehiculo(idV);
    Estacion[id].Enviar(idV, comp);
  }
  
}
Monitor Estacion[id:1..5]
{
  cola fila;
  cond esperar[75];
  cond aviso, fin;
  int esperando = 0;
  boolean libre = true;
  text comprobantes[75];
  
  Procedure Verificar(idV: IN int; comp: OUT text){
    push(fila, idV);
    signal(aviso);
    wait(esperar[idV]);
    comp = comprobantes[idV]
    signal(fin);
  }

  Procedure Atender(next: OUT int);{
    wait(aviso);
    pop(fila, next);
  }

  Procedure Enviar(idV: IN int; comp: IN text){
    comprobantes[idV] = comp;
    signal(esperar[idV]);
    wait(fin);
  }

}







//no se que tan valida es. El monitor Estacion se supone que controla el acceso asique para mi esta bien
Process Vehiculo[id:0..74]
{ int idE; text comprobante;
  idE = getIdE();
  Estacion[idE].Pedido(id);
  comprobante = verificarVehiculo();
  Estacion[idE].Salir(); 
}

Monitor Estacion[id:1..5]
{
  cond fila;
  int esperando = 0;
  boolean libre = true;
  
  Procedure Pedido(idV: IN int){
    if (libre) libre = false;
    else {
      esperando++;
      wait(fila);
    }
  }

  Procedure Salir(){
    if (esperando == 0) libre = true;
    else {
      esperando--;
      signal(fila);
    }
  }
}
