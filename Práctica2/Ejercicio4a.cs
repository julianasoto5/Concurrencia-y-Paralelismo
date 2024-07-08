/*Existen N personas que deben fotocopiar un documento cada una. Resolver cada 
  ítem usando semáforos.
  Implemente una solución suponiendo que existe una única fotocopiadora compartida
  por todas las personas,y las mismas la deben usar de a una persona a la vez, sin
  importar el orden. Existe una función Fotocopiar() llamada por la persona que 
  simula el uso de la fotocopiadora. Sólo se deben usar los procesos que representan
  a las Personas.

  */
sem mutex = 1;
Process Persona[id: 0..N-1]
{
  P(mutex); //se queda esperando a que mutex sea mayor a 0 --> impresora libre
  Fotocopiar();
  V(mutex); //libera el recurso
}
