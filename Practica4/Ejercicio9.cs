/**
Resolver con PMS el siguiente problema. En una exposicion aeronáutica hay un simulador de vuelo
(que debe ser usado con exclusión mutua) y un empleado encargado de administrar el uso del mismo.
Hay P personas que esperan a que el empleado lo deje acceder al simulador, lo usa por un rato y 
se retira. El empleado deja usar el simulador a las personas respetando el orden de llegada. NOTA: 
cada persona una solo una vez el simulador.
**/


Process Persona[0..P-1]
{
  Admin!llegada(id);
  Empleado?usarSimulador();
  usarSimulador();
  Empleado!fin();
}

Process Empleado
{ int cantP, idP;
  cantP = 0;
  while (cantP < P){
    Admin!autorizo(); 
    Admin?siguiente(idP);
    Persona[idP]!usarSimulador();
    Persona[idP]?fin();
    cantP++;
  }
}

Process Admin
{ cola Buffer;
  int idP;
  while (cantP < P){
    if Persona[*]?llegada(idP) -> push (Buffer, idP); 
    [] not empty (Buffer); Empleado?autoriza() -> cantP++;
            pop (Buffer,idP);
            Empleado!siguiente(idP);
    fi
    
  }
}

