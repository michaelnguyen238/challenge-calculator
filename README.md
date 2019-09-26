# challenge-calculator
Restaurant365 Code Challenge - String Calculator

## Summary
Create a calculator that only supports an Add operation given a single formatted string

* C# console application
* Include unit tests
* Provide code via a public distributed version control repository i.e. GitHub. Do NOT fork this repo
* Show each step as a separate commit
* Efficient code is always important but for this excercise readability and separation of concerns are much more critical
* Not including one or more of the stretch goals will not affect your overall assessment but implementing them poorly will

## Requirements
1. Support a maximum of 2 numbers using a comma delimiter
	* examples: `20` will return `20`; `1,5000` will return `5001`
	* invalid/missing numbers should be converted to `0` e.g. "" will return `0`; `5,tytyt` will return `5`
2. Support an unlimited number of numbers e.g. `1,2,3,4,5,6,7,8,9,10,11,12` will return `78`
3. Support a newline character as an alternative delimiter e.g. `1\n2,3` will return `6` 
4. Deny negative numbers. An exception should be thrown that includes all of the negative numbers provided
5. Ignore any number greater than 1000 e.g. `2,1001,6` will return `8`
6. Support 1 custom single character length delimiter
	* use the format: `//{delimiter}\n{numbers}` e.g. `//;\n2;5` will return `7`
	* all previous formats should also be supported
7. Support 1 custom delimiter of any length
	* use the format: `//[{delimiter}]\n{numbers}` e.g. `//[***]\n11***22***33` will return `66`
	* all previous formats should also be supported
8. Support multiple delimiters of any length
	* use the format: `//[{delimiter1}][{delimiter2}]...\n{numbers}` e.g. `//[*][!!][r9r]\n11r9r22*33!!44` will return `110`
	* all previous formats should also be supported

## Stretch goals
[x] Display the formula used to calculate the result e.g. `2,4,rrrr,1001,6` will return `2+4+0+0+6 = 12`
[x] Allow the application to process entered entries until Ctrl+C is used
[x] Allow the acceptance of arguments to define...
	* alternate delimiter in step #3 
	* toggle whether to deny negative numbers in step #4
	* upper bound in step #5
4. Use DI
5. Support subtraction, multiplication, and division operations
