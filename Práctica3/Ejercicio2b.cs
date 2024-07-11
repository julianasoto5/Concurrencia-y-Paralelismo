/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:
b) Modifique la solución de (a) para el caso en que se deba respetar el orden 
   de llegada
*/

Monitor Fotocopiadora{
  Procedure Usar(){
    
  }
  Procedure Salir(){
    
  }
}

Process Persona[id:0..N-1]
{
  Fotocopiadora.Usar();
  Fotocopiar();
  Fotocopiadora.Salir();
}
