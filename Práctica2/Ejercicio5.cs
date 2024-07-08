/*
  Se debe simular una maratón con C corredores donde en la llegada hay UNA máquina
  expendedora de agua con capacidad para 20 botellas. Además existe un repositor 
  ecargado de reponer las botellas de la máquina. Cuando los C corredores han llegado
  al inicio comienza la carrera. Cuando un corredor termina la carrera se dirige a la
  máquina expendedora, espera su turno (respetando el orden de llegada), saca una botella
  y se retira. Si encuentra la máquina sin botellas, le avisa al repositor para que 
  cargue nuevamente la máquina con 20 botellas; espera a que se haga la recarga y se
  retira. NOTA: maximizar la concurrencia; mientras se reponen las botellas se debe 
  permitir que otros corredores se encolen.
*/
  sem mutexMaquina = 1;
  sem mutexCant = 1;
  sem espera([C] 0);
  sem barrera([C] 0);
  sem pedidoBotella = 0; 
  
  cola C;
  int cant = 0, botellas = 20;
  boolean libre = true;
  Process Corredor[id:0..C-1]
  {  int idC;
      P(mutexCant);
      cant=cant+1;
      if (cant == C){
        for i in 0..C-1 
          V(barrera[i]);//bajada de barrera
      }
      V(mutexCant);
      P(barrera[id]); //se queda esperando la bajada de barrera



      hacerCarrera();
      //Uso de maquina
      P(mutexMaquina); //se encolan por orden de llegada
      if (libre){ //si esta libre
        libre = false; //la marca como ocupada
        V(mutexMaquina);
      }
      else{ //la maquina esta ocupada y debe encolarse
        push(C, id);
        V(mutexMaquina);
        P(espera[id]);
      }
      //llega aca cuando la maquina este libre
      //UNO A LA VEZ A ESTA ALTURA DEL PROCESO -> el resto se quedo esperando en el else
      if (botellas == 0){
        V(pedidoBotella); //avisa al repositor que debe reponer botellas
        P(listo); //espera a que el repositor termine
      }
      
      //hay botellas disponibles
      botellas = botellas-1;
      P(mutexMaquina);
      if (empty(C)) libre = true;
      else{ //hay gente en la cola, hay que dejar pasar al siguiente
        pop(C,idC); //agarra siguiente 
        V(espera[idC]);
      }   
      V(mutexMaquina);   
      
  }

  Process Repositor
  {
    while (true){
      P(pedidoBotella);
      botellas = 20;
      V(listo);
    }
  }
