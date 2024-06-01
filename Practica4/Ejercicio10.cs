/**
Resolver con PMS el siguiente problema. En un estadio de futbol hay una máquina 
expendedora de gaseosas que debe ser usada por E Espectadores de acuerdo al orden
de llegada. Cuando el espectador accede a la máquina en su turno usa la máquina y 
luego se retira para dejar al siguiente. NOTA: cada Espectador usa sólo una vez
la máquina.
**/


Process Espectador[id: 0..E-1]
{
  Admin!llegada(id);
  Maquina?usarMaquina();
  usarMaquina();
  Maquina!fin();
}

Process Maquina
{ int idE, cantE = 0; 
  while (cantE < P){
    Admin!ready();
    Admin?siguiente(idE);
    Espectador!usarMaquina();
    Espectador?fin();
    cant++;
  }
  
}


Process Admin
{ int cantE, idE;
  cola Buffer;
  cantE = 0;
  
  while (cantE < E){
    if Espectador?llegada(idE) -> push (Buffer, idE);
    [] not empty(Buffer); Maquina?ready() -> pop (Buffer,idE)
                                             cantE++;
                                             Maquina!siguiente(idE);
  }
}
