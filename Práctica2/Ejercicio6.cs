/*
Una empresa de turismo posee 4 combis con capacidad para 25 personas cada una 
y UN vendedor que vende los pasajes a los clientes de acuerdo al orden de llegada.
Hay C clientes que al llegar intentan comprar un pasake para una combi en 
particular (el cliente conoce este dato); si aún hay lugar en la combi seleccionada
se le da el pasaje y se dirige hacia la combi; en caso contrario se retira. Cada
combi espera a que suban los 25 pasajeros, luego realiza el viaje y cuando llega 
al destino deja bajar a todos los pasajeros. NOTA: maximizar la concurrencia, 
suponga que para cada combi al menos 25 clientes intentarán comprar pasaje.
*/
sem combi([4] 0); //aviso para combi llena
sem mutexCola = 1; //uso de recurso compartido
sem pedidos = 0; //evitar busy waiting en vendedor
sem espera ([C] 0); //espera del cliente
sem finRecorrido ([C] 0); //fin recorrido
boolean pasajes[C];
int pasajeros[4][25]; //guarda el ID de cada cliente para q la combi se comunique

cola C;

//para una barrera necesito un semaforo de barrera y una variable compartida
sem barrera ([C] 0);
int cantP[4] = ([4] 0); 


Process Cliente[id:0..C-1]
{ int idC;
  idC = getIDCombi();
  P(mutexCola); //manda pedido de combi por orden de llegada
  push(C, (id,idC));
  V(mutexCola);
  
  V(pedidos);
  P(espera[id]);
  
  if (pasajes[id]){ //hay lugar en la combi
    subirCombi(idC);
    P(barrera[id]);
    P(finRecorrido[id]);
  }
  //se va
}

Process Vendedor
{ int i, idP, idC;

  for i in 1..C{
    P(pedidos);

    P(mutexCola);
    pop(C, (idP, idC));
    V(mutexCola);
      
    if(cantP[idC] < 25){
      pasajes[idP] = true;
      pasajeros[idC][cantP] = idP; //guarda ID del cliente en la combi
      cantP[idC] = cantP[idC]+1;
      V(espera[idP);
    }
    else{
      pasajes[idP] = false;
      V(espera[idP]);
      V(combi[idC]);
    }
  }
}

Process Combi[id: 0..3]
{ int i;
  P(combi[id]); //espera a que el vendedor le de el OK  
  for i in 0..24{//manda barrera
    V(barrera[i]);
  }
  hacerViaje();
  for i in 0..24{
    V(finRecorrido[i]);
  }


}
