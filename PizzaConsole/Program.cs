using Microsoft.EntityFrameworkCore;
using PizzaStoreApp;
using pda = PizzaStoreApp.DataAccess;
using PizzaStoreAppLibrary;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PizzaConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<pda.PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(SecretString.ConnectionString);
            var options = optionsBuilder.Options;

            var dbContext = new pda.PizzaStoreDBContext(options);
            IPizzaStoreRepo PR = new pda.PizzaStoreRepo(dbContext);


            string UserInput;
            Console.WriteLine("Login (l), Quit (q) or Admin (a)");
            UserInput = Console.ReadLine();
            char CurrentAction = UserInput[0];
            CurrentAction = Char.ToLower(CurrentAction);
            while (CurrentAction == 'l' || CurrentAction == 'a')
            {

                string username, password;

                Console.WriteLine("Please enter your username:");
                username = Console.ReadLine();
                Console.WriteLine("Password:");
                password = Console.ReadLine();

                if (CurrentAction == 'a')
                {
                    try
                    {
                        AdminLoop(username, password, PR);
                    }
                    catch (InvalidLoginException e)
                    {
                        Console.WriteLine(e.Message);

                    }
                }

                if (CurrentAction == 'l')
                {
                    try
                    {
                        CustomerLoop(username, password, PR);

                    }
                    catch (InvalidLoginException e)
                    {
                        Console.WriteLine(e.Message);

                    }
                }

                Console.WriteLine("Login (l), Quit (q) or Admin (a)");
                UserInput = Console.ReadLine();
                CurrentAction = UserInput[0];
                CurrentAction = Char.ToLower(CurrentAction);

            }



        }

        private static void AdminLoop(string username, string password, IPizzaStoreRepo PR)
        {
            if (username != SecretString.AdminUsername || password != SecretString.AdminPassword)
            {
                throw new InvalidLoginException("Invalid Admin Username and/or Password");
            }
            Console.WriteLine("(A)dd location, (C)lose location, display order history by (u)ser, display order history by " +
                "(s)tore location, order history by user (l)ocation, search user by (n)ame, display details of an (o)rder (any other key to quit).");
            string UserInput = Console.ReadLine();
            char CurrentAction = char.ToLower(UserInput[0]);
            char[] AcceptableActions = { 'a', 'c', 'u', 'l', 'n', 'o', 'r', 'l' };
            if (!Array.Exists(AcceptableActions, c => c == CurrentAction))
                return;
            while (Array.Exists(AcceptableActions, c => c == CurrentAction))
            {
                if (CurrentAction == 'a')
                {
                    AddLocation(username, password, PR);
                }
                if (CurrentAction == 'c')
                {
                    CloseLocation(username, password, PR);
                }
                if (CurrentAction == 'u')
                {
                    OrderHistoryByName(username, password, PR);
                }
                if (CurrentAction == 'l')
                {
                    OrderHistoryByStoreLocation(username, password, PR);
                }
                if (CurrentAction == 'n')
                {
                    SearchUserByName(username, password, PR);
                }
                if (CurrentAction == 'o')
                {
                    DetailsOfOrder(username, password, PR);
                }
                if (CurrentAction == 'r')
                {
                    ResetUserPassword(username, password, PR);
                }
                if (CurrentAction == 'l')
                {
                    OrderHistoryByUserLocation(username, password, PR);
                }


                Console.WriteLine("(A)dd location, (C)lose location, display order history by (u)ser, display order history by " +
                  "(l)ocation, search user by (n)ame, display details of an (o)rder, (r)eset user password (any other key to quit).");
                UserInput = Console.ReadLine();
                CurrentAction = Char.ToLower(UserInput[0]);

            }


        }

        private static void OrderHistoryByUserLocation(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Enter username of user");
            string Input = Console.ReadLine();
            CustomerClass user = PR.LoadCustomerByUsername(Input);
            Console.WriteLine("Enter zip code of address");
            Input = Console.ReadLine();
            int.TryParse(Input, out int TargetZip);
            
            List<OrderClass> orders = (List<OrderClass>)PR.LoadOrdersByCustomer(user).Where(e => e.DeliveryAddress.Zip == TargetZip);
            List<OrderClass> sortedOrders = null;
            char response;
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }
            char[] validResponses = { 'r', 'o', 'm', 'l', 'q' };
            do
            {


                Console.WriteLine("Sort by most (r)ecent, (o)ldest, (m)ost expensive or (l)east expensive? (q to give up)");
                response = char.ToLower(Console.ReadLine()[0]);
                if (response == 'r')
                {
                    sortedOrders = orders.OrderBy(o => o.DatePlaced).ToList();
                }
                else if (response == 'o')
                {
                    sortedOrders = orders.OrderByDescending(o => o.DatePlaced).ToList();
                }
                else if (response == 'm')
                {
                    sortedOrders = orders.OrderByDescending(o => o.TotalCost).ToList();
                }
                else if (response == 'l')
                {
                    sortedOrders = orders.OrderBy(o => o.TotalCost).ToList();
                }
                else if (response == 'q')
                {
                    Console.WriteLine("Ah, well, nevermind then.");
                    return;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't get that. Please try again.");
                }
            } while (!validResponses.Contains(response));
            foreach (var order in sortedOrders)
            {
                Console.WriteLine($"{order.DatePlaced}: {order.User} ordered {order.pizzas.Count} pizzas for {order.TotalCost}");
            }
            Console.WriteLine("Press return to continue.");
            Console.ReadLine();
        }

        private static void AddLocation(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Creating new store");
            Console.WriteLine("Store Name:");
            StoreClass NewStore = new StoreClass(Console.ReadLine());
            try
            {

                Console.WriteLine("Address, line 1:");
                NewStore.Address.Street = Console.ReadLine();
                Console.WriteLine("Address, line 2:");
                NewStore.Address.Apartment = Console.ReadLine();
                Console.WriteLine("City:");
                NewStore.Address.City = Console.ReadLine();
                Console.WriteLine("State:");
                NewStore.Address.State = Console.ReadLine();
                Console.WriteLine("Zip:");
                Int32.TryParse(Console.ReadLine(), out int tempZip);
                NewStore.Address.Zip = tempZip;

                PR.AddStore(AdminUsername: username, AdminPassword: password, location: NewStore);
                PR.Save();

            }
            catch (InvalidNullFieldException e)
            {
                Console.WriteLine($"Error: {e.Message} cannot be null");
                return;
                
            }
            catch (InvalidLoginException e)
            {
                Console.WriteLine("Admin Login or Password does not match database. Contact DB Admin.");
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("Store added.");
        }

        private static void CloseLocation(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Store name:");
            string NameOfLocationToClose = Console.ReadLine();
            List<StoreClass> stores = (List<StoreClass>) PR.LoadLocations();
            StoreClass StoreToClose = stores.Where(s => s.Name == NameOfLocationToClose).First();
            if(StoreToClose == null)
            {
                Console.WriteLine("No store by that name found.");
                return;
            }
            Console.WriteLine($"Are you sure you want to close the location {StoreToClose.Name} in {StoreToClose.Address.City}, {StoreToClose.Address.State}?");
            string answer = Console.ReadLine();
            if(answer == "Yes")
            {
                try
                {
                    PR.RemoveLocation(username, password, StoreToClose);
                    PR.Save();
                    Console.WriteLine("Store Removed.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    
                }
            }
            else
            {
                Console.WriteLine("Store closure canceled.");
            }

        }
        private static void SearchUserByName(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("User First Name:");
            string UserFirstName = Console.ReadLine();
            Console.WriteLine("User Last Name:");
            string UserLastName = Console.ReadLine();
            List<CustomerClass> customers = (List<CustomerClass>) PR.LoadCustomerByName(UserFirstName, UserLastName);
            CustomerClass cust = null;
            if (customers.Count > 1)
            {
                Console.WriteLine("Multiple users by that name found. (L)ist all?");
                char ans = char.ToLower(Console.ReadLine()[0]);
                if (ans == 'l')
                {
                    for(int i=0; i<customers.Count; i++)
                    {
                        Console.WriteLine($"Customer {i} Username: {customers[i].Username}");
                    }
                }
                Console.WriteLine("Enter customer number to retrieve information on that customer");
                Int32.TryParse(Console.ReadLine(), out int custNum);
                cust = customers[custNum];
            } else if(customers.Count == 0)
            {
                Console.WriteLine("No customers by that name found.");
            }
            else
            {
                cust = customers[0];

            }
            Console.WriteLine($"Username: {cust.Username}");
            for (int i = 0; i < cust.Addresses.Count(); i++)
            {
                Console.WriteLine($"Address #{i} zip code: {cust.Addresses[i].Zip}");
            }
            Console.WriteLine($"Favorite Store Name: {cust.FavoriteStore}");

            Console.WriteLine("Press return to continue.");
            Console.ReadLine();


        }
        private static void OrderHistoryByStoreLocation(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Enter Location name or city.");
            string Input = Console.ReadLine();
            StoreClass store;
            List<StoreClass> stores = (List<StoreClass>) PR.LoadLocations();
            List<StoreClass> StoresByCity = stores.Where(s => s.Address.City == Input).ToList();
            List<StoreClass> StoresByName = stores.Where(s => s.Name == Input).ToList();
            if (StoresByName.Count == 1)
            {
                store = StoresByName[0];
            } else 
            if (StoresByCity.Count > 1)
            {
                Console.WriteLine("Multiple stores in that city found. Specify by name.");
                foreach (var st in StoresByCity)
                {
                    Console.WriteLine(st.Name);
                }
                return;
            }
            else if (StoresByCity.Count == 0 && StoresByName.Count == 0)
            {
                Console.WriteLine("No stores in specified city or by specified name.");
                return;
            }
            else if (StoresByCity.Count == 1)
            {
                store = StoresByCity[0];
            }
            else
            {
                Console.WriteLine("Something has gone horribly, horribly wrong.");
                return;
            }
            List<OrderClass> orders = (List<OrderClass>) PR.LoadOrdersByLocation(store);
            List<OrderClass> sortedOrders = null;
            char response;
            char[] validResponses = { 'r', 'o', 'm', 'l', 'q' }; 
            do
            {


                Console.WriteLine("Sort by most (r)ecent, (o)ldest, (m)ost expensive or (l)east expensive? (q to give up)");
                response = char.ToLower(Console.ReadLine()[0]);
                if (response == 'r')
                {
                    sortedOrders = orders.OrderBy(o => o.DatePlaced).ToList();
                }
                else if (response == 'o')
                {
                    sortedOrders = orders.OrderByDescending(o => o.DatePlaced).ToList();
                }
                else if (response == 'm')
                {
                    sortedOrders = orders.OrderByDescending(o => o.TotalCost).ToList();
                }
                else if (response == 'l')
                {
                    sortedOrders = orders.OrderBy(o => o.TotalCost).ToList();
                } else if (response == 'q')
                {
                    Console.WriteLine("Ah, well, nevermind then.");
                    return;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't get that. Please try again.");
                }
            } while (!validResponses.Contains(response));
            foreach (var order in sortedOrders)
            {
                Console.WriteLine($"{order.DatePlaced}: {order.User} ordered {order.pizzas.Count} pizzas for {order.TotalCost}");
            }
            Console.WriteLine("Press return to continue.");
            Console.ReadLine();

        }
        private static void OrderHistoryByName(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Enter username of user");
            string Input = Console.ReadLine();
            CustomerClass user = PR.LoadCustomerByUsername(Input);
            List<OrderClass> orders = (List<OrderClass>)PR.LoadOrdersByCustomer(user);
            List<OrderClass> sortedOrders = null;
            char response;
            char[] validResponses = { 'r', 'o', 'm', 'l', 'q' };
            do
            {


                Console.WriteLine("Sort by most (r)ecent, (o)ldest, (m)ost expensive or (l)east expensive? (q to give up)");
                response = char.ToLower(Console.ReadLine()[0]);
                if (response == 'r')
                {
                    sortedOrders = orders.OrderBy(o => o.DatePlaced).ToList();
                }
                else if (response == 'o')
                {
                    sortedOrders = orders.OrderByDescending(o => o.DatePlaced).ToList();
                }
                else if (response == 'm')
                {
                    sortedOrders = orders.OrderByDescending(o => o.TotalCost).ToList();
                }
                else if (response == 'l')
                {
                    sortedOrders = orders.OrderBy(o => o.TotalCost).ToList();
                }
                else if (response == 'q')
                {
                    Console.WriteLine("Ah, well, nevermind then.");
                    return;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't get that. Please try again.");
                }
            } while (!validResponses.Contains(response));
            foreach (var order in sortedOrders)
            {
                Console.WriteLine($"{order.DatePlaced}: {order.User} ordered {order.pizzas.Count} pizzas for {order.TotalCost}");
            }
            Console.WriteLine("Press return to continue.");
            Console.ReadLine();
        }
        private static void DetailsOfOrder(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Enter the username of the ordering customer:");
            string CustUsername = Console.ReadLine();
            CustomerClass customer = PR.LoadCustomerByUsername(CustUsername);
            if (customer == null)
            {
                Console.WriteLine("No such customer found.");
                return;
            }
            Console.WriteLine("Enter the year of the order:");
            int YearOfOrder = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the month (number) of the order:");
            int MonthOfOrder = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the day of the month of the order:");
            int DayOfOrder = int.Parse(Console.ReadLine());
            List<OrderClass> orders = PR.LoadOrdersByCustomer(customer).ToList();
            List<OrderClass> OrderResults = orders.Where(o => o.DatePlaced.Year == YearOfOrder && o.DatePlaced.Month == MonthOfOrder && o.DatePlaced.Day == DayOfOrder).OrderBy(o=>o.DatePlaced).ToList();
            if (OrderResults.Count == 0)
            {
                Console.WriteLine("Customer placed no orders on that day.");
                return;
            }
            else
            {
                foreach (var order in OrderResults)
                {
                    Console.WriteLine($"{order.DatePlaced}:");
                    foreach (var pizza in order.pizzas)
                    {
                        DisplayPizza(pizza);
                    }
                    Console.WriteLine($"Total: {order.TotalCost}");
                }
            }
            Console.WriteLine("Press return to continue.");
            Console.ReadLine();


        }
        private static void ResetUserPassword(string username, string password, IPizzaStoreRepo PR)
        {
            Console.WriteLine("Enter customer username:");
            string CustUsername = Console.ReadLine();
            CustomerClass customer = PR.LoadCustomerByUsername(CustUsername);
            if(customer == null)
            {
                Console.WriteLine("No user found.");
                return;
            }
            Console.WriteLine("Enter new Password for User:");
            string NewUserPassword = Console.ReadLine();
            PR.ChangeUserPassword(username, password, customer, NewUserPassword);
        }


        private static void CustomerLoop(string username, string password, IPizzaStoreRepo PR)
        {
            CustomerClass customer = PR.LoadCustomerByUsername(username);
            if (customer == null || !customer.CheckPassword(password))
            {
                throw new InvalidLoginException("Invalid Username and/or Password");
            }
            Console.WriteLine("(P)lace order, (V)iew suggested order, (a)dd new address, (q)uit");
            string UserInput = Console.ReadLine();
            char CurrentAction = char.ToLower(UserInput[0]);
            char[] AcceptableActions = { 'p', 'v'};
            if (!Array.Exists(AcceptableActions, c => c == CurrentAction))
                return;
            while (Array.Exists(AcceptableActions, c => c == CurrentAction))
            {
                if (CurrentAction == 'a')
                {
                    AddCustomerAddress(customer, PR);
                }
                if (CurrentAction == 'v')
                {
                    ViewSuggestedOrder(customer, password, PR);
                }
                if (CurrentAction == 'p')
                {
                    OrderPizza(customer, password, PR);
                }


                Console.WriteLine("(P)lace order, (V)iew suggested order, (a)dd new address, (q)uit");
                UserInput = Console.ReadLine();
                CurrentAction = char.ToLower(UserInput[0]);

            }

        }
        public static void DisplayPizza(PizzaClass pizza)
        {
            Console.Write($"{pizza.Size} pizza with");
            foreach (var topping in pizza.Ingrediants)
            {
                Console.Write($" {topping}");
            }
            Console.WriteLine();

        }

        private static void ViewSuggestedOrder(CustomerClass customer, string password, IPizzaStoreRepo PR)
        {
            OrderClass SuggestedOrder = customer.SuggestOrder();
            if (SuggestedOrder == null)
            {
                Console.WriteLine("You've never ordered from us before. You should try our One With Everything!");
                HashSet<string> AllIngrediants = new HashSet<string>();
                foreach (var topping in OrderClass.Ingrediants)
                {
                    AllIngrediants.Add(topping);
                }
                SuggestedOrder = new OrderClass(customer, password);
                SuggestedOrder.AddPizza(PizzaClass.PizzaSize.XLarge, AllIngrediants);
            }
            else
            {
                Console.WriteLine("Your suggested order is: ");

            }
            foreach (var pizza in SuggestedOrder.pizzas)
            {
                DisplayPizza(pizza);
            }
            Console.WriteLine("(O)rder Suggested, create (n)ew order, or (m)odify suggested order?");
            char ans = char.ToLower(Console.ReadLine()[0]);
            if (ans == 'o')
            {
                PR.PlaceOrder(SuggestedOrder);
            }
            else if (ans == 'n')
                OrderPizza(customer, password, PR);
            else if (ans == 'm')
                OrderPizza(customer, password, PR, SuggestedOrder.pizzas);
            else
                Console.WriteLine("Sorry, I don't understand. Returning to main menu.");
        }

        private static void AddCustomerAddress(CustomerClass customer, IPizzaStoreRepo PR)
        {
            AddressClass address = new AddressClass();
            Console.WriteLine("Enter the street: ");
            address.Street = Console.ReadLine();
            Console.WriteLine("Enter the apartment or room number: ");
            address.Apartment = Console.ReadLine();
            Console.WriteLine("Enter the City: ");
            address.City = Console.ReadLine();
            Console.WriteLine("Enter the State: ");
            address.State = Console.ReadLine();
            Console.WriteLine("Enter the Zip Code: ");
            int.TryParse(Console.ReadLine(), out int tempZip);
            address.Zip = tempZip;

            PR.AddAddressToCustomer(address, customer);

        }

        public static void OrderPizza(CustomerClass customer, string password, IPizzaStoreRepo PR, List<PizzaClass> StarterPizzas = null)
        {
            string answer;
            char ans;
            OrderClass CurrentOrder = new OrderClass(customer, password);
            if (StarterPizzas != null)
            {
                CurrentOrder.pizzas = StarterPizzas;
            }
            bool placeOrder = false, quitLoop = false;
            while (placeOrder == false && quitLoop == false)
            {
                Console.WriteLine($"Your order currently contains {CurrentOrder.pizzas.Count} pizzas. Your subtotal is {CurrentOrder.CostBeforeTax}");
                Console.WriteLine("Would you like to (a)dd a pizza, (r)emove a pizza, (p)lace your order, change (l)ocation or (c)ancel your order?");
                answer = Console.ReadLine();
                ans = char.ToLower(answer[0]);
                if (ans == 'a')
                {
                    try
                    {
                        CurrentOrder.pizzas.Add(AddPizza());

                    }
                    catch (OrderTooExpensiveException e)
                    {
                        Console.WriteLine("Ooof, that's a lot of pizza. I'm sorry, we can't handle orders of more than $500. Try removing some pizza.");
                        
                    }
                    catch (OrderTooLargeException e)
                    {
                        Console.WriteLine("Ooof, that's a lot of pizza. I'm sorry, we can't handle orders of more than 12 pizzas at once. Try removing some pizza.");

                    }
                }
                else if (ans == 'r')
                {
                    RemovePizza(CurrentOrder);
                }
                else if (ans == 'p')
                {
                    Console.WriteLine($"Your final total comes to {CurrentOrder.TotalCost}");
                    PR.PlaceOrder(CurrentOrder);
                    Console.WriteLine("Order Placed. Enjoy your Pizza!");
                }
                else if (ans == 'l')
                {
                    Console.WriteLine("Order Canceled.");
                }
                else if (ans == 'c')
                {
                    Console.WriteLine("Order Canceled.");
                    return;
                }


            }

        }

        public static void RemovePizza(OrderClass order)
        {
            for (int i = 0; i < order.pizzas.Count; i++)
            {
                Console.WriteLine($"Pizza #{i+1}:");
                DisplayPizza(order.pizzas[i]);
            }
            Console.WriteLine("Delete Which Pizza (0 for none)?");
            int ans = int.Parse(Console.ReadLine());
            if (ans == 0)
            {
                Console.WriteLine("Deleting no pizzas.");
            }
            else
            {
                order.RemovePizza(ans-1);
            }
        }

        public static PizzaClass AddPizza()
        {

            PizzaClass.PizzaSize inputSize;
            Console.WriteLine("Size: ");
            foreach (var size in Enum.GetValues(typeof(PizzaClass.PizzaSize)))
            {
                Console.WriteLine($"{size}: {size.ToString()}");
            }

            inputSize = (PizzaClass.PizzaSize)Console.ReadLine()[0];

            string userInput = "y";
            HashSet<string> ingrediants = new HashSet<string>();
            while (userInput[0] != 'd' && userInput[0] != 'D')
            {
                Console.Write($"Your {inputSize.ToString()} pizza has ");
                if (ingrediants.Count == 0)
                {
                    Console.Write("no toppings");
                }
                else
                {
                    foreach (string topping in ingrediants)
                    {
                        Console.Write(topping);
                    }
                }
                Console.WriteLine(".");
                Console.WriteLine("To add a toping, type the topping name. When done, type (d)one.");
                userInput = Console.ReadLine();
                if (userInput[0] != 'd' && userInput[0] != 'D')
                {
                    if (OrderClass.Ingrediants.Contains(userInput))
                    {
                        ingrediants.Add(userInput);
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry, we don't have that ingrediant.");
                    }
                }

            }

            return new PizzaClass(inputSize, ingrediants);

        }
    }
}
