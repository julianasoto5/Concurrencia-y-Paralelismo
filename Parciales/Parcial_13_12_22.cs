/*
Resolver con MONITORES el siguiente problema. En un comedor estudiantil hay un horno
microondas que debe ser usado por E estudiantes de acuerdo al orden de llegada. 
Cuando el estudiante accede al horno, lo usa y luego se retira para dejar al siguiente.
NOTA: cada Estudiante usa solo una vez el horno; los unicos procesos que se pueden
usar son los estudiantes.
*/

Monitor Horno{ //administrador de acceso
  boolean libre = true;
  cond fila;
  int next, esperando = 0;
  
  Procedure Pedido(idE: IN int){
    if (libre)
      libre = false; //ahora esta ocupado
    else{ //hay fila
      esperando++;
      wait(fila);
    }
    //lo puede usar
  }

  Procedure Liberar(){
    if (esperando == 0) libre = true; //si no hay nadie esperando para usarla, queda libre
    else{
      esperando--;
      signal(fila);
    }
  }
}

Process Estudiante[id: 0..E-1]{
  Comedor.Pedido(id);
  usarHorno();
  Comedor.Liberar();
}
