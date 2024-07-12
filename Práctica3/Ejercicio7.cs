/*
7)Se debe simular una maratón con C corredores donde en la llegada hay UNA máquina
expendedora de agua con capacidad para 20 botellas. Además, existe un repositor
encargado de reponer las botellas de la máquina. Cuando los C corredores han 
llegado al inicio comienza la carrera. Cuando un corredor termina la carrera se 
dirigen a la máquina expendedora, espera su turno (respetando el orden de llegada),
saca una botlla y se retira. Si encuentra la máquina sin botellas, le avisa al 
repositor para que cargue nuevamente la máquina con 20 botellas; espera a que se
haga la recarga; saca una botella y se retira. NOTA: maximizar la concurrencia;
mientras se reponen las botellas se debe permitir que otros corredores se encolen.
*/
Monitor Admin{
  cond barrera;
  cond espera[C];
  cond repositor;
  cond listo;
  int cant = 0, cantB = 20, next;
  cola fila;
  boolean libre = true;
  Procedure Llegada{
    cant++;
    if (cant == C)
      signal_all(barrea);
    else wait(barrera);
  }

  Procedure Pasar(idC: IN int){
    /*si esta ocupada la maquina se tiene que encolar (no pregunto por empty(fila) 
      porque puede pasar que la persona que la estaba usando tuvo que esperar a que 
      el repositor reponga las botellas. En ese caso la cola esta vacia, pero si llega
      alguien el mismo debe encolarse y darle prioridad al que detecto la maquina vacia.
      */
    if (not libre){ 
      push(fila,idC)
      wait(espera[idC]);
    } else libre = false;
  }

  Procedure Agarrar(){
    if (cantB == 0){ //se detecta maquina vacia
      signal(repositor);
      wait(listo);
    }
    cant--; //agarra una botella
  }

  Procedure Salir(){
    if (empty(fila)) libre = true;
    else {
      pop(fila, next);
      signal(espera[next]);
    }
  }

  
  Procedure ReponerBotellas(){
    wait(repositor);
    cantB = 20;
    signal(listo);
  }
}
Process Corredor[id:0..C-1]
{
  Admin.Llegada();
  hacerCarrera();
  Admin.Pasar();
  Admin.Agarrar();
  Admin.Salir();
}


Process Repositor
{
  while(true){
    Admin.ReponerBotellas();
  }
}
