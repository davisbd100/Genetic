# Algoritmo Genetico para la resolucion de dos funciones
Hecho en *C#* utilizando *WPF* con el framework *.NET 4.7.2*


# Funcionamiento
- Los valores posibles a modificar se muestran a la izquierda
- El valor de D, solo se podra modificar en el primer problema al ser una sumatoria, en el segundo siempre se tomara el valor de 7
- Se puede seleccionar el problema deseado en cualquier momento
- Las graficas mostradas se van actualizando a traves de las diferentes ejecuciones del algoritmo

## Funciones a minimizar
### Primer Funcion
![alt text](https://github.com/davisbd100/Genetic/blob/master/Resources/FirstFunction.PNG "Logo Title Text 1")
### Segunda Funcion
![alt text](https://github.com/davisbd100/Genetic/blob/master/Resources/SecondFunction.PNG "Logo Title Text 1")


## Definicion de forma de implementacion a tener en cuenta

|                          |                                                     |
|--------------------------|-----------------------------------------------------|
| Representación           | Real                                                |
| Inicialización           | Aleatoria                                           |
| Selección de padres      | Ruleta                                              |
| Numero de descendientes  | 2                                                   |
| Cruza                    | BLX-Alpha                                           |
| Mutación                 | Uniforme                                            |
| Reemplazo                | Reemplazar el peor                                  |