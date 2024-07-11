/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:

e) Modifique la solución de (b) para el caso en que además haya un Empleado que 
le indica a cada persona cuándo debe usar la fotocopiadora.
*/
//ES COMO EL EJERCICIO 6 DE LA EXPLICACIÓN PRÁCTICA???

Monitor Fotocopiadora{
  
  colaOrdenada c;
  cond esperar[N];
  cond empleado; //despierta al empleado si la fila estaba vacía
  cond libre; //despierta al empleado luego de usar la fotocopiadora
  
  Procedure Usar(idP: IN int){
    if (empty(c))
      signal(empleado); //despierta al empleado 
    push(c, id);
    wait(esperar[id]);
    Fotocopiar();
    signal(libre);
    }  
  }

  Procedure Administrar(){
    int idP;
    if (empty(c)) wait(empleado); //si la cola está vacía se queda esperando a que entre alguien
    
    pop(c,idP); //seguro hay alguien en la cola, lo desencolo
    signal(espera[idP]); //despierto al proceso id=idP; --> le doy permiso para fotocopiar
    wait(libre); //me quedo esperando a que libere la impresora
  }
}

Process Persona[id:0..N-1]
{ 
   Fotocopiadora.Usar(id);
}

Process Empleado
{  int idP;
  for i in 1..N{
    Fotocopiadora.Administrar();
  }
}
