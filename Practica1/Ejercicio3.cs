/*
Realice una solución de grano grueso (utilizando <> y/o <await B; S>) 
para el siguiente problema. Un sistema operativo mantiene 5 instancias 
de un recurso almacenadas en una cola, cuando un proceso necesita usar
una instancia del recurso la saca de la cola, la usa y cuando termina
de usarla la vuelve a depositar.
*/

cola buffer;
int s = 5;
Process Proceso[id:0..P-1]
{ int idI;
  <await s > 0; pop(buffer,idI); s=s-1;>; //equivalente a P(s);
  usarInstancia(idI);
  <push(buffer, idI); s = s+1;>; 
}
