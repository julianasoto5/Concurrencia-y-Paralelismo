/*
  Modifique la solición de (b) para el caso de dar prioridad de acuerdo a la edad de
  cada persona (cuando la fotocopiadora esté libre, la debe usar la persona de mayor
  edad entre las que estén esperando para usarla).
*/

//no me termina de cerrar que se use mutex para la cola y para la fotocopiadora???
sem mutex = 1;
sem espera ([N] 0);
boolean libre = true;
Process Persona[id: 0..N-1]
{
  int next, edad;
  edad = getEdad();
  P(mutex);
  if (libre){ //impresora libre
    libre = false; //la marca ocupada para usarla
    V(mutex); //habilita algo
  }
  else{ //impresora ocupada, debe esperar
    pushOrdenado(C,id,edad); //se encola de acuerdo a la edad
    V(mutex); //habilita algo
    P(espera[id]); //espera su turno
  }
  Fotocopiar(); //usan fotocopiadora
  //Recurso compartido -> COLA
  P(mutex); //espera a algo
  if (empty(C)) 
    libre = true;
  else
  {
    pop(C,next);
    V(espera[next]);
  }
  V(mutex);
}
