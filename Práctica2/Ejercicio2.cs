/**
A una cerealera van T camiones a descargarse trigo y M camiones a descargar maíz. Sólo hay
lugar para que 7 camiones a la vez descarguen pero no pueden ser más de 5 del mismo tipo
de cereal. NOTA: solo se pueden usar procesos que representen a los camiones.
**/

sem mutex = 1, lugar_maiz = 5, lugar_trigo = 5, lugares = 7; 

int cantLugares = 0;
Process CamionTrigo[id:0..T-1]
{
  while(true){
    P(lugar_trigo); //se queda esperando a que haya lugar para un camion de trigo

    P(lugares); //se queda esperando a que haya lugar para un camion

    descargar();

    V(lugares); // libera lugar para un camion

    V(lugar_trigo); //libera lugar para un camion de trigo
  }

}

Process CamionMaiz[id:0..M-1]
{
  while (true){
    P(lugar_maiz); //se queda esperando a que haya lugar para un camion de maiz

    P(lugares); //se queda esperando a que haya lugar para un camion

    descargar();

    V(lugares); // libera lugar para un camion

    V(lugar_maiz); //libera lugar para un camion de maiz
  }
}
