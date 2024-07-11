/*Implementar el acceso a una base de datos de solo lectura que puede 
atender a lo sumo 5 consultas simultáneas*/

//NO ESPECIFICA ORDEN
Monitor BD{
  int cantC = 5;
  cond espera;
  
  Procedure readRQ(id: in int){
    if (cantC == 0){ //no esta disponible
      wait(espera);
    }
    //esta disponible (cantC > 0 o salio del wait)  
    cantC--;
  }

  Procedure Salir(){
    cantC++;
    signal(cola);
  }
  
}

Process Usuario[id:0..U-1]
{
  while(true){
    BD.readRQ();
    hacerConsulta();
    BD.Salir();
  }
  
}

Process Admin{
  
}

