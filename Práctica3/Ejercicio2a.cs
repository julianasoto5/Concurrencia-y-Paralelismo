/*
Existen N personas que deben fotocopiar un documento cada una. Resolver 
cada ítem usando monitores.

a) Implemente una solución suponiendo que existe una única fotocopiadora
compartida por todas las personas, y las mismas la deben usar de a una 
persona a la vez, sin importar el orden. Existe una función Fotocopiar()
que simula el uso de la fotocopiadora. Sólo se deben usar los procesos 
que representan a las Personas (y los monitores que sean necesarios).
*/

/*Las personas van a competir por usar el recurso Fotocopiadora haciendo 
el llamado Fotocopiadora.Usar(), donde UNO SOLO va a poder realizar el 
procedimiento por vez.
*/
Monitor Fotocopiadora{ //es sin orden--> monitor se usa como representacion del propio recurso
  Procedure Usar(){
    Fotocopiar();
  }
}


Process Persona[id:0..N-1]
{  
  Fotocopiadora.Usar();
}
