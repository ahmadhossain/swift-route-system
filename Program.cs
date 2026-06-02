using System.Runtime.CompilerServices;
using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Models;

Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║         SwiftRoute System — Courier Startup              ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");

var individualCustomer = new IndividualCustomer { Name = "John" };
var businessCustomer = new BusinessCustomer { Name = "Nick" };

var sender = new PersonInfo("John","1203048854","Dhaka");
var reciever = new PersonInfo("Alice","1203048854","Chittagong");

var documentParcel = new DocumentParcel(0.5);
documentParcel.Sender = sender;
documentParcel.Recipient = reciever;
var standardParcel = new StandandParcel(20);
standardParcel.Sender = sender;
standardParcel.Recipient = reciever;
var fragileParcel = new FragileParcel(1);
fragileParcel.Sender = sender;
fragileParcel.Recipient = reciever;

var bike = new BikeCourier();

fragileParcel.DeclaredValue = 2000;

var dispatcher = new Dispatcher();

var shipment = dispatcher.BookShipment(documentParcel, individualCustomer, ServiceTier.SameDay);
var shipment2 = dispatcher.BookShipment(standardParcel, individualCustomer, ServiceTier.Economy);
var shipment3 = dispatcher.BookShipment(fragileParcel, individualCustomer, ServiceTier.NextDay);
var shipment4 = dispatcher.BookShipment(standardParcel, individualCustomer, ServiceTier.NextDay);

standardParcel.DeclaredValue = 1000;

shipment2.EnableInsurance();
shipment4.EnableCOD(1000);

PrintAllPrices(shipment.GetAllPrices());
PrintAllPrices(shipment2.GetAllPrices());
PrintAllPrices(shipment3.GetAllPrices());
PrintAllPrices(shipment4.GetAllPrices());

try
{
    dispatcher.AssignCourier(shipment2, bike);
}
catch(Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine();
}

shipment.AdvanceStatus(ParcelStatus.PickedUp);
shipment.AdvanceStatus(ParcelStatus.InTransit);
shipment.AdvanceStatus(ParcelStatus.OutForDelivery);
shipment.AdvanceStatus(ParcelStatus.Delivered);

shipment2.AdvanceStatus(ParcelStatus.PickedUp);
shipment2.AdvanceStatus(ParcelStatus.InTransit);
shipment2.AdvanceStatus(ParcelStatus.OutForDelivery);
shipment2.AdvanceStatus(ParcelStatus.Failed);
shipment2.AdvanceStatus(ParcelStatus.Returned);

try
{
    shipment.AdvanceStatus(ParcelStatus.InTransit);
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine();
}

var shipment5 = dispatcher.BookShipment(documentParcel, businessCustomer, ServiceTier.SameDay);
var shipment6 = dispatcher.BookShipment(standardParcel, businessCustomer, ServiceTier.Economy);

shipment5.AdvanceStatus(ParcelStatus.PickedUp);
shipment5.AdvanceStatus(ParcelStatus.InTransit);
shipment5.AdvanceStatus(ParcelStatus.OutForDelivery);
shipment5.AdvanceStatus(ParcelStatus.Delivered);

shipment6.AdvanceStatus(ParcelStatus.PickedUp);
shipment6.AdvanceStatus(ParcelStatus.InTransit);
shipment6.AdvanceStatus(ParcelStatus.OutForDelivery);
shipment6.AdvanceStatus(ParcelStatus.Delivered);

var (shipments, total) = dispatcher.GenerateBusinessInvoice(businessCustomer);
PrintBusinessInvoice(shipments, total);

shipment3.AdvanceStatus(ParcelStatus.PickedUp);
shipment3.AdvanceStatus(ParcelStatus.InTransit);

shipment4.AdvanceStatus(ParcelStatus.PickedUp);
shipment4.AdvanceStatus(ParcelStatus.InTransit);
shipment4.AdvanceStatus(ParcelStatus.OutForDelivery);


PrintShipments(dispatcher.GetInTransitShipments(), "InTransit Shipments");

var (todayShipments, revenew) = dispatcher.GetDeliveredTodayReport();
PrintShipments(todayShipments, "Delivered Today Report");

dispatcher.GetAllShipments();

void PrintShipments(List<Shipment> shipments, string label = "")
{
    Console.WriteLine("------------------------------");
    Console.WriteLine(label.ToUpper());
    Console.WriteLine("------------------------------");

    if (shipments.Count == 0)
    {
        Console.WriteLine("No Shipments Found! \n");
        return;
    }

    foreach (var shipment in shipments)
    {
        Console.WriteLine($"Customer:           {shipment.Customer.Name} \n" +
                          $"Parcel Status:      {shipment.Status} \n");
    }
}

void PrintBusinessInvoice(List<Shipment> shipments, decimal total)
{
    Console.WriteLine("------------------------------");
    Console.WriteLine("       MONTH-END INVOICE      ");
    Console.WriteLine("------------------------------");

    if (shipments.Count == 0)
    {
        Console.WriteLine("No Parcel Found! \n");
        return;
    }

    foreach (var shipment in shipments)
    {
        Console.WriteLine($"Parcel Type: {shipment.Parcel.ParcelType}   Price:  {shipment.CalculateTotalPrice()} \n");
    }
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Total:              {total} \n");
}

void PrintAllPrices(List<decimal> prices)
{
    Console.WriteLine("------------------------------");
    Console.WriteLine("       PRICE BREAKDOWN        ");
    Console.WriteLine("------------------------------");

    Console.WriteLine(  $"Base Rate:        {prices[0]} \n" +
                        $"Weight Charge:    {prices[1]} \n" +
                        $"Fragile Charge:   {prices[2]} \n" +
                        $"Insurence Charge: {prices[3]} \n" +
                        $"COD Charge:       {prices[4]} \n" +
                        $"Discount:         {prices[5]} \n" +
                        $"------------------------------\n" +
                        $"Total:            {prices[6]} \n");
}