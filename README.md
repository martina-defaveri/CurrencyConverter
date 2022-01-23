CONTENTS OF THIS FILE
---------------------

 * Introduction
 * Improvement
 * Configuration


INTRODUCTION
------------

This is a simple currency converter, similar to Google one, made using .net core 3.1 WPF and its relatives test made using XUnit.
It was done using TDD, so all the steps of the backend part were preceded by the creation of the related tests.

The code is composed by different section such as:
 - Api : Code written to reach fixer api and parse its responses
 - Core : Calculation part
 - MainWindows: UI design

 Dependency injection was used to inject the class and app setting.
 Code was written to avoid code smells, for example,  primitive obsession, log method and class or Data Clumps.

 Interfaces are used when it's possible in order to make the code flexible to, for istance, new data provider or new calculator, with no
 UI changes.

 Currencies, to popolate dropdown menu and rates to calculate changes are both provided by http://fixer.io/ using two different api call such as:
  - latest for rates
  - symbols for currencies

 All exceptions are shown on UI in a message box.

IMPROVEMENT
------------
 - Can be add brand new tests in order to check UI functionality using Selenium.
 - Using a vault system to store api key
 - Add an refresh button to get new rates (or check its date)
 - Add a logger

CONFIGURATION
-------------

In order to get this software working, you need to add on the appsetting.json
your APIKey to http://fixer.io/