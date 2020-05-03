# Procesamiento Asincrono
Hecho en *C#* utilizando *WPF* con el framework *.NET 4.7.2*


# Funcionamiento
1. La opcion 'Tamaño de poblacion' define la poblacion inicial que se generara.
1. La opcion 'Tamaño del tablero' define el tamaño del "tablero" que se ocupara, asi como las reinas que tendra el tablero, dado a la naturaleza del algoritmo, estos valores deben ser los mismos.
1. La opcion 'Evaluaciones maximas' define el numero de evaluaciones maximas que tomara el algoritmo, tomando como evaluacion el calculo de aptitud de un nuevo individuo tomando tambien en cuenta todas aquellas de la generacion inicial.
1. La opcion 'Probabilidad de mutación' define la probabilidad de mutación que se tendra cada vez que se genere un nuevo hijo. Esta mutacion se realiza intercambiando dos valores.
1. La opcion 'Numero de padres para selecciona' define el numero de padres que se seleccionaran aleatoriamente para hacer la comparacion de los mas aptos para cruza. Los padres siempre seran dos y generaran siempre dos hijos.

## Problema de las Ocho Reinas

El problema de las ocho reinas es un pasatiempo que consiste en poner ocho reinas en el tablero de ajedrez sin que se amenacen. En el juego del ajedrez la reina amenaza a aquellas piezas que se encuentren en su misma fila, columna o diagonal. El juego de las 8 reinas consiste en poner sobre un tablero de ajedrez ocho reinas sin que estas se amenacen entre ellas.

## Definicion de forma de implementacion a tener en cuenta

|                          |                                                     |
|--------------------------|-----------------------------------------------------|
| Representación           | Permutaciones                                       |
| Tamaño de la población   | 100                                                 |
| Inicialización           | Aleatoria                                           |
| Selección de padres      | Mejores de 5 aleatorios                             |
| Numero de descendientes  | 2                                                   |
| Cruza                    | Cortar y llenar cruzado                             |
| Probabilidad de mutacion | Intercambio                                         |
| Mutación                 | Intercambio                                         |
| Reemplazo                | Reemplazar el peor                                  |
| Condición de Paro        | Encontrar la solución o llegar a 10000 evaluaciones |