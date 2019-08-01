# Salary Application

Application to explain some Machine Learning basics with [ML.Net](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) during the [Euricom](https://www.euri.com/) Dev Cruise of September 2019.

This application covers Prediction and Spike Detection.

## Models

### Employee

The main model of the application.

An employee has an Age, ExperienceLevel and Salary.

Salary is calculated as follows:

```
BaseSalary = Age * 100 * SalaryMultiplier of ExperienceLevel
MinimumSalary = BaseSalary - 10%
MaximumSalary = BaseSalary + 10%
Salary = Random value between MinimumSalary and MaximumSalary
```
### ExperienceLevel

Can be Junior, Medior or Senior with the following Salary Multiplier:

```
Junior : Salary - 10%
Medior : 100% Salary
Senior : Salary + 10%
```

### Payment

Used to generate payments for Spike detection.

Has a Date and an Amount

### PaymentSpikePrediction

Used to get prediction from MachineLearning service.

Has Payment fields + IsSpike and PValue

## Application Menu Options

### 1. Generate Data

This will generate 100 000 employees.

Will display the first 10 employees to visualize the data structure.

### 2. Train Model

Will Train and Evaluate the model. First half of genereated employees will be used for training, last half for evaluating.

Displays the first 10 employees after transforming them to display data used for training.

Displays the Metrics after evaluating the data.

### 3. Plot Regression Chart

Creates and displays a png file with a regression chart of the trained model (uses only the first 100 employees).

### 4. Get Salary Prediction

Will ask for the Age and an ExperienceLevel of an Employee and will predict the Salary.

### 5. Detect Spike

Will ask for the Age and an ExperienceLevel of an Employee and generate 36 payments.

Will then ask for an amount and determines if that amount is a spike or not.

Will also plot a chart with the payments.

### 6. Exit

Will exit the application

