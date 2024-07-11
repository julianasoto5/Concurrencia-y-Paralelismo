/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:

c) Modifique la solución de (b) para el caso en que se deba dar prioridad
de acuerdo a la edad de cada persona (cuando la fotocopiadora está libre
la debe usar la persona de mayor edad entre las que estén esperando para 
usarla).

*/
/*COMO LA "COLA" ESTÁ MODIFICÁNDOSE CON CADA PERSONA DE ACUERDO A SU EDAD, 
  NO SE PUEDE USAR UN COND. SE USA UNA COLA ORDENADA (y se le puede hacer 
  empty())
*/

Monitor Fotocopiadora{
  cola esperar;
  boolean libre = true;
  cond permiso[N];
  int idAux;
  
  Procedure Usar(idP: IN int; edad: IN int){
    if (not libre){
      pushOrdenado(esperar,idP, edad);
      wait(permiso[idP]); //es PRIVADO, solo esta la persona con id=idP esperando ahi
    }
    else libre = false;
  }
  Procedure Salir(){
    if (empty(esperar)) libre = true;
    else{ 
      pop(esperar,idAux);
      signal(permiso[idAux]); //despierta al proceso de id=idAux
    }
  }
}


Process Persona[id:0..N-1]
{ int edad;
  edad = getEdad();
  Fotocopiadora.Usar(id,edad);
  Fotocopiar();
  Fotocopiadora.Salir();
}
