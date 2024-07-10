/*
Resolver con SEMÁFOROS el siguiente problema. Se debe simular el uso de una máquina expendedora de gaseosas
con capacidad para 100 latas por parte de U usuarios. Además existe un repositor encargado de reponer las latas de
la máquina. Los usuarios usan la máquina según el orden de llegada. Cuando les toca usarla, sacan una lata y luego
se retiran. En el caso de que la máquina se quede sin latas, entonces le debe avisar al repositor para que cargue
nuevamente la máquina en forma completa. Luego de la recarga, saca una lata y se retira. Nota: maximizar la
concurrencia; mientras se reponen las latas se debe permitir que otros usuarios puedan agregarse a la fila.
*/


sem mutexLibre = 1, aviso = 0, listo = 0, espera([U] 0);
int cantLatas = 100; //recurso compartido
boolean libre = true; //recurso compartido
cola C;
Process Usuario[id:0..U-1] //orden de llegada, si la máquina está libre le tiene que usar, no encolarse
{ int idNext;
  //llega a la maquina
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

  //ES SU TURNO --Solo un proceso va a estar haciendo este codigo y nadie pregunta por la cantLatas --> no hay necesidad de proteger cantLatas
  
  //intenta agarrar una lata
  if (cantLatas == 0)
    //NO HAY MAS LATAS
    V(aviso);
    P(listo);//espera a que el repositor reponga las latas
    
  }
  cantLatas = cantLatas-1;
  agarrarLata();
  
  //SE TIENE QUE IR

  P(mutexLibre);
  if (empty(C)) libre = true; //pregunta si la cola esta vacia porque sino le tiene que dar prioridad a los que estan esperando
  else{
    pop(C, idNext);
    V(espera[idNext]);  //despierta al siguiente en la fila
  }
  V(mutexLibre);


}

Process Repositor
{
 while (true){
   P(aviso); //<await (s>0); s=s-1 > --> alguien tiene que hacer V(aviso) --> usuario 101
   cantLatas = 100;
   V(listo); //le avisa al usuario que lo encontro vacio que ahora puede agarrar
 } 
}


