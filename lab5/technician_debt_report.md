# Технический долг
+ Анализ по состоянию на 24.04.2020.  
+ Оценки производятся в SP равных 2 часам .  
# Анализ
### 1.	Оформление
1. WHAT : Папка в которой расположен код недостаточно структурирована. В документах где расписана большая часть информации  добавлены ссылки на  разделы, содержания и подпункты. Сопроводительные документы также хорошо структурированы.  
TO BE : Чтобы информация была легко доступной, документы должны быть размещены по соответствующим для этого папкам. Доступ к которым будет осуществляться по ссылкам. Для этого необходимо структурировать папки с кодом.  
COST : 0.15  
PRIORITY : MINOR  
2. WHAT : В Trello некорректно сформулированы задачи, выполняемые в этот промежуток времени.  
TO BE : Разместить задачи 3-го спринта в нужные списки. Необходимо редактировать задачи, которые уже были реализованы и которые сейчас находятся в процессе.  
COST : 0.1  
PRIORITY : MINOR  
3. WHAT : Явное отсутствие лицензирования исходного кода.  
TO BE : В корень проекта стоит добавить файл LICENSE с подходящим лицензионным соглашением  
COST : 1  
PRIORITY : MAJOR  
### 2.	Исходный код  
1. WHAT : Документация не приложена к коду.  
TO BE: Ко всем классам должны быть добавлены комментарии с описанием его назначения.  
COST: 1.5  
PRIORITY: NORMAL
2. WHAT : Недостаток Unit test-ов.  
TO BE: Необходимо покрыть тестами как можно большую часть кода.  
COST: 3   
PRIORITY: NORMAL   
3. WHAT : Незначительное дублирование кода.
TO BE: Устранить, где возможно, путём замены дублированного кода shared-методами.
COST: 1.5   
PRIORITY: NORMAL
# Оценка и план  
Максимальным допустимым уровнем долга примем 40% от общей стоимости спринта в SP. Для 4 спринта составляет 12 * 0.4 = 4.8 SP.
Минимальным уровнем долга, когда о нем можно не беспокоится, примем 20% от общей стоимости спринта в SP. Для 4 спринта составляет 12 * 0.2 = 2.4.  
### Текущее состояние отражено в таблице.  
|MINOR|NORMAL|MAJOR|
|-----|:----:|----:|
|0.6  |3.0   |2.0  |
### Итого: объем долга равен 4.8
До окончания спринта есть еще достаточно времени, и если его правильно распределить можно успеть закрыть долг если и не полностью, то по-максимуму. 
### Цель работы по ликвидации долга — понизить его до 2.4. 
### Было решено ликвидировать следующие долги:
 
— добавление лицензирования исходного кода.  
— покрытие части кода юнит-тестами.  
— избавление от повторяющегося кода. 