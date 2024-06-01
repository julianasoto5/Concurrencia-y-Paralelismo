/**
Suponga que existe un antivirus y distribuido en él hay R procesos robots que continuamente
están buscando posibles sitios web infectados; cada vez que encuentran uno avisan la dirección
y continúan buscando. Hay un proceso analizador que se encarga de hacer todas las pruebas 
necesarias con cada uno de los sitios encontrados por los robots para determinar si están o no
infectados.
**/



Process Robot[id: 0..R-1]
{ Direccion direccion;
  while (true){
    if (detectarSitioInfectado(direccion)){
      Admin!sitioDetectado(direccion);
    }
  }
}

Process Analizador
{ Direccion
  while (true){
    Admin!pedidoDireccion();
    Admin?hacerPruebas(direccion);
    realizarPruebas(direccion);
  }
}

Process Admin{
  cola Buffer;
  Direccion direccion;
  do  Robot[*]?sitioDetectado(direccion) -> push (Buffer, direccion);
  []  not empty (Buffer); Analizador?pedidoDireccion() -> Analizador!hacerPruebas(pop(Buffer,direccion));
  od
}


