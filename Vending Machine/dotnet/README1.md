## Vendo-Matic 800

The Vendo-Matic 800 is a Tech Elevator capstone project written in C#.  Intended as the culmination of four weeks of study, Vendo-Matic 800 builds on the core concepts of Object-Oriented Programming - Polymorphism, Inheritance, Encapsulation, Abstraction - while reinforcing algorithmic thinking.

## Installation

No installation needed. Run Capstone.exe found in /Capstone/bin/Debug/netcoreapp3.1/


## Usage

At the start of the program, the user is greeted with a MAIN MENU.
> ```
    > (1) Display Vending Machine Items
    > (2) Purchase
    > (3) Exit
    > ```

Option (1) Will display the current stock of snack items in the machine and will give a running tally of the inventory.  Each item is reloaded to its upper limit - five - each time the program is started.
Option (2) will take the user to the PURCHASE MENU - see below.
Option (3) exits the program.


The PURCHASE MENU is as follows:

 >```
    >(1) Feed Money
    >(2) Select Product
    >(3) Finish Transaction
    >
    > Current Money Provided: $42.00
    >```

Option (1) allows the user to add whole-dollar amounts of money to spend and the total is tracked at the bottom.
Option (2) displays the entire inventory of the machine, with prices, and allows the user to purchase. Once the user is finished, they will select an option to quit and receive change in the form of coins.
Option (3) returns the user to the MAIN MENU.

Vendo-Matic 800 also writes to two separate log files.  Each purchase transaction, money addition, and change request is tallied to an AUDIT file 'Log.txt.' Additionally, by selecting (4) at the main menu, a SALES log is generated (time and date-stamped) with the total sales and number of items sold per snack, over the entirety of the user's session.

## Contributing
This program was written by Ryan DuPerow and Robert Kaiser.
Pull requests are, of course, welcome. 

## License
[MIT](https://choosealicense.com/licenses/mit/)

