/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:

f) Modifique la solución de (e) para el caso en que sean 10 fotocopiadoras. El Empleado
le indica a la persona cuándo puede usar la fotocopiadora, y cuál debe usar.
*/
//ES COMO EL EJERCICIO 7 DE LA EXPLICACIÓN PRÁCTICA??

Monitor Fotocopiadora{
  int i;
  int IDfoto[N];
  cola fotoLibre;
  cola c;
  cond esperar[N];
  cond empleado; //despierta al empleado si la fila estaba vacía
  cond libre; //despierta al empleado luego de usar la fotocopiadora
  boolean esperando = false;
  Procedure Usar(idP: IN int; idF: OUT int){
    if (empty(c))
      signal(empleado); //despierta al empleado 
    push(c, idP);  //se pone en la fila
    wait(esperar[idP]); //espera a que le digan el ID de la fotocopiadora
    idF = IDfoto[idP];
    }  
  }

  Procedure Salir(idF: IN int){
    push(fotoLibre,idF);
    if (esperando) //podria ser un not empty(c)??
      signal(libre); //si el empleado esta esperando una fotocopiadora libre, lo despierta
  }

  Procedure Administrar(){
    int idP, idF;
    if (empty(c)) wait(empleado); //si la cola está vacía se queda esperando a que entre alguien
    if (empty (fotoLibre)) {  
      esperando = true;
      wait(libre); //se queda esperando a que se libere una fotocopiadora
    }
    
    pop(c,idP); //seguro hay alguien en la cola y hay una fotocopiadora libre, lo desencolo
    pop(fotoLibre, idF);
    IDfoto[idP] = idF;
    signal(espera[idP]); //despierto al proceso id=idP; --> le doy permiso para fotocopiar
  }
//codigo de inicializacion del monitor
{
  for i in 1..10
    push(fotoLibre, i);
}

  
}

Process Persona[id:0..N-1]
{ int idF;
   Fotocopiadora.Usar(id,idF);
   Fotocopiar(idF);
   Fotocopiadora.Salir(idF);
}

Process Empleado
{  int idP;
  for i in 1..N{
    Fotocopiadora.Administrar();
  }
}
