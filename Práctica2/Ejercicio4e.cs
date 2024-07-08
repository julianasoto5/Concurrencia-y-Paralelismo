/*
  Modifique la solución de (b) para el caso en que además haya un Empleado
  que le indica a cada persona cuándo debe usar la fotocopiadora.
*/
  sem mutex = 1, libre = 1;
  sem espera([N] 0);
  sem pedido = 0;
  cola C;

  Process Persona[id:0..N-1]
  {
    //llega a la fotocopiadora
    P(mutex); //la cola es un recurso compartido
    push(C,id);
    V(mutex);
    
    V(pedido); //avisa que hay alguien en la cola
    P(espera[id]); //espera el OK del empleado
    Fotocopiar();
    V(libre); //avisa al empleado que libera el recurso
  
  }

  Process Empleado{
  int i,id;
    for i in 0..N-1{
      P(pedido); //se queda esperando a que llegue alguien
      
      P(mutex); //la cola es un recurso compartido
      pop(C,id);
      V(mutex);
      
      V(espera[id]);
      P(libre); 
    }
  }
