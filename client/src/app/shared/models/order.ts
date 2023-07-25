export interface IOrder{
  cartId: string;
  address: IAddress
}

export interface IAddress{
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  state: string;
}
