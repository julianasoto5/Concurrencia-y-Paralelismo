/*
4.En cada ítem debe realizar una solución concurrente de grano grueso 
  (utilizando <> y/o <await B; S>) para el siguiente problema, teniendo 
  en cuenta las condiciones indicadas en el item. Existen N personas que 
  deben imprimir un trabajo cada una. 
  
b) Modifique la solución de (a) para el caso en que se deba respetar 
   el orden de llegada.
*/

int siguiente = -1;
cola c;
Process Persona[id:0..N-1]
{ text doc; int next;
  doc = getDoc();
  <if (siguiente == -1) siguiente = id;
   else push(c,id);>
   
  <await (siguiente == id)>
  //es su turno
  imprimir(doc);

  < if (empty(c)) siguiente = -1;
    else{pop(c,siguiente);>

  }

  
}
