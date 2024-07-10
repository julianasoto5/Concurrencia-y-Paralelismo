/*
Resolver con SEMÁFOROS el siguiente problema. En una fábrica de muebles trabajan 50 empleados. Al llegar, los empleados
forman 10 grupos de 5 personas cada uno, de acuerdo al orden de llegada (los 5 primeros en llegar forman el primer grupo, los 5
siguientes el segundo grupo, y así sucesivamente). Cuando un grupo se ha terminado de formar, todos sus integrantes se ponen a
trabajar. Cada grupo debe armar M muebles (cada mueble es armado por un solo empleado); mientras haya muebles por armar en
el grupo los empleados los irán resolviendo (cada mueble es armado por un solo empleado). Nota: Cada empleado puede tardar
distinto tiempo en armar un mueble. Sólo se pueden usar los procesos “Empleado”, y todos deben terminar su ejecución.
Maximizar la concurrencia.
*/

//Solucion de la catedra, te deja no tener una cola qsy
sem mutexLlegada = 1, reunion([10] 0);
int grupoActual = 0, cantE = 0;
int grupos[10]([10] 5);
Process Empleado[id:0..49]
{  int idG;
  P(mutexLlegada); //de a uno se ubican en el grupo correspondiente
  idG = grupoActual;
  cantE++;
  if (cantE == 5){ //no hay mas cupos, y es el ultimo asiq debe avisar al resto de su llegada
    grupoActual++; //actualiza grupo
    cantE = 0; //reinicia cupos
    for i in 1..4{ 
      V(reunion[idG]); //manda aviso de inicio de trabajo --> lo hace 4 veces porque 4 personas van a hacer el P en esa posicion
    }
  }
  else P(reunion[idG]);

  //debe hacer trabajo
  P(mutexCantMueble);
  while (cantMueble[idG] > 0){
    hacerMueble();
    cantMueble[idG] --;
    V(mutexCantMueble[idG]);
    P(mutexCantMueble[idG]);
  }
  V(mutexCantMueble[idG]);
  
  
}






















sem mutexLibre = 1, mutexGrupo([10] 1), espera([50] 0), reunin([10] 0);
int grupoActual = 0; //indice
int cuposGrupo[10] = ([10] 0);
int cantMuebles[10] = ([10] M);

boolean libre = true, last = false;

Process Empleado[id:0..49]
{  int idNext;
  //llegada del empleado --> se tiene que ubicar en algun grupo (de acuerdo al ORDEN DE LLEGADA)
  P(mutexLibre);
  if (libre){
    libre = false;
    V(mutexLibre);
  }
  else{
    push(C,id);
    V(mutexLibre);
    P(espera[id]);
  }
  
  //se tiene que ubicar
  idG = grupoActual;
  cuposGrupo[idG]++;
  if (cuposGrupo[idG] == 5){ //no hay mas cupos en el grupo actual, aumento el indice
    grupoActual++;
    //el ultimo da el aviso de que el grupo esta completo y pueden empezar a trabajar
    last = true;
  }

  //ya se ubico, deja pasar al siguiente para que se ubique
  P(mutexLibre);
  if (empty(C)) libre = true;
  else{
    pop(C, idNext);
    V(espera[idNext]);
  }
  V(mutexLibre); //libera el recurso

  //ya dejo pasar al siguiente, esta disponible para trabajar y si es el ultimo debe avisar que ya estan todos o caso contrario esperar a que llegue el ultimo
  if (last){
      for i in 1..4{
        V(reunion[idG]);
      }
  }
  else P(reunion[idG]);
  //ya se subio la barrera, tienen que trabajar
  P(mutexGrupo[idG]);
  while (cantMuebles[idG] > 0){
    hacerMueble(); 
    cantMueble[idG]++;
    V(mutexGrupo[idG]);
    P(mutexGrupo[idG]);
  }

  V(mutexGrupo[idG]);
  
  
  
}
