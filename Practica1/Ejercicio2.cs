/*
Realice una solución concurrente de grano de grueso (utilizando <> y/o <await B; S>) 
para el siguiente problema. Dado un número N verifique cuántas veces aparece ese número
en un arreglo longitudinal M. Escriba las condiciones que considere necesarias.
*/
int cant = 0;
int arreglo[M];

Process Contador[id:0..M-1]
{
  if (arreglo[id] == N) <cant++>;
}
