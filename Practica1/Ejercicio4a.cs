/*
4) En cada ítem debe realizar una solución concurrente dde grano grueso 
(utilizando <> y/o <await B; S>) para el siguiente problema, teniendo en
cuenta las condiciones indicadas en el ítem. Existen N personas que deben
imprimir un trabajo cada una.
a)Implemente una solución suponiendo que existe una única impresora 
  compartida por todas las personas, y las mismas la deben usar de a 
  una persona a la vez, sin importar el orden. Sólo se deben usar los
  procesos que representan a las Personas.
*/

boolean libre = true;

Process Persona[id:0..N-1]
{ text doc;
  <await libre; libre = false>;
  imprimir(doc);
  libre = true;
}
