/*
En una herrería hay 15 empleados que forman 5 grupos de 3 personas; los grupos 
se forman de acuerdo al orden de llegada (los 3 primeros pertenecen al grupo 1,
los 3 siguientes al grupo2, y así sucesivamente). Ni bien conoce el grupo al que
pertenece el empleado comienza a trabajar (no debe esperar al resto de grupo para 
comenzar). Cada grupo debe hacer exactamente P unidades de un producto (cada 
unidad es hecha por un único empleado). Al terminar de hacer las P unidades de un
grupo, sus 3 empleados se retiran. NOTA: maximizar la concurrencia; ningún grupo 
puede hacer unidades de nás.
*/
sem mutex = 1, mutexCant = 1, mutexCantProd ([5] 1);
sem trabajo([5] 1);
cola C;
int grupos[5] = ([5] 3);
int grupoActual = 1;
int cantProd[5] = ([5] P);
boolean libre = true;

Process Empleado[id:0..14]
{ int idG,idE;
  P(mutex);
  if (libre){
    libre = false;
    V(mutex);
  }else {
    push(C,id);
    V(mutex);
    P(esperar[id]);
  }
  //llega aca si la cola estaba libre o si es el siguiente
  //hay que asignarle un grupo
  idG = grupoActual;
  cuposGrupos[idG-1] = cuposGrupos[idG-1]-1;
  if (cuposGrupos[idG-1] == 0){//se lleno el grupo
    grupoActual = grupoActual + 1;
  }

  P(mutex);//ya se determino el grupo para el actual, hay que dejar ubicarse al siguiente
  if (empty(C)) libre = true;
  else {
    pop(C, idE);
    V(esperar[idE]);
  }
  V(mutex);

  
  if (cant > 0){
    //hay lugar en el grupo actual
    idG = grupoActual; 
    cant = cant-1;
  }
  else { //hay que cambiar de grupo
    grupoActual = grupoActual+1;
    idG = grupoActual;
    cant = 3;
  }
  V(mutexCant);
  //comienza a trabajar una vez que sabe su grupo
  
  while(cantProd[idG] != 0){
  
    P(trabajo[idG]);
    hacerUnidad();
    V(trabajo[idG]);
  }
}
