/*
Existen N personas que deben fotocopiar un documento cada una. Resolver cada ítem
usando monitores:
b) Modifique la solución de (a) para el caso en que se deba respetar el orden 
   de llegada
*/

Monitor Fotocopiadora{
  cond esperar;
  boolean libre = true;
  int esperando = 0;
  
  Procedure Usar(){
    if (not libre){
      esperando++;
      wait(esperar);
    }
    else libre = false; //si no estaba marcado como ocupado ahora sí lo está
  }
  Procedure Salir(){ //hay que darle "prioridad" a quien ya esta en la cola. Además se pierde el EM
    if (esperando == 0)  libre = true;
    else{ 
      esperando--;
      signal(esperar);
    }
  }
}


Process Persona[id:0..N-1]
{
  Fotocopiadora.Usar();
  Fotocopiar();
  Fotocopiadora.Salir();
}

