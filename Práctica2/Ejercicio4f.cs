/*
  Modificar la solución (e) para el caso en que sean 10 fotocopiadoras. 
  El empleado le indica a la persona cuando puede usar una fotocopiadora,
  y cual debe usar.
*/

sem mutex = 1, mutex2 = 1, libre = 10, pedidos = 0, espera ([N] 0);
int id_impresora[N];
Cola llegadas, impresoras;
Process Persona[id:0..N-1]
{
  int id_imp;
  P(mutex);
  push(llegadas, id);
  V(mutex);

  V(pedidos);

  P(espera[id]);
  id_imp = id_impresora[id];
  Fotocopiar();

  P(mutex2);
  push(id_imp);
  V(mutex2);
  
  V(libre);
  }

  Process Empleado
  { int i,idP,idI;
    for i in 1..10
      push(impresoras,i);
      
    for i in 1..N{
      P(pedidos); //llego alguien
      
      P(mutex);
      pop(llegadas, idP);
      V(mutex);

      P(libre); //se va a usar una impresora
      P(mutex2);
      pop(impresoras,idI);
      V(mutex2);
      
      id_impresora[idP] = idI;
      V(espera[idP]);
      
    }
  }

  
  
}
