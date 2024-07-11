/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:
d) Modifique la solución de (a) para el caso en que se deba respetar estrictamente
el orden dado por el identificador del proceso (la persona X no puede usar la 
fotocopiadora hasta que no haya terminado de usarla la persona X-1).
*/

Monitor Fotocopiadora{
  cond esperar[N];
  int act = 0;
  Procedure Usar(idP: IN int){
    if (idP > act) wait(esperar[idP]); //si el proceso es mayor al esperado entonces duerme 
    Fotocopiar();
    act++;
    if (act != N) signal(esperar[act]);
  }
}

Process Persona[id:0..N-1]
{
  Fotocopiadora.Usar(id);
}
