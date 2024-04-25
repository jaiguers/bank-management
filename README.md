# bank-management
API en .NET para la gestión de clientes, cuentas de ahorro, depósitos y transacciones. 

## El endpoint para crear usuarios es
```
api/auht/register

```
## Json para crear usuarios
```
{
  "userName": "email@gmail.com",
  "password": "#Mypassword",
  "person": {
    "fullName": "John D",
    "documentType": "CC",
    "docNumber": "1130000000",
    "phone": "3114500",
    "addressList": []
  }
}

```
Maneja base de datos MongoDB con entoty framework
## Una tarjeta creada en la BD, se veria asi:
```
{
  "_id": {
    "$oid": "66288b443b4bdedd3f743e4f"
  },
  "account_type": "type",
  "activated_at": {
    "$date": "2024-04-24T05:54:26.596Z"
  },
  "balance": "550000",
  "cvc": "870",
  "card_number": "4190000025",
  "card_type": "dsdsd",
  "created_at": {
    "$date": "2024-04-24T04:32:02.658Z"
  },
  "expiration_date": "4/2027",
  "name_on_card": "dddd dddd",
  "provider": "Visa",
  "status": "Active",
  "user_id": "52f04830-5521-4e24-848d-bf9008969e68",
  "amounts": [
    {
      "AmountType": "Deposit",
      "Concept": "Transfiere",
      "CreatedAt": {
        "$date": {
          "$numberLong": "-62135596800000"
        }
      },
      "Currency": "COP",
      "Description": "Consignacion",
      "Reference": "ref123",
      "Status": "Success",
      "TaxRate": "0.1",
      "TotalValue": "10000000"
    },
    {
      "AmountType": "Withdrawal",
      "Concept": "concepto",
      "CreatedAt": {
        "$date": "2024-04-24T05:24:16.956Z"
      },
      "Currency": "COP",
      "Description": "Retiro en efectivo JJJ",
      "Reference": "ref9875",
      "Status": "Success",
      "TaxRate": "0.0",
      "TotalValue": "500000"
    },
    {
      "AmountType": "Withdrawal",
      "Concept": "deefete",
      "CreatedAt": {
        "$date": "2024-04-24T05:39:39.664Z"
      },
      "Currency": "COP",
      "Description": "ED Withdrawal",
      "Reference": "ref45566",
      "Status": "Success",
      "TaxRate": "34",
      "TotalValue": "200000"
    },
    {
      "AmountType": "Withdrawal",
      "Concept": "string",
      "CreatedAt": {
        "$date": "2024-04-24T05:53:36.371Z"
      },
      "Currency": "COP",
      "Description": "string",
      "Reference": "string",
      "Status": "Success",
      "TaxRate": "0.0",
      "TotalValue": "250000"
    }
  ]
}

```
E campo "amounts" es la lista de las transacciones
