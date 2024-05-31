/**
Resolver la administracion de las impresoras de una oficina. Hay 3 impresoras, 
N usuarios y 1 director. Los usuarios y el director están continuamente trabajando
y cada tanto envían documentos a imprimir. Cada impresora, cuando está libre, toma 
un documento y lo imprime, de acuerdo al orden de llegada, pero siempre dando
prioridad a los pedidos del director. Nota: los usuarios y el director no deben 
esperar a que se imprima el documento.
**/

chan listaImpresora[3] (texto);
chan DocumentosDirector(texto);
chan DocumentosUsuarios(texto);
chan Aviso(int);

Process Impresora[id: 0..2]
{ int ready;
  texto doc;
  while (true){
    receive listaImpresora[id](doc);
    Imprimir(doc);
  }
}

Process Usuario[id: 0..N-1]{
  while (true){
      while (noNecesiteImprimir());
      doc = documento_a_imprimir();
      send DocumentosUsuarios(doc);
      send Aviso(1);
  }
  
}

Process Director{
  while (true){
    while (noNecesiteImprimir());
    doc = documento_a_imprimir();
    send DocumentosDirector(doc);
    send Aviso(1);
  }
}


Process Coordinador //coordina entre las tres impresoras. Si alguna esta libre, le manda el documento
{ int ok;
  while (true){
    receive Aviso(ok); //alguien quiere imprimir

    //chequeamos quien quiere imprimir de acuerdo a la prioridad D > U
    if (not empty(DocumentosDirector)) ->
      receive DocumentosDirector(doc);
    [] if ((not empty(DocumentosUsuarios)) and (empty(DocumentosDirector)))->
          receive DocumentosUsuarios(doc);
    fi
    
    //chequeamos cual impresora esta vacia y le mandamos el documento
    if (empty (listaImpresora[0]))->
        send listaImpresora[0](doc);
    [] if (empty (listaImpresora[1]))->
        send listaImpresora[1](doc);
    [] if (empty (listaImpresora[2]))->
        send listaImpresora[2](doc);
    fi
    
  }
}
