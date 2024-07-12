/*
6) En una playa hay 5 equipos de 4 personas cada uno (en total son 20 personas 
donde cada una conoce previamente a que equipo pertenece). Cuando las personas 
van llegando esperan con los de su equipo hasta que el mismo esté completo 
(hayan llegado los 4 integrantes), a partir de ese momento el equipo empieza a 
jugar. El juego consiste en que cada integrante del grupo junta 15 monedas de 
a una en una playa (las monedas pueden ser de 1, 2 o 5 pesos) y se suman los 
montos de las 60 monedas conseguidas en el grupo. Al finalizar cada persona 
debe conocer el monto total juntado por su grupo. NOTA: maximizar la concurrencia.
Suponga que para simular la búsqueda de una moneda por parte de una persona
existe una función Moneda() que retorna el valor de la moneda encontrada.
*/

Monitor Grupo[0..4]{
  cond barrera;
  cond resul;
  int cant = 0, R = 0;
  
  Procedure Llegada{
    cant++;
    if (cant == 4){ //es el ultimo
      signal_all(barrera); //despierta a los otros 3 
    }
    else wait(barrera);
    //en este punto ya llegaron los 4 integrantes
  } 

  Procedure Sumar(cantM: IN int; R: OUT int){
    R+=cantM;
    cant--;
    if (cant == 0){
      //ya sumaron las 60 monedas en R
      signal_all(resul);
    }
    else wait(resul);
  }
}

Process Persona[id:0..19]
{ int idG, cant, resul;
  idG = getGrupo();
  Grupo[idG].Llegada();
  for i in 1..15{
    cant += Moneda;
  }
  Grupo[idG].Sumar(cant, resul);
  
}
