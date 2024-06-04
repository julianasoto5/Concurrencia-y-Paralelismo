/**
Resolver con PMS el siguiente problema. En un examen final hay P alumnos y 3 profesores.
Cuando todos los alumnos han llegado comienza el examen. Cada alumno resuelve su examen,
lo entrega y espera a que alguno de los profesores lo corrija y le indique la nota. Los
profesores corrigen los exámenes respetando el orden en que los alumnos van entregando. 
**/


Process Alumno[id: 0..P-1]
{ Examen examen;
  int nota;
  Admin!llegada();
  Admin!pedidoEmpezarExamen(id);
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

  //registro de llegada de alumnos
  do (cantP < P); Alumno[*]?llegada() -> cantP++;
  od

  //llegaron todos los alumnosn (cantP = P) -> dar el ok para empezar
  do (cantP > 0); Alumno[*]?pedidoEmpezarExamen(id) -> Alumno[id]!empezarExamen(); cantP--;
  od

  //entrega de examenes y aviso a los profesores de correccion

  do (cantP < P); Alumno[*]?entregarExamen(idA, examen) -> push(Buffer,(idA,examen));  cantP++;//quedan alumnos para entregar
  [] not empty(BufferExamen); Profesor[*]?pedidoExamen(idP) -> pop(BufferExamen,(idA,examen)) //se va a vaciar el Buffer cuando los profesores terminen de corregir a todos
                Profesor[idP]!recibirExamen(idA, examen);
  od

// esta bien separar el ok del empiezo del examen de la entrega y correccion?

}
