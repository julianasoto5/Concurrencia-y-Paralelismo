/**
Resolver con PMS el siguiente problema. En un examen final hay P alumnos y 3 profesores.
Cuando todos los alumnos han llegado comienza el examen. Cada alumno resuelve su examen,
lo entrega y espera a que alguno de los profesores lo corrija y le indique la nota. Los
profesores corrigen los exámenes respetando el orden en que los alumnos van entregando. 
**/


Process Alumno[id: 0..P-1]
{ Examen examen;
  int nota;
  Admin!llegada(1);
  Admin?empezarExamen(examen);
  examen = realizarExamen(examen);
  Admin!entregarExamen(id,examen);
  Profesor[*]?recibirNota(nota);
}

Process Profesor[id: 0..2]
{ Examen examen;
  int nota, idA;
  while (true){
    Admin!pedidoExamen(id);
    Admin?recibirExamen(idA,examen);
    nota = corregirExamen(examen);
    Alumno[idA]!mandarNota(examen);
  }
}

Process Admin
{ int idA, idP, nota, i = 0;
  Examen examen;
  cola BufferExamen, BufferNotas;
  while (i<P){
   Alumno[*]?llegada(ok)
   i++;
  }
  i=0;
  //llegaron todos los alumnos y se les debe dar el ok para empezar el parcial
  while (i<P) {
    Alumno[i]!empezarExamen();
    i++;
  }

  //entrega de examenes y aviso a los profesores de correccion

  do Alumno[*]?entregarExamen(idA, examen) -> push(Buffer,(idA,examen));
  [] not empty(BufferExamen); Profesor[*]?pedidoExamen(idP) -> pop(BufferExamen,(idA,examen))
                Profesor[idP]!recibirExamen(idA, examen);
  od

}
