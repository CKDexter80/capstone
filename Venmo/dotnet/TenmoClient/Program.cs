using System;
using System.Collections.Generic;
using TenmoClient.Models;
using System.Linq;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        private static readonly ApiService apiService = new ApiService();

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            while (true)
            {
                int loginRegister = -1;
                while (loginRegister != 1 && loginRegister != 2)
                {
                    Console.WriteLine("Welcome to TEnmo!");
                    Console.WriteLine("1: Login");
                    Console.WriteLine("2: Register");
                    Console.Write("Please choose an option: ");

                    if (!int.TryParse(Console.ReadLine(), out loginRegister))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    else if (loginRegister == 1)
                    {
                        while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                        {
                            LoginUser loginUser = consoleService.PromptForLogin();
                            ApiUser user = authService.Login(loginUser);
                            if (user != null)
                            {
                                UserService.SetLogin(user);
                            }
                        }
                    }
                    else if (loginRegister == 2)
                    {
                        bool isRegistered = false;
                        while (!isRegistered) //will keep looping until user is registered
                        {
                            LoginUser registerUser = consoleService.PromptForLogin();
                            isRegistered = authService.Register(registerUser);
                            if (isRegistered)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Registration successful. You can now log in.");
                                loginRegister = -1; //reset outer loop to allow choice for login
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }

                MenuSelection();
            }
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (menuSelection == 1)
                {
                    Console.WriteLine($"Your current account balance is: {apiService.GetAccountBalance():C2}");
                }
                else if (menuSelection == 2)
                {
                    IList<ViewTransfer> userTransfers = apiService.GetAllUserTransfers();

                    //show all transfers for user
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Transfers");
                    Console.WriteLine("ID           From/To         Amount");
                    Console.WriteLine("-------------------------------------");
                    foreach (ViewTransfer transfer in userTransfers)
                    {
                        if (transfer.ToName == UserService.GetUsername())
                        {
                            Console.WriteLine($"{transfer.TransferId}    From: {transfer.FromName}    {transfer.Amount:C2}");
                        }
                        else if (transfer.FromName == UserService.GetUsername())
                        {
                            Console.WriteLine($"{transfer.TransferId}    To: {transfer.ToName}    {transfer.Amount:C2}");
                        }
                    }

                    var transferId = consoleService.PromptForTransferID("view details");

                    //check if transfer exists on list
                    var checker = false;
                    foreach (ViewTransfer transfer in userTransfers)
                    {
                        if (transferId == transfer.TransferId)
                        {
                            checker = true;
                        }
                    }

                    if (checker == true)
                    {
                        TransferDetails transferDetail = apiService.GetTransferDetail(transferId);

                        Console.WriteLine("");
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Transfer Details");
                        Console.WriteLine("----------------------------------------------");

                        Console.WriteLine($"Id: {transferDetail.TransferId}");
                        Console.WriteLine($"From: {transferDetail.FromName}");
                        Console.WriteLine($"To: {transferDetail.ToName}");
                        Console.WriteLine($"Type: {transferDetail.TransferTypeDesc}");
                        Console.WriteLine($"Status: {transferDetail.TransferStatusDesc}");
                        Console.WriteLine($"Amount: {transferDetail.Amount:C2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID!");
                    }
                }
                else if (menuSelection == 3)
                {
                    Console.WriteLine("Not yet implemented");
                    //throw new NotImplementedException();
                }
                else if (menuSelection == 4)
                {
                    IList<UserTEBucks> users = apiService.GetAllUsers();
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Users");
                    Console.WriteLine("ID      Name");
                    Console.WriteLine("-------------------------------------");

                    foreach (UserTEBucks user in users)
                    {
                        Console.WriteLine($"{user.UserId}      {user.Username}");
                    }

                    var userId = consoleService.PromptForUserID();

                    //var checker = false;
                    //foreach (UserTEBucks user in users)
                    //{
                    //    if (userId == user.UserId)
                    //    {
                    //        checker = true;
                    //    }
                    //}

                    //check if user is a valid user (exists on user list)
                    if (apiService.GetAllUsers().FirstOrDefault(m => m.UserId == userId) != null)
                    {

                        var amount = consoleService.PromptForAmount();

                        //check if there is enough balance for transfer
                        if (amount <= apiService.GetAccountBalance())
                        {
                            Transfer transfer = new Transfer();
                            transfer.Amount = amount;
                            transfer.AccountFrom = apiService.GetAccount().AccountId;
                            transfer.AccountTo = apiService.GetAllUsers().FirstOrDefault(x => x.UserId == userId).AccountId;



                            apiService.CreateTransfer(transfer);
                            apiService.AddToBalance(transfer);
                            apiService.SubtractFromBalance(transfer);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("You don't have enough money to transfer");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid user");
                    }

                }
                else if (menuSelection == 5)
                {
                    Console.WriteLine("Not yet implemented");
                    //throw new NotImplementedException();
                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new ApiUser()); //wipe out previous login info
                    Console.Clear();
                    menuSelection = 0;
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }
    }
}
