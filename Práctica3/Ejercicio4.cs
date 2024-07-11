/*
En una mesa de exámenes hay 3 profesores que les deben tomar un examen oral a 
30 alumnos de acuerdo al orden de llegada. Cada examen es tomado por un único
profesor. Cuando un alumno llega, espera a que uno de los profesores (cualquiera)
lo llame y se dirige al escritorio correspondiente a ese profesor, donde le 
tomará el examen. Al terminar, el profesor le da la nota y el alumno se retira. 
Cuando un profesor está libre llama al siguiente alumno. NOTA: todos los procesos
deben terminar su ejecución.
*/

Monitor Admin{
  //se va a encargar de coordinar un alumno con un profesor
  cola fila;
  cond prof;
  int profAsignado[N];
  int cantAlumnos = 0;
  
  Procedure Llegada(idA: IN int; idP: OUT int){
    push(fila,idA);
    signal(prof);
    wait(espera[idA]);
    idP = profAsignado[idA];
  }

  Procedure Siguiente(idP: IN int; idA: OUT int; fin: OUT boolean){
    while (empty(fila))  wait(prof);
    cantAlumnos++;
    pop(fila, idA);
    profAsignado[idA] = idP;
    signal(espera[idA]);
    fin = (cantAlumnos == 30);
  }
}

Monitor Desk[id:0..2]{
  //se va a encargar de la interacción alumno-profesor
  cond profesor;
  cond notaLista;
  text examenAlumno;
  boolean listo = false;
  int notas[N];
  
  Procedure hacerExamen(idA: IN int; nota: OUT int){
    examenAlumno = hacerExamen();
    listo = true;
    signal(profesor);
    wait(notaLista);
    nota = notas[idA];
  }

  Procedure EsperarExamen(examen: OUT text){
    if (not listo) wait(profesor);
    examen = examenAlumno;
  }

  Procedure MandarNota(idA: IN int; nota: IN int){
    notas[idA] = nota;
    signal(notaLista);
    wait(profesor);
    listo = false;
    
  }
  
}
Process Alumno[id:0..29]
{ int idP, nota;
  Admin.Llegada(id, idP);
  Desk[idP].HacerExamen(nota);
}
Process Profesor[id:0..2]
{ boolean fin = false; int idA, nota; text: examen; 
  do
    Admin.Siguiente(id, idA, fin);
    Desk[id].EsperarExamen(examen);
    nota = Corregir(examen);
    Desk[id].MandarNota(idA, nota);
  until (fin);

}
