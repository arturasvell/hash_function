# hash_function
Programa, skirta testuoti ir lyginti skirtingiems hash algoritmams
# Versija **v0.05**
1. Implementuota dauguma žinomų hashavimo algoritmų, projektas paruoštas greičio analizei (naudojant Stopwatch objektą), kitoje versijoje bus pateiktas hash algoritmas
# Versija **v0.08**
1. Implementuota hash funkcija, kitoje versijoje bus pateiktas skaitymas iš failo ir analizė
# Custom hash funkcijos pseudokodas

initialize "keys" as integer array with 8 consecutive prime numbers
initialize "sum" with **magic number**
  for loop through each character of string
  {
      sum <-sum XOR (sum leftwise keys[ i mod 8]+ string[i]+(sum rightwise keys[(i+1) mod 8])
  }
  sum->((sum rightwise 8) XOR sum) XOR **another magic number**
  sum->((sum rightwise 8) XOR sum) XOR **another magic number**
  sum->((sum rightwise 8) XOR sum)
  return sum in HEX
