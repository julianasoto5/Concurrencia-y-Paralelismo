/*
Existen N personas que deben fotocopiar un documento cada una. Resolver 
cada ítem usando monitores.

a) Implemente una solución suponiendo que existe una única fotocopiadora
compartida por todas las personas, y las mismas la deben usar de a una 
persona a la vez, sin importar el orden. Existe una función Fotocopiar()
que simula el uso de la fotocopiadora. Sólo se deben usar los procesos 
que representan a las Personas (y los monitores que sean necesarios).
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
    else libre = false;
  }
  Procedure Salir(){
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
