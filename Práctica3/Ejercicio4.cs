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
  cola prof, fila;
  int idAlumno;
  Procedure Llegada(idA: IN int){
    push(fila,idA);
    wait(espera[idA]);
  }

  Procedure Siguiente(idP: IN int; idA: OUT int){
    if (empty(fila))  wait(prof);
    
    
  }
}

Monitor Desk{
  //se va a encargar de la interacción alumno-profesor
  
}
Process Alumno[id:0..29]
{
  Admin.Llegada();

}
Process Profesor[id:0..2]
{

}
