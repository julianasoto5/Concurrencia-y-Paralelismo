/*
  Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
  usando semáforos:
  Modifique la solución de (a) para el caso en que se deba respetar el orden de llegada.
*/

sem mutex = 1;
sem espera ([N] 0);
cola C;
boolean libre = true;
Process Persona[0..N-1]
{
  int next;
  //necesita fotocopiar
  P(mutex); //se queda esperando a que se libere la impresora
  if (libre){ //encuentra la impresora libre
    libre = false; //la marca como ocupada
    V(mutex); //avisa para que alguien la ocupe
  }
  else {//si la encuentra ocupada, se debe encolar y esperar su turno
    push(C,id);
    V(mutex);
    P(espera[id]);
  }
  Fotocopiar();
  
  P(mutex);
  if (empty(C))
    libre = true; //impresora libre
  else{ //hay que agarrar al siguiente de la cola
    pop(C, next);
    V(espera[next]);
  }
  V(mutex); //libera recurso
}
