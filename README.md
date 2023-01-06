# API_excel
Test assignment for an intern for the position
of "Programmer in C#"
Task No. 1
Write a WebAPI application with 3 methods.
The first method
Accepts a file of the form .csv, in which on each new line the value of the form:
{Date and time in the format YYYY-MM-DD_hh-mm-ss};{Integer time value in seconds};{Indicator in the form of a floating-point number}
Example:
2022-03-18_09-18-17;1744;1632,472

If a file with the same name already exists, overwrite the values in the database.
This file is parsed, the values are written to the database in the Values table.
Validation:
The date cannot be later than the current one and earlier than 01.01.2000
The time cannot be less than 0
The value of the indicator cannot be less than 0
The number of rows cannot be less than 1 and more than 10,000
The following values are calculated from the values and written to the Results table:
All time (maximum time value minus minimum time value)
Minimum date and time as the moment of the first operation start
Average execution time
Average value by indicators
Medina by indicators
The maximum value of the indicator
The minimum value of the indicator
Number of rows
The second method
Returns Results in JSON format.
Filters can be applied:
By file name
By the start time of the first operation (from, to)
By the average indicator (in the range)
By average time (in the range)
, the third method
Get values from the Values table by file name
Requirements for sent solutions:
.NET 5+
WebApi
Swagger
MS SQL
Ef core
Pay attention to performance
Pay attention to the architecture and style of the project code
It is advisable to cover unit tests (The choice of technologies is not limited, but you can choose useful ones from these: NUnit/xUnit, AutoFixture, Moq)
