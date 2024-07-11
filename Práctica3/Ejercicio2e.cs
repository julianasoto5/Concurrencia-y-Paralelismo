/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:

e) Modifique la solución de (b) para el caso en que además haya un Empleado que 
le indica a cada persona cuándo debe usar la fotocopiadora.
*/
//ES COMO EL EJERCICIO 6 DE LA EXPLICACIÓN PRÁCTICA

Monitor Fotocopiadora{
  boolean libreEmp = true;
  cond esperar;
  int esperando = 0;
  Procedure Llegada(){
    if (not libreEmp){ //el empleado esta ocupado, debe esperar
      wait(esperar);
      esperando++; //uso ordenado de la fotocopiadora
    }  
  }
}

Process Persona[id:0..N-1]
{ 
   Fotocopiadora.Llegada();
   Fotocopiar();
   Fotocopiadora.Salir();
}

Process Empleado
{
  
}
