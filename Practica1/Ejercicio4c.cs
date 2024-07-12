/*
4. En cada ítem debe realizar una solución concurrente de grano grueso (utilizando <> y/o
<await B; S>) para el siguiente problema, teniendo en cuenta las condiciones indicadas en el
item. Existen N personas que deben imprimir un trabajo cada una. 

c) Modifique la solución de (b) para el caso en que se deba respetar 
el orden de llegada pero dando prioridad de acuerdo a la edad de
cada persona (cuando la impresora está libre la debe usar la 
persona de mayor edad entre las que hayan solicitado su uso).
*/

int siguiente = -1;
cola c;
Process Persona[id:0..N-1]
{ text doc; int edad;
  doc = getDoc();
  edad = getEdad();
  <if (siguiente == -1) siguiente = id; //no hay nadie esperando
   else pushOrdenado(c,id,edad);>
   
  <await (siguiente == id)>
  //es su turno
  imprimir(doc);

  < if (empty(c)) siguiente = -1;
    else{pop(c,siguiente);>

  }
  
}
