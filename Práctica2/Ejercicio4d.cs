/*
Modifique la solución de (a) para el caso en que se deba respetar 
estrictamente el orden dado por el identificador del proceso (la persona
X no puede usar la fotocopiadora hasta que no haya terminado de usarña la 
persona X-1)
*/

sem mutex (0 1, [N-1] 0); //todos en 0 menos el primero
Process Persona[id:0..N-1]
{
  //se queda esperando a que id-1 use la fotocopiadora
  P(mutex[id]);
  Fotocopiar();
  V(mutex[id+1]); 
}

//NO SE SI SE PUEDE INICIALIZAR EL SEMAFORO CON EL PRIMERO EN 1 Y EL RESTO EN 0
sem mutex = 1, espera([N],0);
Process Persona[id: 0..N-1]
{
  if (id != 0){
    P(espera[id]);
  }
  Fotocopiar();
  V(espera[id+1]);
  
}
