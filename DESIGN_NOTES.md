# SwiftRoute Design Explanation

## 1. Class List

| Class | Responsibility |
|---------|---------------|
| `Parcel` | Base class that stores common parcel information such as weightKg and declared value. |
| `DocumentParcel` | Represents document shipments and applies document-specific validation rules. |
| `StandardParcel` | Represents standard package shipments with standard-specific constraints. |
| `FragileParcel` | Represents fragile package shipments with fragile-specific constraints. |
| `Customer` | Stores customer information and common customer behavior. |
| `IndividualCustomer` | Represents regular customers with standard pricing rules. |
| `BusinessCustomer` | Represents business customers eligible for discounted pricing. |
| `Courier` | Base class representing a delivery vehicle and its capabilities. |
| `BikeCourier` | Handles lightweight deliveries using a bike. |
| `VanCourier` | Handles medium-sized deliveries using a van. |
| `TruckCourier` | Handles heavy deliveries using a truck. |
| `Shipment` | Combines parcel, customer, courier assignment, pricing, and delivery status into a single booking. |
| `Dispacher` | Coordinates booking, courier assignment, status updates, and shipment tracking. |

## 2. Inheritance vs Composition

### Inheritance

#### Parcel Hierarchy

```text
Parcel
 ├── DocumentParcel
 ├── FragileParcel
 └── StandardParcel
```

**Reason:** Different parcel types share common attributes but have different validation and pricing behavior.

#### Customer Hierarchy

```text
Customer
 ├── IndividualCustomer
 └── BusinessCustomer
```

**Reason:** All customers share common data but differ in discount and billing rules.

#### Courier Hierarchy

```text
Courier
 ├── BikeCourier
 ├── VanCourier
 └── TruckCourier
```

**Reason:** Each courier type has different carrying capacity and parcel compatibility rules.

### Composition

#### Shipment Contains

```text
Shipment
 ├── Parcel
 ├── Customer
 └── Courier
```

**Reason:** A shipment has a parcel and customer rather than being one.

#### Dispacher Uses

```text
Dispacher
 └── Shipments
```

**Reason:** The hub coordinates these services but does not inherit from them.

### Interface

IFragilable, IInsuranceable, ICashOnDeliveryable, IShipment, IDispatcher because with those common funtionalities (funtions), code will be broken.

## 3. Where Lifecycle Rules Live

### Ownership

Shipment Class

### Why Here?

Keeping the lifecycle rule in `Shipment` centralizes validation and prevents invalid state transitions.

## 4. Where Pricing Rules Live

### Ownership

Shipment Class

### Why Here?

Keeping the pricing rule in `Shipment` centralizes validation and prevents invalid state transitions.


### Adding a New Parcel Type: Refrigerated

Requirements:

- Cold-chain shipment
- Maximum weight: 20 kg
- Requires Van or Truck
- Additional surcharge: 100 BDT

### Files That Change

1. `RefrigeratedParcel.cs`

### Files That Do Not Change

- `Shipment.cs`
- `Dispatcher.cs`

### Justification

Ifragile interface added for the surcharge. Adding that class will not break existing code.