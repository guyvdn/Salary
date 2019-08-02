# Salary Application

Application to explain some Machine Learning basics with [ML.Net](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) during the [Euricom](https://www.euri.com/) Dev Cruise of September 2019.\
This application covers Prediction and Spike Detection.

## Table of Contents

**[Models](#models)**\
**[Services](#services)**\
**[Application Menu Options](#application-menu-options)**

## Models

### Employee

The main model of the application.\
An employee has an Age, ExperienceLevel and Salary.\
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

Used to generate payments for Spike detection.\
Has a Date and an Amount

### PaymentSpikePrediction

Used to get prediction from MachineLearning service.\
Has Payment fields + IsSpike and PValue

## Application Menu Options

### 1. Generate Data

This will generate 100 000 employees.\
Will display the first 10 employees to visualize the data structure.

### 2. Train Model

Will Train and Evaluate the model. First half of genereated employees will be used for training, last half for evaluating.\
Displays the first 10 employees after transforming them to display data used for training.\
Displays the Metrics after evaluating the data.

#### Metrics

| Metric | Description | Look for |
| --- | --- | --- |
| **R-Squared** | [R-squared (R2)](https://en.wikipedia.org/wiki/Coefficient_of_determination), or *Coefficient of determination* represents the predictive power of the model as a value between -inf and 1.00. 1.00 means there is a perfect fit, and the fit can be arbitrarily poor so the scores can be negative. A score of 0.00 means the model is guessing the expected value for the label. R2 measures how close the actual test data values are to the predicted values. | **The closer to 1.00, the better quality.** However, sometimes low R-squared values (such as 0.50) can be entirely normal or good enough for your scenario and high R-squared values are not always good and be suspicious. |
| **Absolute-loss** | [Absolute-loss](https://en.wikipedia.org/wiki/Mean_absolute_error) or *Mean absolute error (MAE)* measures how close the predictions are to the actual outcomes. It is the average of all the model errors, where model error is the absolute distance between the predicted label value and the correct label value. This prediction error is calculated for each record of the test data set. Finally, the mean value is calculated for all recorded absolute errors.| **The closer to 0.00, the better quality.** Note that the mean absolute error uses the same scale as the data being measured (is not normalized to specific range). Absolute-loss, Squared-loss, and RMS-loss can only be used to make comparisons between models for the same dataset or dataset with a similar label value distribution. |
| **Squared-loss** | [Squared-loss](https://en.wikipedia.org/wiki/Mean_squared_error) or *Mean Squared Error (MSE)*, also called Mean Squared Deviation (MSD), tells you how close a regression line is to a set of test data values. It does this by taking the distances from the points to the regression line (these distances are the errors E) and squaring them. The squaring gives more weight to larger differences.| It is always non-negative, and **values closer to 0.00 are better.** Depending on your data, it may be impossible to get a very small value for the mean squared error. |
| **RMS-loss**| [RMS-loss](https://en.wikipedia.org/wiki/Root-mean-square_deviation) or *Root Mean Squared Error (RMSE)* (also called *Root Mean Square Deviation, RMSD*), measures the difference between values predicted by a model and the values actually observed from the environment that is being modeled. RMS-loss is the square root of Squared-loss and has the same units as the label, similar to the absolute-loss though giving more weight to larger differences. Root mean square error is commonly used in climatology, forecasting, and regression analysis to verify experimental results.| It is always non-negative, and **values closer to 0.00 are better.** RMSD is a measure of accuracy, to compare forecasting errors of different models for a particular dataset and not between datasets, as it is scale-dependent.

Source: [Model evaluation metrics in ML.NET - Metrics for Regression](https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics#metrics-for-regression)

### 3. Plot Regression Chart

Creates and displays a png file with a regression chart of the trained model (uses only the first 100 employees).

### 4. Get Salary Prediction

Will ask for the Age and an ExperienceLevel of an Employee and will predict the Salary.

### 5. Detect Spike

Will ask for the Age and an ExperienceLevel of an Employee and generate 36 payments.\
Will then ask for an amount and determines if that amount is a spike or not.\
Will also plot a chart with the payments.

### 6. Exit

Will exit the application

