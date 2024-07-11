/*
Suponga una comisión de 50 alumnos. Cuando los alumnos llegan forman una fila,
una vez que están los 50 en la fila el jefe de trabajos prácticos les entrega
el número de grupo (aleatorio del 1 al 25) de tal manera que dos alumnos tendrán
el mismo número de grupo (suponga que el jefe posee una función DarNumero() que 
devuelve en forma aleatoria un número del 1 al 25, el jefe de trabajos prácticos
no guarda el número que le asigna a cada alumno). Cuando un alumno ha recibido
su número de grupo comienza a realizar la práctica. Al terminar de trabajar, el 
alumno le avisa al jefe de trabajos prácticos y espera la nota. El jefe de 
trabajos prácticos, cuando han llegado los dos alumnos de un grupo les devuelve
a ambos la nota del GRUPO (el primer grupo en terminar tendrá como nota 25, el 
segundo 24, y así sucesivamente hasta el último que tendrá nota 1).
*/

//pasaron cosas raras asiq no hay nada garantizado
Monitor Admin{
  int cantA = 0, i;
  int IDgrupos[50];
  cola fila, tp;
  cond barrera; //esperan los alumnos a que lleguen todos y que el jtp les mande un grupo
  cond jtp; //avisa que debe asignarle a la siguiente persona un grupo
  cond corregir; //avisa que hay un tp listo
  cond notaLista[25]; 
  int notas[25], llegadas[25];
  //Codigo de inicializacion del monitor
  {
    for i in 0..24{
      llegadas[i] = 0;
    }
    
  }
  Procedure Llegada(idA: IN int; idG: OUT int){
    push(fila, idA);
    cantA++;
    if (cantA == 50){ //hay que subir la barrera
      signal(jtp); //avisa al JTP que ya estan todos los alumnos -> debe empezar a asignar grupos
    }
    wait(barrera); //se duermen en barrera los 50 alumnos -> coincide posicion fila-barrera
    idG = IDgrupos[idA];
  }

  Procedure EsperarNota(idA,idG: IN int; nota: OUT int){
    llegadas[idG-1]++;
    if (llegadas[idG-1] == 2){
      push(tp, idG);
      signal(corregir);
    }
    wait(notaLista[idG-1]);
    nota = notas[idG-1];
  }

  Procedure Siguiente(idA: OUT int){
    wait(jtp);
    pop(fila, idA);
  }

  Procedure MandarGrupo(idA: IN int; idG: IN int){
    IDgrupo[idA] = idG;
    signal(barrera);
    cantA--;
    if (cantA > 0)
      signal(jtp);
  }

  Procedure RecibirTrabajo(idG: OUT int){
    if (empty(tp)) wait(corregir);
    pop(tp, idG);
  }

  Procedure MandarNota(idG, nota: IN int){
    notas[idG-1] = nota;
    for i in 1..2  signal(notaLista[idG-1]);
    
  }

}


Process Alumno[id:0..49]
{ int idG, nota;
  Admin.Llegada(id,idG);
  realizarTrabajo();
  Admin.EsperarNota(id,idG, nota);
}

Process JTP
{ int i, idA, idG, nota;
  for i in 1..50{
    Admin.Siguiente(idA);
    idG = DarNumero();
    Admin.MandarGrupo(idA,idG);
  }

  for i in 1..25{
    Admin.RecibirTrabajo(idG);
    nota = i;
    Admin.MandarNota(idG,nota);
  ]

  
}
