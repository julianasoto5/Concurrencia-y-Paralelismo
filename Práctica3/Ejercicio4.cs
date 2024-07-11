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
  int idAlumno;
  
  Procedure Llegada(idA: IN int; idP: OUT int){
    push(fila,idA);
    signal(prof);
    wait(espera[idA]);
    idP = profAsignado[idA];
  }

  Procedure Siguiente(idP: IN int; idA: OUT int){
    while (empty(fila))  wait(prof);
    pop(fila, idA);
    profAsignado[idA] = idP;
    signal(espera[idA]);
  }
}

Monitor Desk[id:0..2]{
  //se va a encargar de la interacción alumno-profesor
  
}
Process Alumno[id:0..29]
{ int idP, nota;
  Admin.Llegada(id, idP);
  Desk[idP].hacerExamen(nota);
  

}
Process Profesor[id:0..2]
{


}
