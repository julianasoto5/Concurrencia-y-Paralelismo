//Un sistema operativo mantiene 5 instancias de un recurso almacenadas en una cola. Cuando un 
//proceso necesita usar una instancia del recurso, la saca de la cola, la usa y cuando termina de 
//usarla la vuelve a depositar.

sem mutex = 1, recursos = 5;
cola C;

Process Proceso[id:0..N-1]
{ int rec;
  while (true)
  {
    P(recursos); //pide un recurso y si no hay se queda esperando hasta que se libere alguno
    P(mutex); //acceso a cola
    pop (C,rec);
    V(mutex); //acceso a cola

    usarRecurso(rec);
    P(mutex); //acceso a cola
    push(C,rec);
    V(mutex); //acceso a cola
    V(recursos); //libera un recurso
  }
}
