/*
Se tiene un curso con 40 alumnos. La maestra entrega una tarea distinta a cada alumno, 
luego cada alumno realiza su tarea y se la entrega a la maestra para que la corrija. Esta
revisa la tarea y si está bien le avisa al alumno que puede irse. Si la tarea está mal le
indica los errores y volverá a entregarle la tarea a la maestra para que realice la 
corrección nuevamente. Esto se repite hasta que la tarea no tenga errores.
*/

sem empezar[40] = ([40] 0); //para avisar que ya se le entrego la tarea al alumno
sem aviso = 0;  //para avisar que se entrego una tarea a la maestra
sem resul[40] = ([40] 0);
sem mutex = 1; //mutex cola

text tareas[40]; //entrega de tarea personalizada al alumno
cola entregas; //recurso compartido por 40 alumnos
text correcciones[40]; //entrega de correciones por parte de la maestra
boolean salir[40] = ([40] false);


Process Maestra
{ text tarea; 
  int idA, cantC = 0;
  boolean aux;
  for i in 0..39{
    tareas[i] = hacerTarea();
    V(empezar[i]);
  }
  
  while (cantC < 40){
    P(aviso); //espera a que alguien entregue algo
    P(mutex);
    pop(entregas, (idA,tarea));
    V(mutex);
    correccion = corregir(tareas[idA], tarea);
    if (correccion == NULL){ //no hay correcciones --> puede salir
      salir[idA] = true;
      cantC++; 
    } else{
      correcciones[idA] = correccion; 
    } 
    V(resul[idA]);
  }
  
}

Process Alumno[id:0..39]
{ text tp, tarea; boolean fin = false;
  P(empezar[id]);
  tp = tareas[id];
  tarea = hacerTarea(tp);
  while (!fin){
    P(mutex);
    push(entregas, (id,tarea));
    V(mutex);
    
    V(aviso);
    
    P(resul[id]); //esta listo el resultado
    fin = salir[idA];
    if (!fin){
      tarea = corregirTarea(tp, correcciones[idA]);
    }
    
  }

  
  
}
